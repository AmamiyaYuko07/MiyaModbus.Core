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
                return default;
            }
        }

        public static async Task<bool> OnlyWriteAsync(this ASCIIDevice device, string message)
        {
            var result = await device.SendMessageAsync(new ASCIIOnlyWriteMessage(message));
            return result.IsSuccess;
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
                return default;
            }
        }
    }
}
