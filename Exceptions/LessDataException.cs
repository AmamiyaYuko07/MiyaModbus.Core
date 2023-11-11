using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Exceptions
{
    public class LessDataException : Exception
    {
        public LessDataException(byte[] bytes)
        {
            Bytes = bytes;
        }

        public LessDataException(byte[] bytes, string message)
            : base(message)
        {
            Bytes = bytes;
        }

        public byte[] Bytes { set; get; }
    }
}
