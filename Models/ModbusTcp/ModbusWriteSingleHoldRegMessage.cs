using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusTcp
{
    public class ModbusWriteSingleHoldRegMessage : BaseMessage
    {
        public ModbusWriteSingleHoldRegMessage(byte stationId, ushort point, byte[] values)
        {
            StationId = stationId;
            Point = point;
            Values = values;
        }

        public byte StationId { get; }

        public ushort Point { get; }

        public byte[] Values { get; }

        public override byte[] Build()
        {
            ByteBuilder builder = new ByteBuilder();
            builder.Append(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            builder.Append(0x00);
            builder.Append(0x06);
            builder.Append(StationId);
            builder.Append(0x06);       //功能码
            builder.AppendUInt16(Point);
            builder.Append(Values);
            return builder.ToArray();
        }
    }
}
