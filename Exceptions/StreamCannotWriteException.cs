using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Exceptions
{
    public class StreamCannotWriteException : Exception
    {
        public StreamCannotWriteException() { }

        public StreamCannotWriteException(string message) : base(message) { }
    }
}
