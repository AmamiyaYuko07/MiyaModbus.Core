using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.IO;

namespace MiyaModbus.Core.Networks
{
    public class SerialNetwork : BaseNetwork
    {
        private SerialPort serialPort = null;
        private readonly object _serialLock = new object();

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
                try
                {
                    serialPort.Dispose();
                }
                catch { }
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
            await Task.Yield();

            if (serialPort == null)
            {
                throw new NullReferenceException("serial is null");
            }

            // 快速检查 token
            if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException(cancellationToken);

            // 在串口操作上加锁，避免并发调用导致的竞态
            lock (_serialLock)
            {
                try
                {
                    if (!serialPort.IsOpen)
                    {
                        serialPort.Open();
                    }

                    // 最小化在 Clear/Discard 时的异常未捕获：先检查 IsOpen，再调用
                    try
                    {
                        if (serialPort.IsOpen)
                        {
                            serialPort.DiscardInBuffer();
                            serialPort.DiscardOutBuffer();
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        // 端口状态在操作过程中发生变化（可能被关闭），尝试重新打开或继续让上层重试
                        if (!serialPort.IsOpen)
                        {
                            try { serialPort.Open(); } catch { /* ignore, let write fail below */ }
                        }
                    }
                    catch (IOException)
                    {
                        serialPort.Close();
                        serialPort.Dispose();
                        serialPort = null;
                        // 底层 I/O 被中止（例如设备拔出或驱动问题），将异常抛出给上层以触发重连/重试逻辑
                        throw;
                    }

                    // 写入数据（SerialPort.Write 本身也可能抛出）
                    serialPort.Write(data, 0, data.Length);
                    return;
                }
                catch (OperationCanceledException)
                {
                    // 传入的 cancellationToken 取消，向上层报告
                    throw;
                }
                // 选择性捕获并重新抛出常见串口异常，便于上层按策略重试或重建连接
                catch (UnauthorizedAccessException)
                {
                    throw;
                }
                catch (IOException)
                {
                    throw;
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
            }
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
                while (serialPort.BytesToRead == 0 && !cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(1);
                }

                while (serialPort.BytesToRead > 0)
                {
                    byte[] array = new byte[serialPort.BytesToRead];
                    serialPort.Read(array, 0, array.Length);
                    data.AddRange(array);
                    await Task.Delay(20);

                    //每隔20毫秒判断一次是否有数据 但需要经过60毫秒才确定数据传输完成
                    if (serialPort.BytesToRead > 0)
                    {
                        array = new byte[serialPort.BytesToRead];
                        serialPort.Read(array, 0, array.Length);
                        data.AddRange(array);
                        await Task.Delay(20);
                        continue;
                    }
                    await Task.Delay(20);

                    if (serialPort.BytesToRead > 0)
                    {
                        array = new byte[serialPort.BytesToRead];
                        serialPort.Read(array, 0, array.Length);
                        data.AddRange(array);
                        await Task.Delay(20);
                        continue;
                    }
                    await Task.Delay(20);
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
