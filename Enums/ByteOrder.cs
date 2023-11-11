using System;
using System.Collections.Generic;
using System.Text;

namespace MiyaModbus.Core.Enums
{
    public enum ByteOrder
    {
        ABCD,
        CDBA,
        BADC,
        DCBA
    }

    public enum LongByteOrder
    {
        ABCDEFGH,
        GHEFCDAB,
        BADCFEHG,
        HGFEDCBA
    }
}
