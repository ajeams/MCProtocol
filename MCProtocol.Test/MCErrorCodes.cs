using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public enum MCErrorCode : ushort
    {
        None = 0x0000,
        UnkownRequest = 0x00F1,
        AddressOutOfRange = 0x00F2,
    }

}
