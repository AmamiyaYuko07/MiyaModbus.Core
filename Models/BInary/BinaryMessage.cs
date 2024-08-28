using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.BInary
{
    public class BinaryMessage : BaseMessage
    {
        private readonly byte[] _data;
        private readonly int _count;

        public BinaryMessage(byte[] data, int count)
        {
            _data = data;
            _count = count;
        }
        public int Count => _count;

        public override byte[] Build()
        {
            return _data;
        }
    }
}
