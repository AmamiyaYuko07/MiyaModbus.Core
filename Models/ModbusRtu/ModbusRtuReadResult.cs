using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusRtu
{
    public class ModbusRtuReadResult : BaseResult
    {
        public ModbusRtuReadResult(ResultOption resultOption) 
            : base(resultOption)
        {

        }

        public override void SetData(byte[] data)
        {
            var len = data[2];
            var totalLen = 3 + len;
            var bytes = data.SubBytes(3,totalLen);
            Result = bytes;
        }
    }
}
