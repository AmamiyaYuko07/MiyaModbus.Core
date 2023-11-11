using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Networks
{
    public abstract class BaseNetwork : INetwork
    {
        public string Code { get; set; } = "Unknow";

        public abstract bool IsConnected { get; }

        public abstract Task ConnectAsync(CancellationToken cancellationToken);

        public abstract Task<byte[]> ReciveAsync(CancellationToken cancellationToken);

        public abstract Task SendAsync(byte[] data, CancellationToken cancellationToken);

        public abstract Task Start(double timeout = 5);

        public abstract Task Stop();
    }
}
