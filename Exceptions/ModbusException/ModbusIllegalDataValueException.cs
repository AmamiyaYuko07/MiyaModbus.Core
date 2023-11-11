using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Exceptions.ModbusException
{
    public class ModbusIllegalDataValueException : ModbusException
    {
        public ModbusIllegalDataValueException(byte[] errorData)
            : base(errorData)
        {

        }

        public ModbusIllegalDataValueException(byte[] errorData, string message)
            : base(errorData, message)
        {

        }
    }
}
