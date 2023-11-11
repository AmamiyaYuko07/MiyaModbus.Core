using MiyaModbus.Core.Enums;
using MiyaModbus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiyaModbus.Core.Utils
{
    public static class ByteExtend
    {
        public static short ToInt16(this byte[] bytes)
        {
            var temp = new byte[2];
            Buffer.BlockCopy(bytes, 0, temp, 0, 2);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(temp);
            }
            return BitConverter.ToInt16(temp, 0);
        }

        public static short ToInt16(this byte[] bytes, int index)
        {
            var temp = new byte[2];
            Buffer.BlockCopy(bytes, index, temp, 0, 2);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(temp);
            }
            return BitConverter.ToInt16(bytes, 0);
        }

        public static int ToInt32(this byte[] bytes)
        {
            var temp = new byte[4];
            Buffer.BlockCopy(bytes, 0, temp, 0, 4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(temp);
            }
            return BitConverter.ToInt32(temp, 0);
        }

        public static int ToInt32(this byte[] bytes, int index)
        {
            var temp = new byte[4];
            Buffer.BlockCopy(bytes, index, temp, 0, 4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(temp);
            }
            return BitConverter.ToInt16(temp, 0);
        }

        public static bool BytesEquals(this byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (var i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }

        public static byte[] GetCRC16(this byte[] data)
        {
            int len = data.Length;
            if (len > 0)
            {
                ushort crc = 0xFFFF;

                for (int i = 0; i < len; i++)
                {
                    crc = (ushort)(crc ^ (data[i]));
                    for (int j = 0; j < 8; j++)
                    {
                        crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
                    }
                }
                byte hi = (byte)((crc & 0xFF00) >> 8);
                byte lo = (byte)(crc & 0x00FF);

                return new byte[] { lo, hi };
            }
            return new byte[] { 0, 0 };
        }

        public static byte[] SubBytes(this byte[] bytes, int start, int end)
        {
            int len = end - start;
            var temp = new byte[len];
            Buffer.BlockCopy(bytes, start, temp, 0, len);
            return temp;
        }

        public static byte[] SubBytes(this byte[] bytes, int start)
        {
            int len = bytes.Length - start;
            var temp = new byte[len];
            Buffer.BlockCopy(bytes, start, temp, 0, len);
            return temp;
        }

        public static byte[] BytesOrder(this byte[] bytes, ByteOrder order)
        {
            var data = bytes.Take(4).Reverse().ToArray();
            var ret = new byte[4];
            switch (order)
            {
                case ByteOrder.CDBA:
                    ret[0] = data[2];
                    ret[1] = data[3];
                    ret[2] = data[0];
                    ret[3] = data[1];
                    break;
                case ByteOrder.BADC:
                    ret[0] = data[1];
                    ret[1] = data[0];
                    ret[2] = data[3];
                    ret[3] = data[2];
                    break;
                case ByteOrder.DCBA:
                    ret[0] = data[3];
                    ret[1] = data[2];
                    ret[2] = data[1];
                    ret[3] = data[0];
                    break;
                case ByteOrder.ABCD:
                default:
                    ret = data;
                    break;
            }
            return ret;
        }

        public static byte[] BytesOrder(this byte[] bytes, LongByteOrder order)
        {
            var data = bytes.Take(8).Reverse().ToArray();
            var ret = new byte[8];
            switch (order)
            {
                case LongByteOrder.GHEFCDAB:
                    ret[0] = data[6];
                    ret[1] = data[7];
                    ret[2] = data[4];
                    ret[3] = data[5];
                    ret[4] = data[2];
                    ret[5] = data[3];
                    ret[6] = data[0];
                    ret[7] = data[1];
                    break;
                case LongByteOrder.HGFEDCBA:
                    ret[0] = data[7];
                    ret[1] = data[6];
                    ret[2] = data[5];
                    ret[3] = data[4];
                    ret[4] = data[3];
                    ret[5] = data[2];
                    ret[6] = data[1];
                    ret[7] = data[0];
                    break;
                case LongByteOrder.BADCFEHG:

                    ret[0] = data[1];
                    ret[1] = data[0];
                    ret[2] = data[3];
                    ret[3] = data[2];
                    ret[4] = data[5];
                    ret[5] = data[4];
                    ret[6] = data[7];
                    ret[7] = data[6];
                    break;
                case LongByteOrder.ABCDEFGH:
                default:
                    ret = data;
                    break;
            }
            return ret;
        }
    }
}
