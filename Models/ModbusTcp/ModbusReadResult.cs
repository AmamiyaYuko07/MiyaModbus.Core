using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusTcp
{
    public class ModbusReadResult : BaseResult
    {
        public ModbusReadResult(ResultOption resultOption) 
            : base(resultOption)
        {

        }

        public override void SetData(byte[] data)
        {
            var len = data[8];
            var totalLen = 9 + len;
            var bytes = data.SubBytes(9, totalLen);
            Result = bytes;
        }
    }
}
