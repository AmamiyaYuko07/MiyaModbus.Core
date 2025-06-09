using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Models.Mitsubishi
{
    public class MitsubishiReadMessage : BaseMessage
    {
        public MitsubishiReadMessage(int addr)
        {
            Addr = addr;
        }

        public int Addr { get; }

        public override byte[] Build()
        {
            StringBuilder sb = new StringBuilder();
            var addr = (Addr * 2 + 4096).ToString("X4");
            if (Addr > 8000)
            {
                addr = (Addr * 2 + 3584).ToString("X4");
            }
            var addrasc = Asc(addr);
            var sendcmd = "02 30 " + addrasc + "30 32 03";
            var sum = SumCheck(sendcmd);
            sendcmd = sendcmd + " " + sum;
            return String2Bytes(sendcmd);
        }

        private string Asc(string addr)
        {
            var addletter = addr.ToCharArray();
            string addAsc = null;
            foreach (var item in addletter)
            {
                addAsc = addAsc + string.Format("{0:X}", Convert.ToInt32(item)) + " ";
            }
            return addAsc;
        }

        private string SumCheck(string sendString)
        {
            //和校验
            string sendStringSum = sendString.Substring(3);
            string[] sSS = sendStringSum.Split(' ');//和校验SumCheck
            int sumCheck = 0;
            foreach (var item in sSS)
            {
                sumCheck = sumCheck + Convert.ToInt32(item, 16);
            }

            string SumCheck = sumCheck.ToString("X");
            int l = SumCheck.Length;
            string SumCheck1 = SumCheck.Substring(l - 2, 1);
            string SumCheck2 = SumCheck.Substring(l - 1, 1);
            string a = ((int)Convert.ToChar(SumCheck1)).ToString("X2");
            string b = ((int)Convert.ToChar(SumCheck2)).ToString("X2");
            string C = a + " " + b;
            return C;
        }

        private byte[] String2Bytes(string str)
        {
            var senddata = str.Replace(" ", "");
            List<byte> bytes = new List<byte>();
            for (var i = 0; i < senddata.Length; i += 2)
            {
                bytes.Add((byte)Convert.ToInt32(senddata.Substring(i, 2), 16));
            }
            return bytes.ToArray();
        }
    }
}
