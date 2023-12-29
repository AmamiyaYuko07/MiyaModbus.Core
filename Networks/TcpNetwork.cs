using MiyaModbus.Core.Exceptions;
using System;
using System.Collections.Generic;
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

        public string Address { set; get; }

        public int Port { set; get; }

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
            if (string.IsNullOrWhiteSpace(Address)) throw new ArgumentNullException("param address is null");
            if (Port == 0) throw new Exception("param port is zero");
            client = new TcpClient();
            client.ConnectAsync(IPAddress.Parse(Address), Port).Wait(cancellationToken);
        }

        public override async Task<byte[]> ReciveAsync(CancellationToken cancellationToken)
        {
            if (IsConnected && client != null)
            {
                if (!((client.Client.Poll(200, SelectMode.SelectRead) && (client.Client.Available == 0)) || !client.Connected))
                {
                    if (client.Client.Poll(200, SelectMode.SelectRead) && client.Available > 0)
                    {
                        var ns = client.GetStream();
                        if (ns == null)
                        {
                            throw new NullReferenceException("Netstream is null");
                        }
                        List<byte> data = new List<byte>();
                        var buffer = new byte[1024];
                        int word;
                        while (client.Available > 0 && (word = await ns.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                        {
                            var temp = new byte[word];
                            Buffer.BlockCopy(buffer, 0, temp, 0, word);
                            data.AddRange(temp);
                        }
                        return data.ToArray();
                    }
                    return new byte[] { };
                }
                else
                {
                    client.Close();
                    client.Dispose();
                }
            }
            return new byte[] { };
        }

        public override async Task SendAsync(byte[] data, CancellationToken cancellationToken)
        {
            if (IsConnected && client != null)
            {
                if (client.Client.Poll(200, SelectMode.SelectWrite))
                {
                    var ns = client.GetStream();
                    await ns.WriteAsync(data, 0, data.Length, cancellationToken);
                    await ns.FlushAsync(cancellationToken);
                    return;
                }
                throw new StreamCannotWriteException();
            }
            throw new NetworkNotConnectException($"network {Address}:{Port} not connected");
        }

        public override async Task Start(double timeout = 3)
        {
            if (IsConnected) return;
            await ConnectAsync(new CancellationTokenSource(TimeSpan.FromSeconds(timeout)).Token);
        }

        public override async Task Stop()
        {
            await Task.Delay(0);
            if (client != null && client.Client != null)
            {
                if (client.Connected)
                    client.Close();
                client.Dispose();
            }
            client = null;
        }
    }
}
