using MiyaModbus.Core.Channels;
using MiyaModbus.Core.Exceptions;
using MiyaModbus.Core.Exceptions.ModbusException;
using MiyaModbus.Core.Models;
using MiyaModbus.Core.Models.ModbusRtu;
using MiyaModbus.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Devices
{
    public class ModbusRtuDevice : BaseDevice
    {
        public ModbusRtuDevice(IChannel channel)
            : base(channel)
        {
            StationId = 0x01;
        }

        public ModbusRtuDevice(IChannel channel, Action<DeviceOptions> option)
            : base(channel, option)
        {
            StationId = Options.GetParams<byte>("stationId");
        }

        public byte StationId { set; get; }

        public override async Task<IResult> SendMessageAsync(IMessage message)
        {
            if (!IsRunning)
            {
                throw new Exception($"设备{Code}尚未运行！");
            }
            var tryCount = Options.DeviceRetryCount;
            var options = new ResultOption
            {
                Device = this,
                Data = message.Build()
            };
            while (IsRunning && tryCount > 0)
            {
                byte[] retData;
                try
                {
                    retData = await Channel.SendMessageAsync(message);
                }
                catch (Exception ex)
                {
                    tryCount--;
                    continue;
                }
                if (retData.Length == 5)
                {
                    switch (retData[0])
                    {
                        case 1:
                            throw new ModbusIllegalFunctionException(retData);
                        case 2:
                            throw new ModbusIllegalDataAddressException(retData);
                        case 3:
                            throw new ModbusIllegalDataValueException(retData);
                        case 4:
                            throw new ModbusDeviceFailureException(retData);
                        default:
                            throw new ModbusException(retData);
                    }
                }
                else
                {
                    if (retData == null || retData.Length == 0 || retData.Length <= 5)
                    {
                        tryCount--;
                        continue;
                    }
                    var cmd = retData[1];
                    var len = retData.Length - 2;
                    var crc1 = retData.SubBytes(0, len).GetCRC16();
                    var crc2 = retData.SubBytes(len);
                    if (!crc1.BytesEquals(crc2))
                    {
                        throw new ModbusCRCErrorException(retData);
                    }
                    IResult result;
                    switch (cmd)
                    {
                        case 0x01:
                        case 0x02:
                        case 0x03:
                        case 0x04:
                            result = new ModbusRtuReadResult(options);
                            break;
                        case 0x05:
                        case 0x06:
                        case 0x10:
                        case 0x0F:
                            result = new ModbusRtuWriteResult(options);
                            break;
                        default:
                            tryCount--;
                            continue;
                    }
                    result.SetData(retData);
                    return result;
                }
            }
            return new FailedResult(options);
        }
    }
}
