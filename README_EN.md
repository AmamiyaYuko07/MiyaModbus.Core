# MiyaModbus
## install
``` C#
Install-Package MiyaModbus.Core -Version 1.0.3
```
## Simple Usage
### Create Device
1. Create the ModbusTcp Device
``` C#
    DeviceFactory.CreateModbusTcpDevice("device", 1, "127.0.0.1", 502);
```
2. Create the ModbusRtu Device
``` C#
    DeviceFactory.CreateModbusRtuDevice("device", 0x01, "COM1", 9600, 8, StopBits.One, Parity.None);
```
3. Create the ModbusRtu Device using Tcp
``` C#
    DeviceFactory.CreateModbusRtuOverTcpDevice("device", 1, "127.0.0.1", 502);
```
4. Create the ASCII Serial Device
``` C#
DeviceFactory.CreateModbusRtuOverTcpDevice("COM1", 9600, 8, StopBits.One, Parity.None);
```
and more other

### Read And Write Data
``` C#
    //创建设备
    var device = DeviceFactory.CreateModbusRtuOverTcpDevice(1, "127.0.0.1", 502);
    //启动设备
    await device.StartAsync();

    //写入短整型
    await device.WriteShortAsync(0, d);
    //读取短整型
    var v = await device.ReadShortAsync(0);

    //写入浮点数
    await device.WriteFloatAsync(0, f);
    //读取浮点数
    var v = await device.ReadFloatAsync(0);

    //写入无符号整数
    await device.WriteUIntAsync(0, d);
    //读取无符号整数
    var v = await device.ReadUIntAsync(0);

    //停止设备
    await device.StopAsync();
```

### Advanced Usage
**1. Change Byte Order**

Currently, many different PLCs use different byte orders, and MiyaModbus supports changing the byte order of device reads and writes using simple settings
To adapt to different devices

``` C#
//短整型是否反序
device.Options.ShortReverse = true;
//浮点型数据字节序
device.Options.FloatOrder = ByteOrder.ABCD;
//双精度浮点型字节序
device.Options.DoubleOrder = LongByteOrder.ABCDEFGH;
//整型字节序
device.Options.IntOrder = ByteOrder.ABCD;
//长整型字节序
device.Options.LongOrder = LongByteOrder.ABCDEFGH;
```

**2. Multiple devices using the same network channel**

If multiple ModbusRtu devices are connected on a serial port line, and each device is distinguished by a different station number, then
We can create multiple devices using the same network, and the data sent by different devices will be automatically sorted within the network channel,
And thread safe, but multiple devices using the same network channel can cause a decrease in communication speed.
``` C#

    var network = new SerialNetwork("COM1", 9600, 8, StopBits.One, Parity.None);
    var channel = new DefaultChannel(network);
    var device1 = new ModbusRtuDevice(channel,options=>
    {
         options.AddParams("stationid", 0x01);
    });    
    var device2 = new ModbusRtuDevice(channel,options=>
    {
         options.AddParams("stationid", 0x02);
    });

```

The data sent by **deivce1** and **device2** will both pass through the same channel and automatically within the channel
Send and obtain the returned results in sequence, without interfering with each other.
