using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public enum PacketDirection : byte
    {
        None = 0,
        Request = 1,
        Response = 2,
    }

}
