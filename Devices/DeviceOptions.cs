using MiyaModbus.Core.Channels;
using MiyaModbus.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Devices
{
    public class DeviceOptions
    {
        private readonly Dictionary<string, object> Options = new Dictionary<string, object>();

        public string Code { set; get; }

        public int DeviceRetryCount { set; get; } = 3;

        public bool ShortReverse { set; get; } = true;

        public ByteOrder IntOrder { set; get; } = ByteOrder.ABCD;

        public LongByteOrder LongOrder { set; get; } = LongByteOrder.ABCDEFGH;

        public ByteOrder FloatOrder { set; get; } = ByteOrder.ABCD;

        public LongByteOrder DoubleOrder { set; get; } = LongByteOrder.ABCDEFGH;

        public TimeSpan WriteTimeout { set; get; } = TimeSpan.FromSeconds(3);

        public TimeSpan ReciveTimeout { set; get; } = TimeSpan.FromSeconds(3);

        public TimeSpan StepTime { set; get; } = TimeSpan.FromSeconds(0.2);

        public void AddParams(string key, object value)
        {
            key = key.ToLower();
            if (Options.ContainsKey(key))
            {
                Options[key] = value;
            }
            else
            {
                Options.Add(key, value);
            }
        }

        public T GetParams<T>(string key)
        {
            key = key.ToLower();
            if (Options.ContainsKey(key))
            {
                var result = Options[key];
                return (T)result;
            }
            return default;
        }
    }
}
