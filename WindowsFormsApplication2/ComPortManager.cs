using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TextStyle;

namespace WindowsFormsApplication2
{
    class ComPortManager
    {
        public string response;
        public List<SmsModemBlock> activeComs = new List<SmsModemBlock>();

        public ComPortManager(MainForm mainForm)
        {
            var ports = SerialPort.GetPortNames();
            //Console.WriteLine("All com ports:" + ports.Length);
            //Console.WriteLine();
            foreach (string port in ports)
            {
                response = string.Empty;
                CheckSerial(port);
            }
            mainForm.loadComsButton.Enabled = true;
            //Console.WriteLine("SIM bank com ports:" + activeComs.Count);
            //Console.ReadLine();
        }

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

            mySerialPort.Open();
            mySerialPort.WriteLine("AT \r\n");
            System.Threading.Thread.Sleep(500);
            mySerialPort.Close();

            if (response.Contains("OK"))
            {
                //GreenText("{0} is a SIM", name);
                activeComs.Add(new SmsModemBlock(name));
            }
            else
            {
                //RedText("{0} is NOT a SIM", name);
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            response = sp.ReadExisting();
            //Console.WriteLine("Data Received:");
            //Console.WriteLine(response.Trim());
        }

        public SmsModemBlock GetBlockByName(string name)
        {
            return activeComs.Find(com => com.Name == name);
        }
    }
}
