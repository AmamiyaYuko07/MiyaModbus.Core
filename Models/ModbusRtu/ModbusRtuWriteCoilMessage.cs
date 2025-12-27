using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusRtu
{
    public class ModbusRtuWriteCoilMessage : BaseMessage
    {
        public ModbusRtuWriteCoilMessage(byte stationId, ushort point, ushort bits, byte[] values)
        {
            StationId = stationId;
            Point = point;
            Values = values;
            Bits = bits;
        }

        public ModbusRtuWriteCoilMessage(byte stationId, ushort point, params bool[] values)
        {
            StationId = stationId;
            Point = point;
            Bits = (ushort)values.Length;
            var strs = values.Select(x => x ? "1" : "0").ToArray();
            var index = strs.Length / 8;
            List<byte> bytes = new List<byte>();
            for (var i = 0; i < index; i++)
            {
                var b = strs.Skip(i * 8).Take(8).Reverse().ToArray();
                var s = string.Join("", b);
                bytes.Add(Convert.ToByte(s, 2));
            }
            var last = strs.Length % 8;
            if (last > 0)
            {
                var b = strs.Reverse().Take(last).Reverse();
                var s = string.Join("", b.Reverse());
                s = s.PadLeft(8, '0');
                bytes.Add(Convert.ToByte(s, 2));
            }
            Values = bytes.ToArray();
        }

        /// <summary>
        /// 站号
        /// </summary>
        public byte StationId { get; }

        /// <summary>
        /// 起始点位
        /// </summary>
        public ushort Point { get; }

        /// <summary>
        /// 写入的值
        /// </summary>
        public byte[] Values { get; }

        /// <summary>
        /// 写入多少位
        /// </summary>
        public ushort Bits { set; get; }

        public override byte[] Build()
        {
            ByteBuilder builder = new ByteBuilder();
            builder.Append(StationId);
            builder.Append(0x0F);
            builder.AppendUInt16(Point);
            builder.AppendUInt16(Bits);
            builder.Append((byte)Values.Length);
            builder.Append(Values);
            var crc16 = builder.ToArray().GetCRC16();
            builder.Append(crc16);
            return builder.ToArray();
        }
    }
}
