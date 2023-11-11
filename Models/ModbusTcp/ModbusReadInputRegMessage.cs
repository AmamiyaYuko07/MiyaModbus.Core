using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusTcp
{
    public class ModbusReadInputRegMessage : BaseMessage
    {
        public ModbusReadInputRegMessage(byte stationId, short point, short length)
        {
            StationId = stationId;
            Point = point;
            Length = length;
        }

        public byte StationId { get; }

        public short Point { get; }

        public short Length { get; }

        public override byte[] Build()
        {
            ByteBuilder builder = new ByteBuilder();
            builder.Append(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06 });
            builder.Append(StationId);
            builder.Append(0x04);       //功能码
            builder.AppendInt16(Point);
            builder.AppendInt16(Length);
            return builder.ToArray();
        }
    }
}
