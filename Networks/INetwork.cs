using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MiyaModbus.Core.Networks
{
    public interface INetwork
    {
        /// <summary>
        /// 网络编码
        /// </summary>
        string Code { set; get; }

        /// <summary>
        /// 是否已连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 连接网络
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ConnectAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SendAsync(byte[] data, CancellationToken cancellationToken);

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<byte[]> ReciveAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        Task Start(double timeout = 5);

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        Task Stop();
    }
}
