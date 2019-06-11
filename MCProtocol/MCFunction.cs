using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public abstract class MCFunction
    {
        public MCFunction()
        {

        }

        public abstract void Write(MCDataProcessor target);

        public abstract void Read(MCDataProcessor source);

        public abstract ushort GetSubCommandCode();

        public abstract PacketDirection GetDirection();
    }

}
