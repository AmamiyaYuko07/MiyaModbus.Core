using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusRtu
{
    public class ModbusRtuWriteSingleHoldRegMessage : BaseMessage
    {
        public ModbusRtuWriteSingleHoldRegMessage(byte stationId, short point, byte[] values)
        {
            StationId = stationId;
            Point = point;
            Values = values;
        }

        public byte StationId { get; }

        public short Point { get; }

        public byte[] Values { get; }

        public override byte[] Build()
        {
            ByteBuilder builder = new ByteBuilder();
            builder.Append(StationId);
            builder.Append(0x06);
            builder.AppendInt16(Point);
            builder.Append(Values);
            var crc16 = builder.ToArray().GetCRC16();
            builder.Append(crc16);
            return builder.ToArray();
        }
    }
}
