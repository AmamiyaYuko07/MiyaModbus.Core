using MiyaModbus.Core.Devices;
using MiyaModbus.Core.Models.Mitsubishi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Utils
{
    public static class MitsubishiExtend
    {
        public static async Task<bool> WriteValue(this MitsubishiDevice device, int Addr, int Value)
        {
            var result = await device.SendMessageAsync(new MitsubishiWriteMessage(Addr, Value));
            return result.IsSuccess;
        }

        public static async Task<int> ReadValue(this MitsubishiDevice device, int Addr)
        {
            var result = await device.SendMessageAsync(new MitsubishiReadMessage(Addr));
            if (result.IsSuccess)
            {
                var s1 = ((char)result.Result[1]).ToString();
                var s2 = ((char)result.Result[2]).ToString();
                var s3 = ((char)result.Result[3]).ToString();
                var s4 = ((char)result.Result[4]).ToString();
                var recive = s3 + s4 + s1 + s2;
                return Convert.ToInt32(recive);
            }
            return default;
        }
    }
}
