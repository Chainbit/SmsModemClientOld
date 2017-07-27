using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class SmsModemBlock
    {
        public SerialPort sp { get; private set; }
        public string message = string.Empty;
        public string Name { get; private set; }
        public MainForm MF;
        public List<RecievedSMS> Inbox { get; set; }

        public bool isRecieved;
        public EventWaitHandle waitForResponse = new EventWaitHandle(true, EventResetMode.ManualReset);

        public Mode ModemMode;

        /// <summary>
        /// Создает новый объект класса
        /// </summary>
        /// <param name="portName">Имя порта к которому подключен блок</param>
        public SmsModemBlock(string portName)
        {
            sp = new SerialPort(portName);
            //присваиваем имя порту
            Name = sp.PortName;
            sp.BaudRate = 115200;
            sp.Parity = Parity.None;
            sp.StopBits = StopBits.One;
            sp.DataBits = 8;
            sp.Handshake = Handshake.None;
            sp.RtsEnable = true;
            sp.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            ModemMode = Mode.Commands;

            Inbox = new List<RecievedSMS>();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            isRecieved = true;
            string response;
            waitForResponse.Set();
            SerialPort sp = (SerialPort)sender;
            //кодировка порта
            sp.Encoding = Encoding.ASCII;
            //получаем ответ строкой
            response = sp.ReadExisting();
            ////получаем ответ в байтах
            //byte[] data = new byte[sp.BytesToRead];
            //sp.Read(data, 0, data.Length);
            if(ModemMode == Mode.RecieveSMS)
            {
                ParseSMS(response);
            }
            else
            {
                MF.PrintMessage(response);
            }
            //MF.PrintMessage(data);
        }

        public void AtCommand(string command)
        {
            isRecieved = false;
            //sp.Open();
            sp.WriteLine(command + " \r\n");
            //waitForResponse.WaitOne();
            //Thread.Sleep(500);
            //sp.Close();
        }


        private void ParseSMS(string response)
        {
            string data = string.Empty;
            var lines = response.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //если в сообщении больше 1 строки
            if (lines.Length > 1 && lines[1]!="OK")
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("+CMGL:"))
                    {
                        var SMSdata = lines[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        int index;
                        //парсим номер сообщения
                        if (!int.TryParse(SMSdata[0].Replace("+CMGL: ", ""), out index))
                        {
                            index = 0;
                        }
                        var status = SMSdata[1];
                        var sender = SMSdata[2];
                        var datetime = SMSdata[3] + " " + SMSdata[4];
                        var msg = lines[++i];
                        RecievedSMS sms = new RecievedSMS(index, status, sender, datetime, msg);
                        Inbox.Add(sms);
                    }
                }
            }
            
        }

        /// <summary>
        /// Проверить входящие смс
        /// </summary>
        public void CheckInbox()
        {
            AtCommand("AT+CMGL=\"ALL\"");
        }
    }

    public enum Mode
    {
        Commands,
        RecieveSMS
    }
}
