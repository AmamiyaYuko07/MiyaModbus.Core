using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusTcp
{
    public class ModbusWriteResult : BaseResult
    {
        public ModbusWriteResult(ResultOption resultOption) 
            : base(resultOption)
        {

        }
    }
}
