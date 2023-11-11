using MiyaModbus.Core.Models;
using MiyaModbus.Core.Networks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Channels
{
    public class NullChannel : IChannel
    {
        public static NullChannel Instance => new NullChannel();
        public string Code { get; set; } = "Unknow";

        private bool _isRunning = false;
        public bool IsRunning => _isRunning;

        public bool IsConnected => Network.IsConnected;

        public INetwork Network => NullNetwork.Instance;

        public async Task<byte[]> SendMessageAsync(IMessage message)
        {
            return new byte[0];
        }

        public async Task Start()
        {
            _isRunning = true;
        }

        public async Task Stop()
        {
            _isRunning = false;
        }
    }
}
