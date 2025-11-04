using MiyaModbus.Core.Channels;
using MiyaModbus.Core.Exceptions.ModbusException;
using MiyaModbus.Core.Models;
using MiyaModbus.Core.Models.ASCII;
using MiyaModbus.Core.Models.ModbusRtu;
using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Devices
{
    public class ASCIIDevice : BaseDevice
    {
        private CancellationTokenSource cancellationTokenSource = null;
        public ASCIIDevice(IChannel channel)
            : base(channel)
        {

        }

        public ASCIIDevice(IChannel channel, Action<DeviceOptions> option)
            : base(channel, option)
        {

        }

        public override async Task StartAsync()
        {
            await base.StartAsync();
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }
            cancellationTokenSource = new CancellationTokenSource();
        }

        public override async Task StopAsync()
        {
            await base.StopAsync();
            if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Cancel();
            }
        }

        public override async Task<IResult> SendMessageAsync(IMessage message)
        {
            if (!IsRunning)
            {
                throw new Exception($"Device {Code} not running！");
            }
            var tryCount = Options.DeviceRetryCount;
            var options = new ResultOption
            {
                Device = this,
                Data = message.Build()
            };
            if (message.GetType() == typeof(ASCIIOnlyWriteMessage))
            {
                //只写不读
                while (IsRunning && tryCount > 0)
                {
                    try
                    {
                        await Channel.Network.SendAsync(message.Build(), cancellationTokenSource.Token);
                        return new ASCIIStringResult(options);
                    }
                    catch (Exception ex)
                    {
                        tryCount--;
                        if (!IsRunning)
                        {
                            await StartAsync();
                        }
                    }
                }
            }
            else if (message.GetType() == typeof(ASCIIOnlyReadMessage))
            {
                //只读不写
                while (IsRunning && tryCount > 0)
                {
                    try
                    {
                        var retData = await Channel.Network.ReciveAsync(new CancellationTokenSource(Options.ReciveTimeout).Token);
                        if (retData == null || retData.Length == 0)
                        {
                            tryCount--;
                            continue;
                        }
                        IResult result = new ASCIIStringResult(options);
                        result.SetData(retData);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tryCount--;
                        if (IsRunning)
                        {
                            await StartAsync();
                        }
                    }
                }
            }
            else
            {
                while (IsRunning && tryCount > 0)
                {
                    try
                    {
                        await Channel.Network.SendAsync(message.Build(), cancellationTokenSource.Token);
                        var token = new CancellationTokenSource(Options.ReciveTimeout).Token;
                        byte[] retData = null;
                        while (!token.IsCancellationRequested)
                        {
                            retData = await Channel.Network.ReciveAsync(token);
                            if (retData != null && retData.Length > 0) break;
                        }
                        if (retData == null || retData.Length == 0)
                        {
                            tryCount--;
                            continue;
                        }
                        IResult result = new ASCIIStringResult(options);
                        result.SetData(retData);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tryCount--;
                        if (IsRunning)
                        {
                            await StartAsync();
                        }
                    }
                }
            }
            return new FailedResult(options);
        }
    }
}
