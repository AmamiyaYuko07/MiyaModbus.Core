using MiyaModbus.Core.Channels;
using MiyaModbus.Core.Devices;
using MiyaModbus.Core.Networks;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net;
using System.Text;

namespace MiyaModbus.Core.Factories
{
    public class DeviceFactory
    {
        /// <summary>
        /// 创建一个ModbusTcp设备
        /// </summary>
        /// <param name="code">设备代码</param>
        /// <param name="stationId">modbus站号</param>
        /// <param name="address">网口地址</param>
        /// <param name="port">端口</param>
        /// <returns></returns>
        public static ModbusTcpDevice CreateModbusTcpDevice(byte stationId, string address, int port)
        {
            var channel = new DefaultChannel(new TcpNetwork(address, port));
            var device = new ModbusTcpDevice(channel, options =>
            {
                options.AddParams("stationid", stationId);
            });
            return device;
        }

        /// <summary>
        /// 创建一个使用网口的ModbusRtu设备
        /// </summary>
        /// <param name="code">设备代码</param>
        /// <param name="stationId">modbus站号</param>
        /// <param name="address">网口地址</param>
        /// <param name="port">端口</param>
        /// <returns></returns>
        public static ModbusRtuDevice CreateModbusRtuOverTcpDevice(byte stationId, string address, int port)
        {
            var channel = new DefaultChannel(new TcpNetwork(address, port));
            var device = new ModbusRtuDevice(channel, options =>
            {
                options.AddParams("stationid", stationId);
            });
            return device;
        }

        /// <summary>
        /// 创建一个ModbusRtu设备
        /// </summary>
        /// <param name="code">设备编码</param>
        /// <param name="stationId">Modbus站号</param>
        /// <param name="portname">串口名称</param>
        /// <param name="baudrate">波特率</param>
        /// <param name="databits">数据位</param>
        /// <param name="stopbits">停止位</param>
        /// <param name="parity">校验位</param>
        /// <returns></returns>
        public static ModbusRtuDevice CreateModbusRtuDevice(byte stationId,
            string portname,
            int baudrate,
            int databits,
            StopBits stopbits,
            Parity parity)
        {
            var channel = new DefaultChannel(new SerialNetwork(portname, baudrate, databits, stopbits, parity));
            var device = new ModbusRtuDevice(channel, options =>
            {
                options.AddParams("stationid", stationId);
            });
            return device;
        }

        public static ASCIIDevice CreateSerialASCIIDevice(string portname,
            int baudrate,
            int databits,
            StopBits stopbits,
            Parity parity)
        {
            var channel = new DefaultChannel(new SerialNetwork(portname, baudrate, databits, stopbits, parity));
            var device = new ASCIIDevice(channel);
            return device;
        }

        public static ASCIIDevice CreateTcpASCIIDevice(string address, int port)
        {
            var channel = new DefaultChannel(new TcpNetwork(address, port));
            var device = new ASCIIDevice(channel);
            return device;
        }
    }
}
