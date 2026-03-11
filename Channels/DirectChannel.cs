using MiyaModbus.Core.Models;
using MiyaModbus.Core.Networks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Channels
{
    public class DirectChannel : IChannel
    {
        private readonly INetwork _network;
        private readonly double _sendTimeout;
        private readonly double _receiveTimeout;

        public DirectChannel(INetwork network, double sendTimeout = 1.0, double receiveTimeout = 2.0)
        {
            _network = network;
            _sendTimeout = sendTimeout;
            _receiveTimeout = receiveTimeout;
        }

        public string Code { get; set; }

        public bool IsRunning => _network.IsConnected;

        public bool IsConnected => _network.IsConnected;

        public INetwork Network => _network;

        public async Task<byte[]> SendMessageAsync(IMessage message)
        {
            var data = message.Build();

            using (var sendCts = new CancellationTokenSource(TimeSpan.FromSeconds(_sendTimeout)))
            {
                await Network.SendAsync(data, sendCts.Token);
            }

            using (var receiveCts = new CancellationTokenSource(TimeSpan.FromSeconds(_receiveTimeout)))
            {
                var result = await Network.ReciveAsync(receiveCts.Token);
                return result;
            }
        }

        public async Task Start()
        {
            await Network.Start();
        }

        public async Task Stop()
        {
            await Network.Stop();
        }
    }
}
