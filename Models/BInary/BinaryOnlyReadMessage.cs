using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.BInary
{
    public class BinaryOnlyReadMessage : BaseMessage
    {
        private readonly int _count = 0;

        public BinaryOnlyReadMessage(int count)
        {
            _count = count;
        }

        public int Count => _count;

        public override byte[] Build()
        {
            return null;
        }
    }
}
