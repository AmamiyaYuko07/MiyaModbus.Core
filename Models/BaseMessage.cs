using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models
{
    public abstract class BaseMessage : IMessage
    {
        /// <summary>
        /// 构建字节数组
        /// </summary>
        /// <returns></returns>
        public abstract byte[] Build();
    }
}
