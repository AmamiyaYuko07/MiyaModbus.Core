using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.ASCII
{
    public class ASCIIWriteStringMessage : BaseMessage
    {
        private readonly string _message;
        public ASCIIWriteStringMessage(string message)
        {
            _message = message;
        }

        public override byte[] Build()
        {
            return Encoding.UTF8.GetBytes(_message);
        }
    }
}
