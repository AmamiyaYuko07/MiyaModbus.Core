﻿using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusTcp
{
    public class ModbusWriteCoilMessage : BaseMessage
    {
        public ModbusWriteCoilMessage(byte stationId, short point, short bits, byte[] values)
        {
            StationId = stationId;
            Point = point;
            Values = values;
            Bits = bits;
        }

        public ModbusWriteCoilMessage(byte stationId, short point, params bool[] values)
        {
            StationId = stationId;
            Point = point;
            Bits = (short)values.Length;
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
        public short Point { get; }

        /// <summary>
        /// 写入的值
        /// </summary>
        public byte[] Values { get; }

        /// <summary>
        /// 写入多少位
        /// </summary>
        public short Bits { set; get; }

        public override byte[] Build()
        {
            ByteBuilder builder = new ByteBuilder();
            builder.Append(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            builder.AppendInt16((short)(7 + Values.Length));
            builder.Append(StationId);
            builder.Append(0x0F);       //功能码
            builder.AppendInt16(Point);
            builder.AppendInt16(Bits);
            builder.Append((byte)Values.Length);
            builder.Append(Values);
            return builder.ToArray();
        }
    }
}
