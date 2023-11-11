using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Exceptions
{
    public class DeviceTypeErrorException : Exception
    {
        public DeviceTypeErrorException(){ }

        public DeviceTypeErrorException(string message) : base(message) { }
    }
}
