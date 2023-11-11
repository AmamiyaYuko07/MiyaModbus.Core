using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Exceptions
{
    public class NetworkNotConnectException : Exception
    {
        public NetworkNotConnectException() { }

        public NetworkNotConnectException(string message) : base(message) { }
    }
}
