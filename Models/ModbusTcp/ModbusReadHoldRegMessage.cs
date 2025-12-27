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
        public ModbusReadHoldRegMessage(byte stationId, ushort point, ushort length)
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
            builder.Append(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06 });
            builder.Append(StationId);
            builder.Append(0x03);       //功能码
            builder.AppendUInt16(Point);
            builder.AppendUInt16(Length);
            return builder.ToArray();
        }
    }
}
