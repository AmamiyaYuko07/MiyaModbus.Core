using MiyaModbus.Core.Devices;
using MiyaModbus.Core.Exceptions.ModbusException;
using MiyaModbus.Core.Models.ModbusRtu;
using MiyaModbus.Core.Models.ModbusTcp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Utils
{
    public static class ModbusTcpExtend
    {
        /// <summary>
        /// 读取保持寄存器有符号整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<short> ReadShortAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, 1));
            if (result.IsSuccess)
            {
                return result.GetShort();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 批量读取保持寄存器有符号整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static async Task<short[]> ReadShortsAsync(this ModbusTcpDevice device, short point, short length)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, length));
            if (result.IsSuccess)
            {
                List<short> shorts = new List<short>();
                for (var i = 0; i < length; i++)
                {
                    shorts.Add(result.GetShort(i * 2));
                }
                return shorts.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取保持寄存器多字节数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static async Task<byte[]> ReadDataAsync(this ModbusTcpDevice device, short point, short length)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, length));
            if (result.IsSuccess)
            {
                return result.Result;
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取输入寄存器有符号短整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<short> ReadInputShortAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, 1));
            if (result.IsSuccess)
            {
                return result.GetShort();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 批量读取输入寄存器有符号短整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<short[]> ReadInputShortsAsync(this ModbusTcpDevice device, short point, short length)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, length));
            if (result.IsSuccess)
            {
                List<short> shorts = new List<short>();
                for (var i = 0; i < length; i++)
                {
                    shorts.Add(result.GetShort(i * 2));
                }
                return shorts.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取保持寄存器有符号整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<int> ReadIntAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, 2));
            if (result.IsSuccess)
            {
                return result.GetInt();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 批量读取保持寄存器有符号整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<int[]> ReadIntsAsync(this ModbusTcpDevice device, short point, short length)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, (short)(length * 2)));
            if (result.IsSuccess)
            {
                List<int> ints = new List<int>();
                for (var i = 0; i < length; i++)
                {
                    ints.Add(result.GetInt(i * 4));
                }
                return ints.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取输入寄存器有符号整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<int> ReadInputIntAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, 2));
            if (result.IsSuccess)
            {
                return result.GetInt();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 批量读取输入寄存器有符号整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<int[]> ReadInputIntsAsync(this ModbusTcpDevice device, short point, short length)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, (short)(length * 2)));
            if (result.IsSuccess)
            {
                List<int> ints = new List<int>();
                for (var i = 0; i < length; i++)
                {
                    ints.Add(result.GetInt(i * 4));
                }
                return ints.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取保持寄存器无符号短整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<ushort> ReadUShortAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, 1));
            if (result.IsSuccess)
            {
                return result.GetUShort();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 批量读取保持寄存器无符号短整型数据
        /// </summary>
        public static async Task<ushort[]> ReadUShortsAsync(this ModbusTcpDevice device, short point, short length)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, length));
            if (result.IsSuccess)
            {
                List<ushort> shorts = new List<ushort>();
                for (var i = 0; i < length; i++)
                {
                    shorts.Add(result.GetUShort(i * 2));
                }
                return shorts.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取输入寄存器无符号短整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<ushort> ReadInputUShortAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, 1));
            if (result.IsSuccess)
            {
                return result.GetUShort();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 批量读取输入寄存器无符号短整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<ushort[]> ReadInputUShortsAsync(this ModbusTcpDevice device, short point, short length)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, length));
            if (result.IsSuccess)
            {
                List<ushort> shorts = new List<ushort>();
                for (var i = 0; i < length; i++)
                {
                    shorts.Add(result.GetUShort(i * 2));
                }
                return shorts.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取保持寄存器无符号整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<uint> ReadUIntAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, 2));
            if (result.IsSuccess)
            {
                return result.GetUInt();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 批量读取保持寄存器无符号整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<uint[]> ReadUIntsAsync(this ModbusTcpDevice device, short point, short length)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, (short)(length * 2)));
            if (result.IsSuccess)
            {
                List<uint> ints = new List<uint>();
                for (var i = 0; i < length; i++)
                {
                    ints.Add(result.GetUInt(i * 4));
                }
                return ints.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取输入寄存器无符号整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<uint> ReadInputUIntAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, 2));
            if (result.IsSuccess)
            {
                return result.GetUInt();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 批量读取输入寄存器无符号整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<uint[]> ReadInputUIntsAsync(this ModbusTcpDevice device, short point, short length)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, (short)(length * 2)));
            if (result.IsSuccess)
            {
                List<uint> ints = new List<uint>();
                for (var i = 0; i < length; i++)
                {
                    ints.Add(result.GetUInt(i * 4));
                }
                return ints.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取保持寄存器长整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<long> ReadLongAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, 4));
            if (result.IsSuccess)
            {
                return result.GetLong();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取保持寄存器无符号长整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<ulong> ReadULongAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, 4));
            if (result.IsSuccess)
            {
                return result.GetULong();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取输入寄存器长整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<long> ReadInputLongAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, 4));
            if (result.IsSuccess)
            {
                return result.GetLong();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取输入寄存器无符号长整型数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<ulong> ReadInputULongAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, 4));
            if (result.IsSuccess)
            {
                return result.GetULong();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取保持寄存器浮点数数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<float> ReadFloatAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, 2));
            if (result.IsSuccess)
            {
                return result.GetFloat();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 批量读取保持寄存器浮点数数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<float[]> ReadFloatsAsync(this ModbusTcpDevice device, short point, short length)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, (short)(length * 2)));
            if (result.IsSuccess)
            {
                List<float> floats = new List<float>();
                for (var i = 0; i < length; i++)
                {
                    floats.Add(result.GetFloat(i * 4));
                }
                return floats.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取保持寄存器双精度浮点数
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<double> ReadDoubleAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadHoldRegMessage(device.StationId, point, 4));
            if (result.IsSuccess)
            {
                return result.GetDouble();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取输入寄存器浮点数
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<float> ReadInputFloatAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, 2));
            if (result.IsSuccess)
            {
                return result.GetFloat();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 批量读取输入寄存器浮点数
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<float[]> ReadInputFloatsAsync(this ModbusTcpDevice device, short point, short length)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, (short)(length * 2)));
            if (result.IsSuccess)
            {
                List<float> floats = new List<float>();
                for (var i = 0; i < length; i++)
                {
                    floats.Add(result.GetFloat(i * 4));
                }
                return floats.ToArray();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取输入寄存器双精度浮点数
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<double> ReadInputDoubleAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputRegMessage(device.StationId, point, 4));
            if (result.IsSuccess)
            {
                return result.GetDouble();
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取线圈状态
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static async Task<bool> ReadCoilAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadOutputCoilMessage(device.StationId, point, 1));
            if (result.IsSuccess)
            {
                return result.GetBool(0);
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 批量读取线圈状态
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static async Task<bool[]> ReadCoilsAsync(this ModbusTcpDevice device, short point, short length)
        {
            bool[] lists = new bool[length];
            var result = await device.SendMessageAsync(new ModbusReadOutputCoilMessage(device.StationId, point, length));
            if (result.IsSuccess)
            {
                var bytes = result.Result;
                for (var i = 0; i < lists.Length; i++)
                {
                    var index = i;
                    var pair = index / 8;
                    var idx = 0;
                    if (index % 8 > 0)
                    {
                        idx = index % 8;
                    }
                    var data = bytes[pair];
                    var bits = Convert.ToString(data, 2).PadLeft(8, '0');
                    var b = bits[7 - idx] == '1';
                    lists[i] = b;
                }
                return lists;
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 读取指定输入线圈
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static async Task<bool> ReadInputCoilAsync(this ModbusTcpDevice device, short point)
        {
            var result = await device.SendMessageAsync(new ModbusReadInputCoilMessage(device.StationId, point, 1));
            if (result.IsSuccess)
            {
                return result.GetBool(0);
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 批量读取指定输入线圈状态
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static async Task<bool[]> ReadInputCoilsAsync(this ModbusTcpDevice device, short point, short length)
        {
            bool[] lists = new bool[length];
            var result = await device.SendMessageAsync(new ModbusReadInputCoilMessage(device.StationId, point, length));
            if (result.IsSuccess)
            {
                var bytes = result.Result;
                for (var i = 0; i < lists.Length; i++)
                {
                    var index = i;
                    var pair = index / 8;
                    var idx = 0;
                    if (index % 8 > 0)
                    {
                        idx = index % 8;
                    }
                    var data = bytes[pair];
                    var bits = Convert.ToString(data, 2).PadLeft(8, '0');
                    var b = bits[7 - idx] == '1';
                    lists[i] = b;
                }
                return lists;
            }
            throw new Exception($"对于点位:{point}的返回值错误");
        }

        /// <summary>
        /// 写入线圈状态
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static async Task<bool> WriteSingleCoilAsync(this ModbusTcpDevice device, short point, bool value)
        {
            var result = await device.SendMessageAsync(new ModbusWriteSingleCoilMessage(device.StationId, point, value));
            return result.IsSuccess;
        }

        /// <summary>
        /// 批量写入线圈
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static async Task<bool> WriteCoilAsync(this ModbusTcpDevice device, short point, params bool[] values)
        {
            var result = await device.SendMessageAsync(new ModbusWriteCoilMessage(device.StationId, point, values));
            return result.IsSuccess;
        }

        /// <summary>
        /// 将短整型写入指定保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> WriteShortAsync(this ModbusTcpDevice device, short point, short value)
        {
            var data = BitConverter.GetBytes(value);
            if (device.Options.ShortReverse)
            {
                Array.Reverse(data);
            }
            var result = await device.SendMessageAsync(new ModbusWriteSingleHoldRegMessage(device.StationId, point, data));
            return result.IsSuccess;
        }

        /// <summary>
        /// 批量写入短整型
        /// </summary>
        public static async Task<bool> WriteShortsAsync(this ModbusTcpDevice device, short point, params short[] values)
        {
            List<byte> bytes = new List<byte>();
            foreach (var value in values)
            {
                var data = BitConverter.GetBytes(value);
                if (device.Options.ShortReverse)
                {
                    Array.Reverse(data);
                }
                bytes.AddRange(data);
            }
            var result = await device.SendMessageAsync(new ModbusWriteHoldRegMessage(device.StationId, point, bytes.ToArray()));
            return result.IsSuccess;
        }

        /// <summary>
        /// 将无符号短整型写入指定保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> WriteUShortAsync(this ModbusTcpDevice device, short point, ushort value)
        {
            var data = BitConverter.GetBytes(value);
            if (device.Options.ShortReverse)
            {
                Array.Reverse(data);
            }
            var result = await device.SendMessageAsync(new ModbusWriteSingleHoldRegMessage(device.StationId, point, data));
            return result.IsSuccess;
        }

        /// <summary>
        /// 批量写入无符号短整型到保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> WriteUShortsAsync(this ModbusTcpDevice device, short point, params ushort[] values)
        {
            List<byte> bytes = new List<byte>();
            foreach (var value in values)
            {
                var data = BitConverter.GetBytes(value);
                if (device.Options.ShortReverse)
                {
                    Array.Reverse(data);
                }
                bytes.AddRange(data);
            }
            var result = await device.SendMessageAsync(new ModbusWriteHoldRegMessage(device.StationId, point, bytes.ToArray()));
            return result.IsSuccess;
        }

        /// <summary>
        /// 将有符号整型写入保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> WriteIntAsync(this ModbusTcpDevice device, short point, int value)
        {
            var data = BitConverter.GetBytes(value);
            var ret = data.BytesOrder(device.Options.IntOrder);
            var result = await device.SendMessageAsync(new ModbusWriteHoldRegMessage(device.StationId, point, ret));
            return result.IsSuccess;
        }

        /// <summary>
        /// 批量将有符号整型写入保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> WriteIntsAsync(this ModbusTcpDevice device, short point, params int[] values)
        {
            List<byte> bytes = new List<byte>();
            foreach (var value in values)
            {
                var data = BitConverter.GetBytes(value);
                data = data.BytesOrder(device.Options.IntOrder);
                bytes.AddRange(data);
            }
            var result = await device.SendMessageAsync(new ModbusWriteHoldRegMessage(device.StationId, point, bytes.ToArray()));
            return result.IsSuccess;
        }

        /// <summary>
        /// 将无符号整型写入指定保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> WriteUIntAsync(this ModbusTcpDevice device, short point, uint value)
        {
            var data = BitConverter.GetBytes(value);
            var ret = data.BytesOrder(device.Options.IntOrder);
            var result = await device.SendMessageAsync(new ModbusWriteHoldRegMessage(device.StationId, point, ret));
            return result.IsSuccess;
        }

        /// <summary>
        /// 批量将无符号整型写入指定保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static async Task<bool> WriteUIntsAsync(this ModbusTcpDevice device, short point, params uint[] values)
        {
            List<byte> bytes = new List<byte>();
            foreach (var value in values)
            {
                var data = BitConverter.GetBytes(value);
                data = data.BytesOrder(device.Options.IntOrder);
                bytes.AddRange(data);
            }
            var result = await device.SendMessageAsync(new ModbusWriteHoldRegMessage(device.StationId, point, bytes.ToArray()));
            return result.IsSuccess;
        }

        /// <summary>
        /// 将长整型写入指定保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> WriteLongAsync(this ModbusTcpDevice device, short point, long value)
        {
            var data = BitConverter.GetBytes(value);
            var ret = data.BytesOrder(device.Options.LongOrder);
            var result = await device.SendMessageAsync(new ModbusWriteHoldRegMessage(device.StationId, point, ret));
            return result.IsSuccess;
        }

        /// <summary>
        /// 将无符号长整型写入指定保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> WriteULongAsync(this ModbusTcpDevice device, short point, ulong value)
        {
            var data = BitConverter.GetBytes(value);
            var ret = data.BytesOrder(device.Options.LongOrder);
            var result = await device.SendMessageAsync(new ModbusWriteHoldRegMessage(device.StationId, point, ret));
            return result.IsSuccess;
        }

        /// <summary>
        /// 将浮点数写入指定保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> WriteFloatAsync(this ModbusTcpDevice device, short point, float value)
        {
            var data = BitConverter.GetBytes(value);
            var ret = data.BytesOrder(device.Options.FloatOrder);
            var result = await device.SendMessageAsync(new ModbusWriteHoldRegMessage(device.StationId, point, ret));
            return result.IsSuccess;
        }

        /// <summary>
        /// 将浮点数写入指定保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> WriteFloatsAsync(this ModbusTcpDevice device, short point, params float[] values)
        {
            List<byte> bytes = new List<byte>();
            foreach (var value in values)
            {
                var data = BitConverter.GetBytes(value);
                data = data.BytesOrder(device.Options.FloatOrder);
                bytes.AddRange(data);
            }
            var result = await device.SendMessageAsync(new ModbusWriteHoldRegMessage(device.StationId, point, bytes.ToArray()));
            return result.IsSuccess;
        }

        /// <summary>
        /// 将双精度浮点数写入指定保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<bool> WriteDoubleAsync(this ModbusTcpDevice device, short point, double value)
        {
            var data = BitConverter.GetBytes(value);
            var ret = data.BytesOrder(device.Options.DoubleOrder);
            var result = await device.SendMessageAsync(new ModbusWriteHoldRegMessage(device.StationId, point, ret));
            return result.IsSuccess;
        }

        /// <summary>
        /// 将数据写入指定保持寄存器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="point"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<bool> WriteDataAsync(this ModbusTcpDevice device, short point, byte[] data)
        {
            var result = await device.SendMessageAsync(new ModbusWriteHoldRegMessage(device.StationId, point, data));
            return result.IsSuccess;
        }
    }
}
