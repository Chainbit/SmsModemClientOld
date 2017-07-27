using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class MainForm : Form
    {
        private delegate void EditStatusTextDelegate(string strArg);


        ComPortManager manager;
        SmsModemBlock selectedPort;


        public MainForm()
        {
            InitializeComponent();
            manager = new ComPortManager(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
        // кнопка "загрузить ком-порты"
        private void loadComsButton_Click(object sender, EventArgs e)
        {
            //очищаем все
            dataGridView1.Rows.Clear();
            for (int i = 0; i < manager.activeComs.Count; i++)
            {
                //заполняем датагрид
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["number"].Value = i;
                dataGridView1.Rows[i].Cells["ComPortName"].Value = manager.activeComs[i].Name;
                dataGridView1.Rows[i].Cells["isOpen"].Value = manager.activeComs[i].sp.IsOpen;

                CheckOpen(i);
            }
        }

        // выбор конкретного порта
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            var senderRow = senderGrid.CurrentRow.Index;
            try
            {
                selectedPort.sp.Close();
            }
            catch
            {
                //если порт не был выбран до этого
            }
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                try
                {
                    //обработка выбора СОМ-порта
                    var portName = senderGrid.Rows[e.RowIndex].Cells["ComPortName"].Value.ToString();
                    //MessageBox.Show(string.Format("Выбран порт {0}", portName));
                    selectedPort = manager.GetBlockByName(portName);
                    selectedPortName.Text = selectedPort.Name;
                    selectedPort.sp.Open();
                    selectedPort.MF = this;

                    CheckOpen(senderRow);
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Ничего не выбрано!");
                }
            }
        }

        private void request_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedPort != null && e.KeyCode == Keys.Enter)
            {
                selectedPort.AtCommand(request.Text);
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (selectedPort != null)
            selectedPort.AtCommand(request.Text);
        }

        // метод для отображения сообщения с ком-порта в виде строки
        public void PrintMessage(string message)
        {
            var bytes = Encoding.Unicode.GetBytes(message);
            string str = Encoding.Unicode.GetString(bytes);
            // его нужно инвокать
            if (this.InvokeRequired)
            {
                this.Invoke(new EditStatusTextDelegate(UpdateTextBox), new object[] { str.Trim() });
                return;
            }
            UpdateTextBox(str);
        }

        // метод для отображения сообщения с ком-порта в виде байтов 
        public void PrintMessage(byte[] data)
        {
            var unicode = Encoding.Unicode.GetChars(data);
            var utf = Encoding.UTF8.GetChars(data);
            var ascii = Encoding.ASCII.GetChars(data);

            string unicodestr = new string(unicode);
            string utfstr = new string(utf);
            string asciistr = new string(ascii);
            // его нужно инвокать
            if (this.InvokeRequired)
            {
                this.Invoke(new EditStatusTextDelegate(UpdateTextBox), new object[] { selectedPort.sp.Encoding.EncodingName+" to Unicode: "+unicodestr });
                this.Invoke(new EditStatusTextDelegate(UpdateTextBox), new object[] { selectedPort.sp.Encoding.EncodingName + " to UTF-8: " + utfstr });
                this.Invoke(new EditStatusTextDelegate(UpdateTextBox), new object[] { selectedPort.sp.Encoding.EncodingName + " to ASCII: " + asciistr });
                return;
            }
            //UpdateTextBox(unicode.ToString());
        }

        private void UpdateTextBox(string str)
        {
            if (response.TextLength > response.MaxLength)
            {
                response.Text = response.Text.Remove(0, response.TextLength - response.MaxLength);
            }

            response.AppendText(str);
            response.AppendText(Environment.NewLine);
            response.ScrollToCaret();
        }


        /// <summary>
        /// метод для перекраски ячеек
        /// </summary>
        /// <param name="i"> номер ячейки</param>
        private void CheckOpen(int i)
        {
            dataGridView1.Rows[i].Cells["isOpen"].Value = manager.activeComs[i].sp.IsOpen;

            if (manager.activeComs[i].sp.IsOpen == false)
            {
                dataGridView1.Rows[i].Cells["isOpen"].Style.BackColor = Color.Red;
            }
            else
            {
                dataGridView1.Rows[i].Cells["isOpen"].Style.BackColor = Color.Green;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages["commandsTab"])
                {
                    selectedPort.ModemMode = Mode.Commands;
                }
                else if (tabControl1.SelectedTab == tabControl1.TabPages["SMSListTab"])
                {
                    selectedPort.ModemMode = Mode.RecieveSMS;
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void getSMSListButton_Click(object sender, EventArgs e)
        {
            if (selectedPort != null)
            {
                selectedPort.Inbox.Clear();

                selectedPort.AtCommand("AT+CMGL=\"ALL\"");
                do
                {
                    Thread.Sleep(500);
                }
                while (!selectedPort.isRecieved);
                //предварительно очищаем таблицу
                SMSList.Rows.Clear();
                for (int i = 0; i < selectedPort.Inbox.Count; i++)
                {
                    //заполняем датагрид
                    SMSList.Rows.Add();
                    SMSList.Rows[i].Cells["index"].Value = selectedPort.Inbox[i].Index;
                    SMSList.Rows[i].Cells["sender"].Value = selectedPort.Inbox[i].Sender;
                    SMSList.Rows[i].Cells["timeStamp"].Value = selectedPort.Inbox[i].Date;
                }
            }
        }

        private void SMSList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            var senderRow = senderGrid.CurrentRow.Index;
            
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewColumn &&
                e.RowIndex >= 0)
            {
                try
                {
                    int index = (int)senderGrid.Rows[e.RowIndex].Cells["index"].Value;
                    SMSsender.Text = senderGrid.Rows[e.RowIndex].Cells["sender"].Value.ToString();
                    SMStime.Text = senderGrid.Rows[e.RowIndex].Cells["timeStamp"].Value.ToString();
                    SMStext.Text = selectedPort.Inbox.Find(sms => sms.Index == index).Message;
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Ничего не выбрано!");
                }
            }
        }

        private void DecodeButton_Click(object sender, EventArgs e)
        {
            SMStext.Text = Decode.USC2ToString(SMStext.Text) + Environment.NewLine + Decode.Decode7bit(SMStext.Text);
        }
    }
}
