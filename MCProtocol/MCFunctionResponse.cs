using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public abstract class MCFunctionResponse : MCFunction
    {
        public MCFunctionResponse()
        {

        }

        public override PacketDirection GetDirection()
        {
            return PacketDirection.Response;
        }

    }

}
