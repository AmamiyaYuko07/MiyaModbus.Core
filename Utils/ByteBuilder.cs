using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Utils
{
    public class ByteBuilder
    {
        private readonly List<byte> bytes = new List<byte>();

        public void Append(byte b)
        {
            bytes.Add(b);
        }

        public void Append(byte[] bytes)
        {
            this.bytes.AddRange(bytes);
        }

        public void AppendInt16(short number)
        {
            var bs = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bs);
            }
            Append(bs);
        }

        public void AppendUInt16(ushort number)
        {
            var bs = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bs);
            }
            Append(bs);
        }

        public void AppendInt32(int number)
        {
            var bs = BitConverter.GetBytes(number);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bs);
            }
            Append(bs);
        }

        public byte[] ToArray()
        {
            return bytes.ToArray();
        }
    }
}
