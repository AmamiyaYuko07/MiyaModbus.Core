using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.ASCII
{
    public class ASCIIOnlyReadMessage : BaseMessage
    {
        public override byte[] Build()
        {
            return new byte[0];
        }
    }
}
