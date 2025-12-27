using MiyaModbus.Core.Devices;
using MiyaModbus.Core.Exceptions.ModbusException;
using MiyaModbus.Core.Models;
using MiyaModbus.Core.Models.ModbusRtu;
using MiyaModbus.Core.Models.ModbusTcp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Utils
{
    public static class ModbusExtend
    {
        // 读取保持寄存器 有符号短整型
        public static async Task<short> ReadShortAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, 1));
            if (result.IsSuccess) return result.GetShort();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 批量读取保持寄存器 有符号短整型
        public static async Task<short[]> ReadShortsAsync(this IDevice device, ushort point, ushort length)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, length));
            if (result.IsSuccess)
            {
                var shorts = new List<short>(length);
                for (var i = 0; i < length; i++) shorts.Add(result.GetShort(i * 2));
                return shorts.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取保持寄存器 多字节数据
        public static async Task<byte[]> ReadDataAsync(this IDevice device, ushort point, ushort length)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, length));
            if (result.IsSuccess) return result.Result;
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取输入寄存器 有符号短整型
        public static async Task<short> ReadInputShortAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, 1));
            if (result.IsSuccess) return result.GetShort();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 批量读取输入寄存器 有符号短整型
        public static async Task<short[]> ReadInputShortsAsync(this IDevice device, ushort point, ushort length)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, length));
            if (result.IsSuccess)
            {
                var shorts = new List<short>(length);
                for (var i = 0; i < length; i++) shorts.Add(result.GetShort(i * 2));
                return shorts.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取保持寄存器 有符号整型
        public static async Task<int> ReadIntAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, 2));
            if (result.IsSuccess) return result.GetInt();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 批量读取保持寄存器 有符号整型
        public static async Task<int[]> ReadIntsAsync(this IDevice device, ushort point, ushort length)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, (ushort)(length * 2)));
            if (result.IsSuccess)
            {
                var ints = new List<int>(length);
                for (var i = 0; i < length; i++) ints.Add(result.GetInt(i * 4));
                return ints.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取输入寄存器 有符号整型
        public static async Task<int> ReadInputIntAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, 2));
            if (result.IsSuccess) return result.GetInt();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 批量读取输入寄存器 有符号整型
        public static async Task<int[]> ReadInputIntsAsync(this IDevice device, ushort point, ushort length)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, (ushort)(length * 2)));
            if (result.IsSuccess)
            {
                var ints = new List<int>(length);
                for (var i = 0; i < length; i++) ints.Add(result.GetInt(i * 4));
                return ints.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取保持寄存器 无符号短整型
        public static async Task<ushort> ReadUShortAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, 1));
            if (result.IsSuccess) return result.GetUShort();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 批量读取保持寄存器 无符号短整型
        public static async Task<ushort[]> ReadUShortsAsync(this IDevice device, ushort point, ushort length)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, length));
            if (result.IsSuccess)
            {
                var arr = new List<ushort>(length);
                for (var i = 0; i < length; i++) arr.Add(result.GetUShort(i * 2));
                return arr.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取输入寄存器 无符号短整型
        public static async Task<ushort> ReadInputUShortAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, 1));
            if (result.IsSuccess) return result.GetUShort();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 批量读取输入寄存器 无符号短整型
        public static async Task<ushort[]> ReadInputUShortsAsync(this IDevice device, ushort point, ushort length)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, length));
            if (result.IsSuccess)
            {
                var arr = new List<ushort>(length);
                for (var i = 0; i < length; i++) arr.Add(result.GetUShort(i * 2));
                return arr.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取保持寄存器 无符号整型
        public static async Task<uint> ReadUIntAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, 2));
            if (result.IsSuccess) return result.GetUInt();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 批量读取保持寄存器 无符号整型
        public static async Task<uint[]> ReadUIntsAsync(this IDevice device, ushort point, ushort length)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, (ushort)(length * 2)));
            if (result.IsSuccess)
            {
                var arr = new List<uint>(length);
                for (var i = 0; i < length; i++) arr.Add(result.GetUInt(i * 4));
                return arr.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取输入寄存器 无符号整型
        public static async Task<uint> ReadInputUIntAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, 2));
            if (result.IsSuccess) return result.GetUInt();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 批量读取输入寄存器 无符号整型
        public static async Task<uint[]> ReadInputUIntsAsync(this IDevice device, ushort point, ushort length)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, (ushort)(length * 2)));
            if (result.IsSuccess)
            {
                var arr = new List<uint>(length);
                for (var i = 0; i < length; i++) arr.Add(result.GetUInt(i * 4));
                return arr.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取保持寄存器 长整型
        public static async Task<long> ReadLongAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, 4));
            if (result.IsSuccess) return result.GetLong();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取保持寄存器 无符号长整型
        public static async Task<ulong> ReadULongAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, 4));
            if (result.IsSuccess) return result.GetULong();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取输入寄存器 长整型
        public static async Task<long> ReadInputLongAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, 4));
            if (result.IsSuccess) return result.GetLong();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取输入寄存器 无符号长整型
        public static async Task<ulong> ReadInputULongAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, 4));
            if (result.IsSuccess) return result.GetULong();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取保持寄存器 浮点数
        public static async Task<float> ReadFloatAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, 2));
            if (result.IsSuccess) return result.GetFloat();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 批量读取保持寄存器 浮点数
        public static async Task<float[]> ReadFloatsAsync(this IDevice device, ushort point, ushort length)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, (ushort)(length * 2)));
            if (result.IsSuccess)
            {
                var arr = new List<float>(length);
                for (var i = 0; i < length; i++) arr.Add(result.GetFloat(i * 4));
                return arr.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取保持寄存器 双精度浮点数
        public static async Task<double> ReadDoubleAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadHoldRegMessage(device, point, 4));
            if (result.IsSuccess) return result.GetDouble();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取输入寄存器 浮点数
        public static async Task<float> ReadInputFloatAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, 2));
            if (result.IsSuccess) return result.GetFloat();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 批量读取输入寄存器 浮点数
        public static async Task<float[]> ReadInputFloatsAsync(this IDevice device, ushort point, ushort length)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, (ushort)(length * 2)));
            if (result.IsSuccess)
            {
                var arr = new List<float>(length);
                for (var i = 0; i < length; i++) arr.Add(result.GetFloat(i * 4));
                return arr.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取输入寄存器 双精度浮点数
        public static async Task<double> ReadInputDoubleAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadInputRegMessage(device, point, 4));
            if (result.IsSuccess) return result.GetDouble();
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取线圈
        public static async Task<bool> ReadCoilAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadOutputCoilMessage(device, point, 1));
            if (result.IsSuccess) return result.GetBool(0);
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 批量读取线圈
        public static async Task<bool[]> ReadCoilsAsync(this IDevice device, ushort point, ushort length)
        {
            bool[] lists = new bool[length];
            var result = await device.SendMessageAsync(CreateReadOutputCoilMessage(device, point, length));
            if (result.IsSuccess)
            {
                var bytes = result.Result;
                for (var i = 0; i < lists.Length; i++)
                {
                    var index = i;
                    var pair = index / 8;
                    var idx = index % 8 > 0 ? index % 8 : 0;
                    var data = bytes[pair];
                    var bits = Convert.ToString(data, 2).PadLeft(8, '0');
                    lists[i] = bits[7 - idx] == '1';
                }
                return lists;
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 读取输入线圈
        public static async Task<bool> ReadInputCoilAsync(this IDevice device, ushort point)
        {
            var result = await device.SendMessageAsync(CreateReadInputCoilMessage(device, point, 1));
            if (result.IsSuccess) return result.GetBool(0);
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 批量读取输入线圈
        public static async Task<bool[]> ReadInputCoilsAsync(this IDevice device, ushort point, ushort length)
        {
            bool[] lists = new bool[length];
            var result = await device.SendMessageAsync(CreateReadInputCoilMessage(device, point, length));
            if (result.IsSuccess)
            {
                var bytes = result.Result;
                for (var i = 0; i < lists.Length; i++)
                {
                    var index = i;
                    var pair = index / 8;
                    var idx = index % 8 > 0 ? index % 8 : 0;
                    var data = bytes[pair];
                    var bits = Convert.ToString(data, 2).PadLeft(8, '0');
                    lists[i] = bits[7 - idx] == '1';
                }
                return lists;
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        // 写入单个线圈
        public static async Task<bool> WriteSingleCoilAsync(this IDevice device, ushort point, bool value)
        {
            var result = await device.SendMessageAsync(CreateWriteSingleCoilMessage(device, point, value));
            return result.IsSuccess;
        }

        // 批量写入线圈
        public static async Task<bool> WriteCoilAsync(this IDevice device, ushort point, params bool[] values)
        {
            var result = await device.SendMessageAsync(CreateWriteCoilMessage(device, point, values));
            return result.IsSuccess;
        }

        // 写入保持寄存器 短整型
        public static async Task<bool> WriteShortAsync(this IDevice device, ushort point, short value)
        {
            var baseDev = AsBaseDevice(device);
            var data = BitConverter.GetBytes(value);
            if (baseDev.Options.ShortReverse) Array.Reverse(data);
            var result = await device.SendMessageAsync(CreateWriteSingleHoldRegMessage(device, point, data));
            return result.IsSuccess;
        }

        // 批量写入保持寄存器 短整型
        public static async Task<bool> WriteShortsAsync(this IDevice device, ushort point, params short[] values)
        {
            var baseDev = AsBaseDevice(device);
            var bytes = new List<byte>(values.Length * 2);
            foreach (var value in values)
            {
                var data = BitConverter.GetBytes(value);
                if (baseDev.Options.ShortReverse) Array.Reverse(data);
                bytes.AddRange(data);
            }
            var result = await device.SendMessageAsync(CreateWriteHoldRegMessage(device, point, bytes.ToArray()));
            return result.IsSuccess;
        }

        // 写入保持寄存器 无符号短整型
        public static async Task<bool> WriteUShortAsync(this IDevice device, ushort point, ushort value)
        {
            var baseDev = AsBaseDevice(device);
            var data = BitConverter.GetBytes(value);
            if (baseDev.Options.ShortReverse) Array.Reverse(data);
            var result = await device.SendMessageAsync(CreateWriteSingleHoldRegMessage(device, point, data));
            return result.IsSuccess;
        }

        // 批量写入保持寄存器 无符号短整型
        public static async Task<bool> WriteUShortsAsync(this IDevice device, ushort point, params ushort[] values)
        {
            var baseDev = AsBaseDevice(device);
            var bytes = new List<byte>(values.Length * 2);
            foreach (var value in values)
            {
                var data = BitConverter.GetBytes(value);
                if (baseDev.Options.ShortReverse) Array.Reverse(data);
                bytes.AddRange(data);
            }
            var result = await device.SendMessageAsync(CreateWriteHoldRegMessage(device, point, bytes.ToArray()));
            return result.IsSuccess;
        }

        // 写入保持寄存器 有符号整型
        public static async Task<bool> WriteIntAsync(this IDevice device, ushort point, int value)
        {
            var baseDev = AsBaseDevice(device);
            var data = BitConverter.GetBytes(value);
            var ret = data.BytesOrder(baseDev.Options.IntOrder);
            var result = await device.SendMessageAsync(CreateWriteHoldRegMessage(device, point, ret));
            return result.IsSuccess;
        }

        // 批量写入保持寄存器 有符号整型
        public static async Task<bool> WriteIntsAsync(this IDevice device, ushort point, params int[] values)
        {
            var baseDev = AsBaseDevice(device);
            var bytes = new List<byte>(values.Length * 4);
            foreach (var value in values)
            {
                var data = BitConverter.GetBytes(value);
                data = data.BytesOrder(baseDev.Options.IntOrder);
                bytes.AddRange(data);
            }
            var result = await device.SendMessageAsync(CreateWriteHoldRegMessage(device, point, bytes.ToArray()));
            return result.IsSuccess;
        }

        // 写入保持寄存器 无符号整型
        public static async Task<bool> WriteUIntAsync(this IDevice device, ushort point, uint value)
        {
            var baseDev = AsBaseDevice(device);
            var data = BitConverter.GetBytes(value);
            var ret = data.BytesOrder(baseDev.Options.IntOrder);
            var result = await device.SendMessageAsync(CreateWriteHoldRegMessage(device, point, ret));
            return result.IsSuccess;
        }

        // 批量写入保持寄存器 无符号整型
        public static async Task<bool> WriteUIntsAsync(this IDevice device, ushort point, params uint[] values)
        {
            var baseDev = AsBaseDevice(device);
            var bytes = new List<byte>(values.Length * 4);
            foreach (var value in values)
            {
                var data = BitConverter.GetBytes(value);
                data = data.BytesOrder(baseDev.Options.IntOrder);
                bytes.AddRange(data);
            }
            var result = await device.SendMessageAsync(CreateWriteHoldRegMessage(device, point, bytes.ToArray()));
            return result.IsSuccess;
        }

        // 写入保持寄存器 长整型
        public static async Task<bool> WriteLongAsync(this IDevice device, ushort point, long value)
        {
            var baseDev = AsBaseDevice(device);
            var data = BitConverter.GetBytes(value);
            var ret = data.BytesOrder(baseDev.Options.LongOrder);
            var result = await device.SendMessageAsync(CreateWriteHoldRegMessage(device, point, ret));
            return result.IsSuccess;
        }

        // 写入保持寄存器 无符号长整型
        public static async Task<bool> WriteULongAsync(this IDevice device, ushort point, ulong value)
        {
            var baseDev = AsBaseDevice(device);
            var data = BitConverter.GetBytes(value);
            var ret = data.BytesOrder(baseDev.Options.LongOrder);
            var result = await device.SendMessageAsync(CreateWriteHoldRegMessage(device, point, ret));
            return result.IsSuccess;
        }

        // 写入保持寄存器 浮点数
        public static async Task<bool> WriteFloatAsync(this IDevice device, ushort point, float value)
        {
            var baseDev = AsBaseDevice(device);
            var data = BitConverter.GetBytes(value);
            var ret = data.BytesOrder(baseDev.Options.FloatOrder);
            var result = await device.SendMessageAsync(CreateWriteHoldRegMessage(device, point, ret));
            return result.IsSuccess;
        }

        // 批量写入保持寄存器 浮点数
        public static async Task<bool> WriteFloatsAsync(this IDevice device, ushort point, params float[] values)
        {
            var baseDev = AsBaseDevice(device);
            var bytes = new List<byte>(values.Length * 4);
            foreach (var value in values)
            {
                var data = BitConverter.GetBytes(value);
                data = data.BytesOrder(baseDev.Options.FloatOrder);
                bytes.AddRange(data);
            }
            var result = await device.SendMessageAsync(CreateWriteHoldRegMessage(device, point, bytes.ToArray()));
            return result.IsSuccess;
        }

        // 写入保持寄存器 双精度浮点数
        public static async Task<bool> WriteDoubleAsync(this IDevice device, ushort point, double value)
        {
            var baseDev = AsBaseDevice(device);
            var data = BitConverter.GetBytes(value);
            var ret = data.BytesOrder(baseDev.Options.DoubleOrder);
            var result = await device.SendMessageAsync(CreateWriteHoldRegMessage(device, point, ret));
            return result.IsSuccess;
        }

        // 写入保持寄存器 原始数据
        public static async Task<bool> WriteDataAsync(this IDevice device, ushort point, byte[] data)
        {
            var result = await device.SendMessageAsync(CreateWriteHoldRegMessage(device, point, data));
            return result.IsSuccess;
        }

        // ----------------- 内部辅助方法：根据设备类型选择消息 -----------------

        private static IMessage CreateReadHoldRegMessage(IDevice device, ushort point, ushort length)
        {
            if (device is ModbusTcpDevice tcp) return new ModbusReadHoldRegMessage(tcp.StationId, point, length);
            if (device is ModbusRtuDevice rtu) return new ModbusRtuReadHoldRegMessage(rtu.StationId, point, length);
            throw new NotSupportedException("不支持的设备类型");
        }

        private static IMessage CreateReadInputRegMessage(IDevice device, ushort point, ushort length)
        {
            if (device is ModbusTcpDevice tcp) return new ModbusReadInputRegMessage(tcp.StationId, point, length);
            if (device is ModbusRtuDevice rtu) return new ModbusRtuReadInputRegMessage(rtu.StationId, point, length);
            throw new NotSupportedException("不支持的设备类型");
        }

        private static IMessage CreateReadOutputCoilMessage(IDevice device, ushort point, ushort length)
        {
            if (device is ModbusTcpDevice tcp) return new ModbusReadOutputCoilMessage(tcp.StationId, point, length);
            if (device is ModbusRtuDevice rtu) return new ModbusRtuReadOutputCoilMessage(rtu.StationId, point, length);
            throw new NotSupportedException("不支持的设备类型");
        }

        private static IMessage CreateReadInputCoilMessage(IDevice device, ushort point, ushort length)
        {
            if (device is ModbusTcpDevice tcp) return new ModbusReadInputCoilMessage(tcp.StationId, point, length);
            if (device is ModbusRtuDevice rtu) return new ModbusRtuReadInputCoilMessage(rtu.StationId, point, length);
            throw new NotSupportedException("不支持的设备类型");
        }

        private static IMessage CreateWriteSingleCoilMessage(IDevice device, ushort point, bool value)
        {
            if (device is ModbusTcpDevice tcp) return new ModbusWriteSingleCoilMessage(tcp.StationId, point, value);
            if (device is ModbusRtuDevice rtu) return new ModbusRtuWriteSingleCoilMessage(rtu.StationId, point, value);
            throw new NotSupportedException("不支持的设备类型");
        }

        private static IMessage CreateWriteCoilMessage(IDevice device, ushort point, bool[] values)
        {
            if (device is ModbusTcpDevice tcp) return new ModbusWriteCoilMessage(tcp.StationId, point, values);
            if (device is ModbusRtuDevice rtu) return new ModbusRtuWriteCoilMessage(rtu.StationId, point, values);
            throw new NotSupportedException("不支持的设备类型");
        }

        private static IMessage CreateWriteSingleHoldRegMessage(IDevice device, ushort point, byte[] data)
        {
            if (device is ModbusTcpDevice tcp) return new ModbusWriteSingleHoldRegMessage(tcp.StationId, point, data);
            if (device is ModbusRtuDevice rtu) return new ModbusRtuWriteSingleHoldRegMessage(rtu.StationId, point, data);
            throw new NotSupportedException("不支持的设备类型");
        }

        private static IMessage CreateWriteHoldRegMessage(IDevice device, ushort point, byte[] data)
        {
            if (device is ModbusTcpDevice tcp) return new ModbusWriteHoldRegMessage(tcp.StationId, point, data);
            if (device is ModbusRtuDevice rtu) return new ModbusRtuWriteHoldRegMessage(rtu.StationId, point, data);
            throw new NotSupportedException("不支持的设备类型");
        }

        private static BaseDevice AsBaseDevice(IDevice device)
        {
            var baseDev = device as BaseDevice;
            if (baseDev == null) throw new NotSupportedException("不支持的设备类型");
            return baseDev;
        }
    }
}