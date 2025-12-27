using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusRtu
{
    public class ModbusRtuReadOutputCoilMessage : BaseMessage
    {
        public ModbusRtuReadOutputCoilMessage(byte stationId, ushort point, ushort length)
        {
            StationId = stationId;
            Point = point;
            Length = length;
        }

        public byte StationId { get; }
        public ushort Point { get; }
        public ushort Length { get; }

        public override byte[] Build()
        {
            ByteBuilder builder = new ByteBuilder();
            builder.Append(StationId);
            builder.Append(0x01);       //功能码
            builder.AppendUInt16(Point);
            builder.AppendUInt16(Length);
            var crc16 = builder.ToArray().GetCRC16();
            builder.Append(crc16);
            return builder.ToArray();
        }
    }
}
