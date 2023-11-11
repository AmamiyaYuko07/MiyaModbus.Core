using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models
{
    public interface IMessage
    {
        byte[] Build();
    }
}
