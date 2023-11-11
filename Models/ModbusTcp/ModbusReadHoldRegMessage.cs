using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusTcp
{
    /// <summary>
    /// 读取保持寄存器
    /// </summary>
    public class ModbusReadHoldRegMessage : BaseMessage
    {
        public ModbusReadHoldRegMessage(byte stationId, short point, short length)
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
            builder.Append(0x03);       //功能码
            builder.AppendInt16(Point);
            builder.AppendInt16(Length);
            return builder.ToArray();
        }
    }
}
