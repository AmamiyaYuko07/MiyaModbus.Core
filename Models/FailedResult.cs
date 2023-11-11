using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models
{
    public class FailedResult : BaseResult
    {
        public FailedResult(ResultOption resultOption) 
            : base(resultOption)
        {

        }

        public override bool IsSuccess => false;
    }
}
