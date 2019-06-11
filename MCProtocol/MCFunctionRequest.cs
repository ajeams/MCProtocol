using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public abstract class MCFunctionRequest : MCFunction
    {
        public MCFunctionRequest()
        {

        }

        public override PacketDirection GetDirection()
        {
            return PacketDirection.Request;
        }

    }

}
