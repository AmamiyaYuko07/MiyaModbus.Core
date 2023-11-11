﻿using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace MiyaModbus.Core.Models.ModbusRtu
{
    public class ModbusRtuReadHoldRegMessage : BaseMessage
    {
        public ModbusRtuReadHoldRegMessage(byte stationId, short point, short length)
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
            builder.Append(StationId);
            builder.Append(0x03);       //功能码
            builder.AppendInt16(Point);
            builder.AppendInt16(Length);
            var crc16 = builder.ToArray().GetCRC16();
            builder.Append(crc16);
            return builder.ToArray();
        }
    }
}
