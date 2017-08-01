using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TextStyle;
using System.Threading;

namespace WindowsFormsApplication2
{
    class ComPortManager
    {
        public string response;
        public List<SmsModemBlock> activeComs = new List<SmsModemBlock>();
        private ConcurrentQueue<SmsModemBlock> activeComsQueue = new ConcurrentQueue<SmsModemBlock>();

        public ComPortManager(MainForm mainForm)
        {
            GetModemPorts();
            //GetModemOperators();
            mainForm.loadComsButton.Enabled = true;
        }

        /// <summary>
        /// Проверка, является ли последовательный порт блоком модема
        /// </summary>
        /// <param name="name">имя порта</param>
        public void CheckSerial(string name)
        {
            SerialPort mySerialPort = new SerialPort(name);

            mySerialPort.BaudRate = 115200;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            if(!mySerialPort.IsOpen)
            mySerialPort.Open();
            mySerialPort.WriteLine("AT \r\n");
            System.Threading.Thread.Sleep(500);
            mySerialPort.Close();

            if (response.Contains("OK"))
            {
                activeComs.Add(new SmsModemBlock(name));
                //activeComsQueue.Enqueue(new SmsModemBlock(name));
            }
        }

        /// <summary>
        /// Проверка, является ли последовательный порт блоком модема (с использованием потоков)
        /// </summary>
        /// <param name="name">имя порта</param>
        public void CheckSerial(object n)
        {
            string name = (string)n;

            SmsModemBlock mySerialPort = new SmsModemBlock(name);
            mySerialPort.ModemMode = Mode.Check;

            if(!mySerialPort.sp.IsOpen)
            mySerialPort.sp.Open();
            mySerialPort.sp.WriteLine("AT \r\n");
            System.Threading.Thread.Sleep(500);
            mySerialPort.sp.Close();

            if (mySerialPort.isRecieved)
            {
                activeComsQueue.Enqueue(new SmsModemBlock(name));
                // завершаем текущий поток
                Thread.CurrentThread.Abort();
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            response = sp.ReadExisting();
            //Console.WriteLine("Data Received:");
            //Console.WriteLine(response.Trim());
        }

        /// <summary>
        /// Получить блок смс-модема по имени COM-порта
        /// </summary>
        /// <param name="name">Имя COM порта</param>
        public SmsModemBlock GetBlockByName(string name)
        {
            return activeComs.Find(com => com.Name == name);
        }

        /// <summary>
        /// Получить список портов смс-модема
        /// </summary>
        public void GetModemPorts()
        {
            activeComs.Clear();

            var ports = SerialPort.GetPortNames();

            List<Thread> _threadList = new List<Thread>();

            foreach (string port in ports)
            {
                Thread myThread = new Thread(new ParameterizedThreadStart(CheckSerial));
                myThread.Name = port;
                _threadList.Add(myThread);
                myThread.Start(port);

                //response = string.Empty;
                //CheckSerial(port);
            }
            // ждем всех
            foreach (Thread thread in _threadList)
            {
                thread.Join();
            }
            activeComs = activeComsQueue.ToList();

        }

        /// <summary>
        /// Получить список портов смс-модема (асинхронно)
        /// </summary>
        /// <returns></returns>
        public Task GetModemPortsAsync()
        {
            return Task.Run(() =>
            {
                activeComs.Clear();

                var ports = SerialPort.GetPortNames();

                foreach (string port in ports)
                {
                    response = string.Empty;
                    CheckSerial(port);
                }
                return;
            });
        }

        //public void GetModemOperators()
        //{
        //    foreach (var port in activeComs)
        //    {
        //        port.AtCommand("AT+COPS=3,0");
        //    }
        //    foreach (var port in activeComs)
        //    {
        //        port.AtCommand("AT+COPS?");
        //    }
        //    // ДОДЕЛАТЬ!!!
        //}

        private void QueueToList()
        {
            activeComs = activeComsQueue.ToList();
        }
    }
}
