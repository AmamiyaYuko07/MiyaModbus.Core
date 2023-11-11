using MiyaModbus.Core.Enums;
using MiyaModbus.Core.Exceptions;
using MiyaModbus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiyaModbus.Core.Utils
{
    public static class ResultExtend
    {
        /// <summary>
        /// 获取整型
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="LessDataException"></exception>
        public static int GetInt(this IResult result)
        {
            if (!result.IsSuccess || result.Result.Length < 4)
            {
                throw new LessDataException(result.Result, "return value failed or data to less");
            }
            var data = result.Result.Take(4).ToArray();
            var options = result.Option.Device?.Options;
            if (options != null)
            {
                var ret = data.BytesOrder(options.IntOrder);
                return BitConverter.ToInt32(ret, 0);
            }
            return BitConverter.ToInt32(data, 0);
        }

        /// <summary>
        /// 获取无符号整型
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="LessDataException"></exception>
        public static uint GetUInt(this IResult result)
        {
            if (!result.IsSuccess || result.Result.Length < 4)
            {
                throw new LessDataException(result.Result, "return value failed or data to less");
            }
            var data = result.Result.Take(4).ToArray();
            var options = result.Option.Device?.Options;
            if (options != null)
            {
                var ret = data.BytesOrder(options.IntOrder);
                return BitConverter.ToUInt32(ret, 0);
            }
            return BitConverter.ToUInt32(data, 0);
        }

        /// <summary>
        /// 获取短整型
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="LessDataException"></exception>
        public static short GetShort(this IResult result, int skip = 0)
        {
            if (!result.IsSuccess || result.Result.Length < 2)
            {
                throw new LessDataException(result.Result, "return value failed or data to less");
            }
            var data = result.Result.Skip(skip).Take(2).ToArray();
            var options = result.Option.Device?.Options;
            if (options != null && options.ShortReverse)
            {
                Array.Reverse(data);
            }
            return BitConverter.ToInt16(data, 0);
        }

        /// <summary>
        /// 获取无符号短整型
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="LessDataException"></exception>
        public static ushort GetUShort(this IResult result)
        {
            if (!result.IsSuccess || result.Result.Length < 2)
            {
                throw new LessDataException(result.Result, "return value failed or data to less");
            }
            var data = result.Result.Take(2).ToArray();
            var options = result.Option.Device?.Options;
            if (options != null && options.ShortReverse)
            {
                Array.Reverse(data);
            }
            return BitConverter.ToUInt16(data, 0);
        }

        /// <summary>
        /// 获取长整型
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="LessDataException"></exception>
        public static long GetLong(this IResult result)
        {
            if (!result.IsSuccess || result.Result.Length < 8)
            {
                throw new LessDataException(result.Result, "return value failed or data to less");
            }
            var data = result.Result.Take(8).ToArray();
            var options = result.Option.Device?.Options;
            if (options != null)
            {
                var ret = data.BytesOrder(options.LongOrder);
                return BitConverter.ToInt64(ret, 0);
            }
            return BitConverter.ToInt64(data, 0);
        }

        /// <summary>
        /// 获取无符号长整型
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="LessDataException"></exception>
        public static ulong GetULong(this IResult result)
        {
            if (!result.IsSuccess || result.Result.Length < 8)
            {
                throw new LessDataException(result.Result, "return value failed or data to less");
            }
            var data = result.Result.Take(8).ToArray();
            var options = result.Option.Device?.Options;
            if (options != null)
            {
                var ret = data.BytesOrder(options.LongOrder);
                return BitConverter.ToUInt64(ret, 0);
            }
            return BitConverter.ToUInt64(data, 0);
        }

        /// <summary>
        /// 获取浮点数
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static float GetFloat(this IResult result)
        {
            if (!result.IsSuccess || result.Result.Length < 4)
            {
                throw new LessDataException(result.Result, "return value failed or data to less");
            }
            var data = result.Result.Take(4).ToArray();
            var options = result.Option.Device?.Options;
            if (options != null)
            {
                var ret = data.BytesOrder(options.FloatOrder);
                return BitConverter.ToSingle(ret, 0);
            }
            return BitConverter.ToSingle(data, 0);
        }

        /// <summary>
        /// 获取双精度浮点数
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static double GetDouble(this IResult result)
        {
            if (!result.IsSuccess || result.Result.Length < 8)
            {
                throw new LessDataException(result.Result, "return value failed or data to less");
            }
            var data = result.Result.Take(8).ToArray();
            var options = result.Option.Device?.Options;
            if (options != null)
            {
                var ret = data.BytesOrder(options.DoubleOrder);
                return BitConverter.ToDouble(ret, 0);
            }
            return BitConverter.ToDouble(data, 0);
        }

        /// <summary>
        /// 获取返回值中指定数据位的布尔值
        /// </summary>
        /// <param name="result"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="LessDataException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static bool GetBool(this IResult result, byte index)
        {
            if (!result.IsSuccess || result.Result.Length < 1)
            {
                throw new LessDataException(result.Result, "return value failed or data to less");
            }
            var pair = index / 8;
            var idx = 0;
            if (index % 8 > 0)
            {
                idx = index % 8;
            }
            if (result.Result.Length <= pair)
            {
                throw new IndexOutOfRangeException();
            }
            var data = result.Result[pair];
            var bits = Convert.ToString(data, 2).PadLeft(8, '0');
            return bits[7 - idx] == '1';
        }
    }
}
