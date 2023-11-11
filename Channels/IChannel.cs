using MiyaModbus.Core.Models;
using MiyaModbus.Core.Networks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Channels
{
    public interface IChannel
    {
        /// <summary>
        /// 网络通道编码
        /// </summary>
        string Code { set; get; }

        /// <summary>
        /// 运行状态
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 网络连接状态
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 通信网络
        /// </summary>
        INetwork Network { get; }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        Task Start();

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        Task Stop();

        /// <summary>
        /// 推送消息到通道并等待返回结果
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<byte[]> SendMessageAsync(IMessage message);
    }
}
