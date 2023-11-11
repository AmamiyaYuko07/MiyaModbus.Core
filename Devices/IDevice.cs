using MiyaModbus.Core.Channels;
using MiyaModbus.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Devices
{
    public interface IDevice
    {
        /// <summary>
        /// 编码
        /// </summary>
        string Code { set; get; }

        /// <summary>
        /// 设备配置
        /// </summary>
        DeviceOptions Options { get; }

        /// <summary>
        /// 通信通道
        /// </summary>
        IChannel Channel { set; get; }

        /// <summary>
        /// 是否连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 是否运行
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<IResult> SendMessageAsync(IMessage message);

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        Task StartAsync();

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
    }
}
