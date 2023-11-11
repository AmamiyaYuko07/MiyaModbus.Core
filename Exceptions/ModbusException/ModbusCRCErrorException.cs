using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Exceptions.ModbusException
{
    public class ModbusCRCErrorException : ModbusException
    {
        public ModbusCRCErrorException(byte[] errorData)
            : base(errorData)
        {

        }

        public ModbusCRCErrorException(byte[] errorData, string message) 
            : base(errorData, message)
        {

        }
    }
}
