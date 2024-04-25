using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;

namespace MiyaModbus.Core.Networks
{
    public class SerialNetwork : BaseNetwork
    {
        private SerialPort serialPort = null;

        public string PortName { set; get; }

        public int BaudRate { set; get; }

        public int DataBits { set; get; }

        public StopBits StopBits { set; get; }

        public Parity Parity { set; get; }

        public override bool IsConnected => serialPort?.IsOpen ?? false;

        public SerialNetwork(string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity)
        {
            PortName = portName;
            BaudRate = baudRate;
            DataBits = dataBits;
            StopBits = stopBits;
            Parity = parity;
        }

        public override async Task ConnectAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            if (serialPort != null)
            {
                serialPort.Dispose();
                serialPort = null;
            }
            serialPort = new SerialPort();
            serialPort.PortName = PortName;
            serialPort.BaudRate = BaudRate;
            serialPort.DataBits = DataBits;
            serialPort.StopBits = StopBits;
            serialPort.Parity = Parity;
            serialPort.Open();
        }

        public override async Task SendAsync(byte[] data, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            if (serialPort != null)
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
                serialPort.Write(data, 0, data.Length);
                return;
            }
            throw new NullReferenceException("serial is null");
        }

        public override async Task<byte[]> ReciveAsync(CancellationToken cancellationToken)
        {
            if (serialPort != null)
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
                List<byte> data = new List<byte>();
                var count = 3;
                while (count > 0)
                {
                    while (serialPort.BytesToRead > 0)
                    {
                        var temp = new byte[serialPort.BytesToRead];
                        serialPort.Read(temp, 0, temp.Length);
                        data.AddRange(temp);
                        await Task.Delay(10);
                    }
                    if (data.Count > 0) return data.ToArray();
                    count--;
                    await Task.Delay(10);
                }
                return data.ToArray();
            }
            throw new NullReferenceException("serial is null");
        }

        public override async Task Start(double timeout = 5)
        {
            await Task.Delay(0);
            if (IsConnected) return;
            if (serialPort != null)
            {
                serialPort.Open();
            }
            else
            {
                serialPort = new SerialPort();
                serialPort.PortName = PortName;
                serialPort.BaudRate = BaudRate;
                serialPort.DataBits = DataBits;
                serialPort.StopBits = (StopBits)StopBits;
                serialPort.Parity = (Parity)Parity;
                serialPort.Open();
            }
        }

        public override async Task Stop()
        {
            await Task.Delay(0);
            if (!IsConnected) return;
            if (serialPort != null)
            {
                serialPort.Close();
            }
        }
    }
}
