using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MiyaModbus.Core.Networks;
using MiyaModbus.Core.Models;

namespace MiyaModbus.Core.Channels
{
    public class DefaultChannel : IChannel
    {
        private readonly ChannelOption _options;
        private readonly ConcurrentQueue<MessageContainer> _messages = new ConcurrentQueue<MessageContainer>();

        public DefaultChannel(INetwork network)
        {
            _options = new ChannelOption();
            Code = "";
            Network = network;
        }

        public DefaultChannel(INetwork network, Action<ChannelOption> settings)
        {
            Network = network;
            _options = new ChannelOption();
            settings(_options);
            Code = _options.Code ?? "";
        }

        /// <summary>
        /// 网络标识
        /// </summary>
        public string Code { set; get; }

        /// <summary>
        /// 网络通信服务
        /// </summary>
        public INetwork Network { get; }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning { private set; get; }

        /// <summary>
        /// 网络是否已连接
        /// </summary>
        public bool IsConnected => Network?.IsConnected ?? false;

        /// <summary>
        /// 通道内全局取消Token
        /// </summary>
        private CancellationTokenSource CancellationTokenSource = null;

        /// <summary>
        /// 启动通道
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            if (CancellationTokenSource != null)
            {
                CancellationTokenSource.Dispose();
                CancellationTokenSource = null;
            }
            CancellationTokenSource = new CancellationTokenSource();
            try
            {
                var timeout = _options.ConnectTimeout;
                var token = CancellationTokenSource.CreateLinkedTokenSource(CancellationTokenSource.Token, new CancellationTokenSource(timeout).Token);
                await Network.ConnectAsync(token.Token);
            }
            catch { }
            ChannelProcess();
        }

        /// <summary>
        /// 停止通道
        /// </summary>
        /// <returns></returns>
        public async Task Stop()
        {
            if (!IsRunning) return;
            IsRunning = false;
            CancellationTokenSource.Cancel();
            await Network.Stop();
        }

        /// <summary>
        /// 发送并等待返回数据
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async Task<byte[]> SendMessageAsync(IMessage msg)
        {
            var message = new MessageContainer(msg.Build());
            _messages.Enqueue(message);
            var timeToken = new CancellationTokenSource(_options.MsgWaitTime).Token;
            while (IsRunning
                && !timeToken.IsCancellationRequested
                && CancellationTokenSource != null
                && !CancellationTokenSource.IsCancellationRequested
                && !message.HaveResult)
            {
                await Task.Delay(_options.StepWaitTime);
            }
            return message.Result;
        }

        /// <summary>
        /// 消息处理线程
        /// </summary>
        private void ChannelProcess()
        {
            Task.Run(async () =>
            {
                while (IsRunning
                    && CancellationTokenSource != null
                    && !CancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        if (!IsConnected)
                        {
                            var timeout = _options.ConnectTimeout;
                            var token = CancellationTokenSource.CreateLinkedTokenSource(CancellationTokenSource.Token, new CancellationTokenSource(timeout).Token);
                            await Network.ConnectAsync(token.Token);
                        }
                        if (IsConnected && _messages.TryDequeue(out var message))
                        {
                            if (message.Data == null || message.HaveResult)
                            {
                                message.HaveResult = true;
                                continue;
                            }
                            byte[] result = null;
                            try
                            {
                                if (message.Data.Length > 0)
                                {
                                    //发送数据
                                    await Network.SendAsync(message.Data, CancellationTokenSource.Token);
                                }

                                //等待设备处理数据时间
                                await Task.Delay(_options.StepWaitTime);

                                //尝试接收数据
                                result = await Network.ReciveAsync(CancellationTokenSource.Token);
                            }
                            catch
                            {
                                await Task.Delay(100);
                            }
                            if (result == null || result.Length == 0)
                            {
                                message.TryCount++;
                                if (message.TryCount <= _options.ErrorRetryCount)
                                {
                                    _messages.Enqueue(message);
                                    continue;
                                }
                                message.HaveResult = true;
                            }
                            else
                            {
                                message.Result = result;
                                message.HaveResult = true;
                            }
                        }
                        else
                        {
                            await Task.Delay(_options.StepWaitTime);
                        }
                    }
                    catch (Exception ex)
                    {
                        await Task.Delay(300);
                    }
                }
            }, CancellationTokenSource.Token);
        }
    }
}
