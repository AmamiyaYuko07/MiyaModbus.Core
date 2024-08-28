using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MiyaModbus.Core.Networks
{
    /// <summary>
    /// 此类不管有没有读取到数据
    /// 都会直接返回
    /// </summary>
    public class SerialRealTimeNetwork : BaseNetwork
    {
        private SerialPort serialPort = null;

        public string PortName { set; get; }

        public int BaudRate { set; get; }

        public int DataBits { set; get; }

        public StopBits StopBits { set; get; }

        public Parity Parity { set; get; }

        public override bool IsConnected => serialPort?.IsOpen ?? false;

        public SerialRealTimeNetwork(string portName,
            int baudRate,
            int dataBits,
            StopBits stopBits,
            Parity parity)
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
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();
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
                await Task.Delay(0);
                byte[] array = new byte[serialPort.BytesToRead];
                serialPort.Read(array, 0, array.Length);
                data.AddRange(array);
                return array;
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
