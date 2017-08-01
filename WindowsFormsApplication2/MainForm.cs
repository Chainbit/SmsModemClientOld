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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            manager = new ComPortManager(this);
            FillDatagrid1();
        }

        // кнопка "загрузить ком-порты"
        private async void loadComsButton_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            //Ждкм выполнения команды
            await manager.GetModemPortsAsync();

            FillDatagrid1();

            (sender as Button).Enabled = true;
        }

        private void FillDatagrid1()
        {
            if (selectedPort != null)
            {
                selectedPort.sp.Close();
                selectedPort = null;
            }
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
            //var senderRow = senderGrid.CurrentRow.Index;
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
                    foreach (DataGridViewRow row in senderGrid.Rows)
                    {
                        CheckOpen(row.Index);
                    }
                    
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Ничего не выбрано!");
                }
            }
        }

        // послать команду на Enter
        private void request_KeyDown(object sender, KeyEventArgs e)
        {
            selectedPort.ModemMode = Mode.Commands;
            if (selectedPort != null && e.KeyCode == Keys.Enter)
            {
                selectedPort.AtCommand(request.Text);
            }
        }

        // послать команду на Click
        private async void sendButton_Click(object sender, EventArgs e)
        {
            sendButton.Enabled = false;
            await selectedPort.ResponseRecieved();

            selectedPort.ModemMode = Mode.Commands;
            if (selectedPort != null)
            selectedPort.AtCommand(request.Text);

            sendButton.Enabled = true;
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

        // обновление текстбокса
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
            var x = dataGridView1.Rows[i].Cells["ComPortName"].Value;

            if (manager.activeComs[i].sp.IsOpen == false)
            {
                dataGridView1.Rows[i].Cells["isOpen"].Style.BackColor = Color.Red;
            }
            else
            {
                dataGridView1.Rows[i].Cells["isOpen"].Style.BackColor = Color.Green;
            }
        }

        // возможно не нужно
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

        // обработчик нажатия на "Получить список смс"
        private void getSMSListButton_Click(object sender, EventArgs e)
        {
            getSMSList();
        }

        /// <summary>
        /// Получить список смс и загрузить его в таблицу
        /// </summary>
        private async void getSMSList()
        {
            selectedPort.ModemMode = Mode.RecieveSMS;
            if (selectedPort != null)
            {
                SMSList.Rows.Clear();
                // отключаем кнопку
                getSMSListButton.Enabled = false;
                var temp = getSMSListButton.Text;
                getSMSListButton.Text = "Зарузка...";

                //ждем входящие
                var inbox = await LoadSmsInbox();
                // заполняем таблицу
                FillSMSTable(inbox);

                getSMSListButton.Text = temp;
                getSMSListButton.Enabled = true;
            }
        }

        //Выбор СМСки
        private void SMSList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewColumn &&
                e.RowIndex >= 0)
            {
                try
                {
                    SMSindex.Text = senderGrid.Rows[e.RowIndex].Cells["index"].Value.ToString();
                    SMSsender.Text = senderGrid.Rows[e.RowIndex].Cells["sender"].Value.ToString();
                    SMStime.Text = senderGrid.Rows[e.RowIndex].Cells["timeStamp"].Value.ToString();
                    SMStext.Text = senderGrid.Rows[e.RowIndex].Cells["text"].Value.ToString(); 
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

        /// <summary>
        /// Метод, возвращающий асинхронную задачу на получение списка входячих
        /// </summary>
        /// <returns></returns>
        private Task<List<RecievedSMS>> LoadSmsInbox()
        {
            return Task.Run(() =>
            {
                selectedPort.Inbox.Clear();

                selectedPort.AtCommand("AT+CMGL=\"ALL\"");
                do
                {
                    Thread.Sleep(500);
                }
                while (!selectedPort.isRecieved);

                return selectedPort.Inbox;
            });
        }

        private void deleteMessageButton_Click(object sender, EventArgs e)
        {
            selectedPort.AtCommand("AT+CMGD=" + SMSList.CurrentRow.Cells["index"].Value);
            ClearSMSInfo();
            getSMSList();
        }

        /// <summary>
        /// Заполнить таблицу данными
        /// </summary>
        /// <param name="inbox">список входящих сообщений</param>
        private void FillSMSTable(List<RecievedSMS> inbox)
        {
            //предварительно очищаем таблицу
            SMSList.Rows.Clear();
            for (int i = 0; i < inbox.Count; i++)
            {
                //заполняем датагрид
                SMSList.Rows.Add();
                SMSList.Rows[i].Cells["index"].Value = inbox[i].Index;
                SMSList.Rows[i].Cells["sender"].Value = inbox[i].Sender;
                SMSList.Rows[i].Cells["timeStamp"].Value = inbox[i].Date;
                SMSList.Rows[i].Cells["text"].Value = inbox[i].Message;
            }
            //????
            inbox.Clear();
        }

        /// <summary>
        /// очищаем поле соообщения
        /// </summary>
        private void ClearSMSInfo()
        {
            SMSindex.Text = "";
            SMSsender.Text ="";
            SMStime.Text = "";
            SMStext.Text = "";
        }

        private async void DeleteAllSMS()
        {
            deleteAllButton.Enabled = false;

            selectedPort.AtCommand("AT+CMGD=" + SMSList.CurrentRow.Cells["index"].Value + ",4");
            await selectedPort.ResponseRecieved();

            deleteAllButton.Enabled = true;
        }

        private void deleteAllButton_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (answer)
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    DeleteAllSMS();
                    getSMSList();
                    break;
            }
        }

        private void getResponseButton_Click(object sender, EventArgs e)
        {
            selectedPort.RecieveData();
        }
    }
}
