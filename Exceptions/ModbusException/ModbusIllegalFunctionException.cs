using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Exceptions.ModbusException
{
    public class ModbusIllegalFunctionException : ModbusException
    {
        public ModbusIllegalFunctionException(byte[] errorData)
            : base(errorData)
        {

        }

        public ModbusIllegalFunctionException(byte[] errorData, string message)
            : base(errorData, message)
        {

        }
    }
}
