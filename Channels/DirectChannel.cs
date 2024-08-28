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
        public DirectChannel(INetwork network)
        {
            _network = network;
        }

        public string Code { get; set; }

        public bool IsRunning => _network.IsConnected;

        public bool IsConnected => _network.IsConnected;

        public INetwork Network => _network;

        public async Task<byte[]> SendMessageAsync(IMessage message)
        {
            var data = message.Build();
            CancellationTokenSource cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(3));
            await Network.SendAsync(data, cancellationToken.Token);
            var result = await Network.ReciveAsync(cancellationToken.Token);
            return result;
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
