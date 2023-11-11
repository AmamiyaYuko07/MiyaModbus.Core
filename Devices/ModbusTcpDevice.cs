using MiyaModbus.Core.Channels;
using MiyaModbus.Core.Exceptions.ModbusException;
using MiyaModbus.Core.Models;
using MiyaModbus.Core.Models.ModbusTcp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiyaModbus.Core.Devices
{
    public class ModbusTcpDevice : BaseDevice
    {
        public ModbusTcpDevice(IChannel channel)
            : base(channel)
        {
            StationId = 0x01;
        }

        public ModbusTcpDevice(IChannel channel, Action<DeviceOptions> option)
            : base(channel, option)
        {
            StationId = Options.GetParams<byte>("stationId");
        }

        public byte StationId { set; get; }

        public override async Task<IResult> SendMessageAsync(IMessage message)
        {
            if (!IsRunning)
            {
                await StartAsync();
            }
            var tryCount = Options.DeviceRetryCount;
            var options = new ResultOption
            {
                Device = this,
                Data = message.Build()
            };
            while (IsRunning && tryCount > 0)
            {
                var retData = new byte[0];
                try
                {
                    retData = await Channel.SendMessageAsync(message);
                }
                catch (Exception ex)
                {
                    tryCount--;
                    continue;
                }
                if (retData.Length == 9)
                {
                    //PLC返回错误 报错
                    switch (retData[6])
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
                    if (retData == null || retData.Length == 0 || retData.Length <= 9)
                    {
                        tryCount--;
                        continue;
                    }
                    var cmd = retData[7];
                    IResult result;
                    switch (cmd)
                    {
                        case 0x01:
                        case 0x02:
                        case 0x03:
                        case 0x04:
                            result = new ModbusReadResult(options);
                            break;
                        case 0x05:
                        case 0x06:
                        case 0x10:
                            result = new ModbusWriteResult(options);
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
