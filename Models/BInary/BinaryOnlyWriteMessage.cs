using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.BInary
{
    public class BinaryOnlyWriteMessage : BaseMessage
    {
        private readonly byte[] _data;

        public BinaryOnlyWriteMessage(byte[] data)
        {
            _data = data;
        }

        public override byte[] Build()
        {
            return _data;
        }
    }
}
