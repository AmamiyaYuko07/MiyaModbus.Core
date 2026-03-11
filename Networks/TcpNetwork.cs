using MiyaModbus.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Networks
{
    public class TcpNetwork : BaseNetwork
    {
        private TcpClient client = null;

        public override bool IsConnected => client?.Connected ?? false;

        public string Address { get; set; }

        public int Port { get; set; }

        public TcpNetwork(string address, int port)
        {
            Address = address;
            Port = port;
            client = new TcpClient();
        }

        public override async Task ConnectAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            if (client != null)
            {
                client.Dispose();
                client = null;
            }

            if (string.IsNullOrWhiteSpace(Address))
            {
                throw new ArgumentNullException("param address is null");
            }

            if (Port == 0)
            {
                throw new Exception("param port is zero");
            }

            client = new TcpClient();
            client.ConnectAsync(IPAddress.Parse(Address), Port).Wait(cancellationToken);
        }

        public override async Task<byte[]> ReciveAsync(CancellationToken cancellationToken)
        {
            if (IsConnected && client != null)
            {
                NetworkStream ns = client.GetStream();
                List<byte> datas = new List<byte>();
                bool dataReceived = false;
                int idleCount = 0;
                const int maxIdleChecks = 3;

                while (!cancellationToken.IsCancellationRequested)
                {
                    if (!client.Connected)
                    {
                        client.Close();
                        client.Dispose();
                        break;
                    }

                    if (client.Available > 0)
                    {
                        byte[] buffer = new byte[1024];
                        int word = await ns.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                        if (word > 0)
                        {
                            byte[] temp = new byte[word];
                            Buffer.BlockCopy(buffer, 0, temp, 0, word);
                            datas.AddRange(temp);
                            dataReceived = true;
                            idleCount = 0;
                        }
                    }
                    else if (dataReceived)
                    {
                        await Task.Delay(20, cancellationToken);
                        idleCount++;
                        if (idleCount >= maxIdleChecks)
                        {
                            return datas.ToArray();
                        }
                    }
                    else
                    {
                        await Task.Delay(10, cancellationToken);
                    }
                }

                if (datas.Count > 0)
                {
                    return datas.ToArray();
                }
            }
            return new byte[0];
        }

        public override async Task SendAsync(byte[] data, CancellationToken cancellationToken)
        {
            if (!IsConnected || client == null)
            {
                throw new NetworkNotConnectException($"network {Address}:{Port} not connected");
            }

            try
            {
                NetworkStream ns = client.GetStream();
                if (!ns.CanWrite)
                {
                    throw new StreamCannotWriteException();
                }

                await ns.WriteAsync(data, 0, data.Length, cancellationToken);
                await ns.FlushAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex) when (ex is IOException || ex is ObjectDisposedException)
            {
                throw new StreamCannotWriteException();
            }
        }

        public override async Task Start(double timeout = 3.0)
        {
            if (!IsConnected)
            {
                await ConnectAsync(new CancellationTokenSource(TimeSpan.FromSeconds(timeout)).Token);
            }
        }

        public override async Task Stop()
        {
            await Task.Delay(0);
            if (client != null && client.Client != null)
            {
                if (client.Connected)
                {
                    client.Close();
                }

                client.Dispose();
            }

            client = null;
        }
    }
}
