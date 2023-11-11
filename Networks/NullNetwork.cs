using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Networks
{
    public class NullNetwork : BaseNetwork
    {
        public static NullNetwork Instance => new NullNetwork();

        public override bool IsConnected => true;

        private bool _connected;

        public override async Task ConnectAsync(CancellationToken cancellationToken)
        {
            _connected = true;
        }

        public override async Task<byte[]> ReciveAsync(CancellationToken cancellationToken)
        {
            return new byte[0];
        }

        public override async Task SendAsync(byte[] data, CancellationToken cancellationToken)
        {

        }

        public override async Task Start(double timeout = 5)
        {
            _connected = true;
        }

        public override async Task Stop()
        {
            _connected = false;
        }
    }
}
