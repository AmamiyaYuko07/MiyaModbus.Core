using MiyaModbus.Core.Devices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models
{
    public class ResultOption
    {
        public IDevice Device { get; set; }

        public byte[] Data { set; get; }
    }
}
