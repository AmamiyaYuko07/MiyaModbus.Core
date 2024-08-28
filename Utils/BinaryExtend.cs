using MiyaModbus.Core.Devices;
using MiyaModbus.Core.Models.ASCII;
using MiyaModbus.Core.Models.BInary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Utils
{
    public static class BinaryExtend
    {
        public static async Task<byte[]> WriteDataAsync(this BinaryDevice device, int count = 0, params byte[] data)
        {
            var result = await device.SendMessageAsync(new BinaryMessage(data, count));
            if (result.IsSuccess)
            {
                return result.Result;
            }
            else
            {
                throw new Exception($"对于消息:{BitConverter.ToString(data)}的返回值错误");
            }
        }

        public static async Task OnlyWriteAsync(this BinaryDevice device, params byte[] data)
        {
            var result = await device.SendMessageAsync(new BinaryOnlyWriteMessage(data));
            if (!result.IsSuccess)
            {
                throw new Exception($"消息:{BitConverter.ToString(data)}的发送失败");
            }
        }

        public static async Task<byte[]> OnlyReadAsync(this BinaryDevice device, int count = 0)
        {
            var result = await device.SendMessageAsync(new BinaryOnlyReadMessage(count));
            if (result.IsSuccess)
            {
                return result.Result;
            }
            else
            {
                throw new Exception($"设备没有返回任何返回值！");
            }
        }
    }
}
