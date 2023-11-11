using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Exceptions.ModbusException
{
    public class ModbusValueNotValidException : ModbusException
    {
        public ModbusValueNotValidException() : base(new byte[0]) { }

        public ModbusValueNotValidException(byte[] errorData) : base(errorData)
        {

        }

        public ModbusValueNotValidException(byte[] errorData, string message) : base(errorData, message) { }
    }
}
