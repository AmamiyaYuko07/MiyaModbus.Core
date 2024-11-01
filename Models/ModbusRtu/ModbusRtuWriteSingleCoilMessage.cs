using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusRtu
{
    public class ModbusRtuWriteSingleCoilMessage : BaseMessage
    {
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
        public bool Value { get; }

        public ModbusRtuWriteSingleCoilMessage(byte stationId, short point, bool value)
        {
            StationId = stationId;
            Point = point;
            Value = value;
        }

        public override byte[] Build()
        {
            ByteBuilder builder = new ByteBuilder();
            builder.Append(StationId);
            builder.Append(0x05);
            builder.AppendInt16(Point);
            if (Value)
            {
                builder.Append(0xFF);
                builder.Append(0x00);
            }
            else
            {
                builder.Append(0x00);
                builder.Append(0x00);
            }
            var crc16 = builder.ToArray().GetCRC16();
            builder.Append(crc16);
            return builder.ToArray();
        }
    }
}
