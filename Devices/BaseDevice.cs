using MiyaModbus.Core.Channels;
using MiyaModbus.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Devices
{
    public abstract class BaseDevice : IDevice
    {

        public BaseDevice(IChannel channel, Action<DeviceOptions> option)
        {
            Channel = channel;
            var options = new DeviceOptions();
            option(options);
            Options = options;
        }

        public BaseDevice(IChannel channel)
        {
            Channel = channel;
            var options = new DeviceOptions();
            Options = options;
        }

        public DeviceOptions Options { get; }

        public string Code { get; set; } = "Unknow";

        public IChannel Channel { get; set; } = NullChannel.Instance;

        public bool IsConnected => Channel.IsConnected;

        public bool IsRunning => Channel.IsRunning;

        public abstract Task<IResult> SendMessageAsync(IMessage message);

        public virtual async Task StartAsync()
        {
            if (!Channel.IsRunning)
            {
                await Channel.Start();
            }
        }

        public virtual async Task StopAsync()
        {
            if (Channel.IsRunning)
            {
                await Channel.Stop();
            }
        }
    }
}
