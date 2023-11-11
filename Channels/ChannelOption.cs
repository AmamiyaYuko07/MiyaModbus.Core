using MiyaModbus.Core.Networks;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Channels
{
    public class ChannelOption
    {
        /// <summary>
        /// 通道代码
        /// </summary>
        public string Code { set; get; }

        /// <summary>
        /// 发送消息后等待结果超时时间（默认5秒）
        /// </summary>
        public TimeSpan MsgWaitTime { set; get; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// 每隔多长时间检查一次是否有返回值（默认0.1秒）
        /// </summary>
        public TimeSpan StepWaitTime { set; get; } = TimeSpan.FromSeconds(0.1);

        /// <summary>
        /// 错误重试次数（默认5次）
        /// </summary>
        public int ErrorRetryCount { set; get; } = 5;
    }
}
