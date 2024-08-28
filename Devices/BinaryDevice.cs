using MiyaModbus.Core.Channels;
using MiyaModbus.Core.Models.ASCII;
using MiyaModbus.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MiyaModbus.Core.Models.BInary;
using System.Linq;

namespace MiyaModbus.Core.Devices
{
    /// <summary>
    /// 二进制设备
    /// 不对数据做任何处理
    /// </summary>
    public class BinaryDevice : BaseDevice
    {
        private CancellationTokenSource cancellationTokenSource = null;
        private readonly List<byte> buffer = new List<byte>();

        public BinaryDevice(IChannel channel)
            : base(channel)
        {

        }

        public BinaryDevice(IChannel channel, Action<DeviceOptions> option)
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
            if (message.GetType() == typeof(BinaryOnlyWriteMessage))
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
                    }
                }
            }
            else if (message.GetType() == typeof(BinaryOnlyReadMessage))
            {
                //只读不写
                while (IsRunning && tryCount > 0)
                {
                    try
                    {
                        var msg = message as BinaryOnlyReadMessage;
                        var retData = await Channel.Network.ReciveAsync(new CancellationTokenSource(Options.ReciveTimeout).Token);

                        if (msg.Count == 0)
                        {
                            IResult result = new BinaryResult(options);
                            result.SetData(retData);
                            return result;
                        }
                        else
                        {
                            if (retData == null || retData.Length == 0)
                            {
                                tryCount--;
                                continue;
                            }
                            buffer.AddRange(retData);
                            if (buffer.Count >= msg.Count)
                            {
                                IResult result = new BinaryResult(options);
                                result.SetData(buffer.Take(msg.Count).ToArray());
                                buffer.RemoveRange(0, msg.Count);
                                return result;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        tryCount--;
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
                        IResult result = new BinaryResult(options);
                        result.SetData(retData);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tryCount--;
                    }
                }
            }
            return new FailedResult(options);
        }
    }
}
