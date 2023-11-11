using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Channels
{
    /// <summary>
    /// 消息包装器
    /// </summary>
    public class MessageContainer
    {
        public MessageContainer(byte[] data)
        {
            Data = data;
        }

        /// <summary>
        /// 发送的数据
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 是否已有返回结果
        /// </summary>
        public bool HaveResult { set; get; }

        /// <summary>
        /// 发送失败重试次数
        /// </summary>
        public int TryCount { set; get; }

        /// <summary>
        /// 返回的结果
        /// </summary>
        public byte[] Result { set; get; } = new byte[0];
    }
}
