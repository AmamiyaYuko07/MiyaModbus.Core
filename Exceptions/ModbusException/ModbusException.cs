using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Exceptions.ModbusException
{
    public class ModbusException : Exception
    {
        public byte[] ErrorData { set; get; }

        public ModbusException(byte[] errorData)
        {
            ErrorData = errorData;
        }

        public ModbusException(byte[] errorData, string message) : base(message)
        {
            ErrorData = errorData;
        }
    }
}
