using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusTcp
{
    public class ModbusWriteHoldRegMessage : BaseMessage
    {
        public ModbusWriteHoldRegMessage(byte stationId, short point, byte[] values)
        {
            StationId = stationId;
            Point = point;
            if (values.Length % 2 > 0)
            {
                Values = new byte[values.Length + 1];
                Buffer.BlockCopy(values, 0, Values, 1, values.Length);
            }
            else
            {
                Values = values;
            }
        }

        public byte StationId { get; }

        public short Point { get; }

        public byte[] Values { get; }

        public override byte[] Build()
        {
            ByteBuilder builder = new ByteBuilder();
            builder.Append(new byte[] { 0x00, 0x00, 0x00, 0x00 });
            builder.AppendInt16((short)(7 + Values.Length));
            builder.Append(StationId);
            builder.Append(0x10);       //功能码
            builder.AppendInt16(Point);
            builder.AppendInt16((short)(Values.Length / 2));
            builder.Append((byte)Values.Length);
            builder.Append(Values);
            return builder.ToArray();
        }
    }
}
