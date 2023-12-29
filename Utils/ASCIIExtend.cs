using MiyaModbus.Core.Devices;
using MiyaModbus.Core.Exceptions.ModbusException;
using MiyaModbus.Core.Models.ModbusRtu;
using MiyaModbus.Core.Models.ASCII;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Utils
{
    public static class ASCIIExtend
    {
        public static async Task<string> WriteStringAsync(this ASCIIDevice device, string message)
        {
            var result = await device.SendMessageAsync(new ASCIIWriteStringMessage(message));
            if (result.IsSuccess)
            {
                return Encoding.UTF8.GetString(result.Result);
            }
            else
            {
                throw new Exception($"对于消息:{message}的返回值错误");
            }
        }

        public static async Task OnlyWriteAsync(this ASCIIDevice device, string message)
        {
            var result = await device.SendMessageAsync(new ASCIIOnlyWriteMessage(message));
            if (!result.IsSuccess)
            {
                throw new Exception($"消息:{message}的发送失败");
            }
        }

        public static async Task<string> OnlyReadAsync(this ASCIIDevice device)
        {
            var result = await device.SendMessageAsync(new ASCIIOnlyReadMessage());
            if (result.IsSuccess)
            {
                return Encoding.UTF8.GetString(result.Result);
            }
            else
            {
                throw new Exception($"设备没有返回任何返回值！");
            }
        }
    }
}
