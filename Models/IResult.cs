using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models
{
    public interface IResult
    {
        ResultOption Option { get; }

        void SetData(byte[] data);

        byte[] Result { get; }

        bool IsSuccess { get; }
    }
}
