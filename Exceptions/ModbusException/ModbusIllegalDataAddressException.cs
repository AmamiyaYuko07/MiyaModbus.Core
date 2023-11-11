using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Exceptions.ModbusException
{
    public class ModbusIllegalDataAddressException : ModbusException
    {
        public ModbusIllegalDataAddressException(byte[] errorData)
            : base(errorData)
        {

        }

        public ModbusIllegalDataAddressException(byte[] errorData, string message)
            : base(errorData, message)
        {

        }
    }
}
