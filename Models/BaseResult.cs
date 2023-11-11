using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models
{
    public class BaseResult : IResult
    {
        public BaseResult(ResultOption resultOption)
        {
            Option = resultOption;
            Result = new byte[0];
        }

        public byte[] Result { protected set; get; }

        public virtual bool IsSuccess { protected set; get; } = true;

        public ResultOption Option { get; }

        public virtual void SetData(byte[] data)
        {
            Result = data;
        }
    }
}
