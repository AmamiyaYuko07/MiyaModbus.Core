using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusTcp
{
    public class ModbusWriteSingleCoilMessage : BaseMessage
    {
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
        public bool Value { get; }

        public ModbusWriteSingleCoilMessage(byte stationId, ushort point, bool value)
        {
            StationId = stationId;
            Point = point;
            Value = value;
        }

        public override byte[] Build()
        {
            ByteBuilder builder = new ByteBuilder();
            builder.Append(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            builder.Append(0x00);
            builder.Append(0x06);
            builder.Append(StationId);
            builder.Append(0x05);       //功能码
            builder.AppendUInt16(Point); 
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
            return builder.ToArray();
        }
    }
}
