# MiyaModbus
## 安装
``` C#
Install-Package MiyaModbus.Core -Version 1.0.3
```
## 简易使用方法
### 创建设备
1. 创建ModbusTcp设备
``` C#
    DeviceFactory.CreateModbusTcpDevice(1, "127.0.0.1", 502);
```
2. 创建ModbusRtu设备
``` C#
    DeviceFactory.CreateModbusRtuDevice(0x01, "COM1", 9600, 8, StopBits.One, Parity.None);
```
3. 创建使用TCP协议的ModbusRtu设备
``` C#
    DeviceFactory.CreateModbusRtuOverTcpDevice(1, "127.0.0.1", 502);
```
4. 创建使用串口的文本通讯设备
``` C#
DeviceFactory.CreateModbusRtuOverTcpDevice("COM1", 9600, 8, StopBits.One, Parity.None);
```
还有其他类似的设备创建方式

### 读写数据
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

### 高级用法
**1. 更改字节序**

当前很多不同的PLC使用不同的字节序，MiyaModbus支持使用简单的设置的方式更改设备读写的字节序
以适应不同的设备

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

**2. 多个设备使用同一个网络通道**

如果在一条串口线上连接了多个ModbusRtu设备，每个设备使用不同的站号来区分，那么
我们可以创建多个设备使用同一个网络，不同设备发送的数据在网络通道内会自动排序，
并且线程安全，但是多个设备使用同一个网络通道会导致**通信速率下降**。
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

此时***deivce1***和***device2***发送的数据都会经过同一个通道，并在通道内自动
按顺序发送并获取返回结果，两者互不干扰。
