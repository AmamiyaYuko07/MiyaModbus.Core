using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Exceptions.ModbusException
{
    public class ModbusDeviceFailureException : ModbusException
    {
        public ModbusDeviceFailureException(byte[] errorData) 
            : base(errorData)
        {

        }

        public ModbusDeviceFailureException(byte[] errorData,string message) 
            : base(errorData,message) 
        {
            
        }
    }
}
