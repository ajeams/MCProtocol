using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCPacket
    {
        public MCSubHeader SubHeader { get; set; } = null;
        public MCQHeader QHeader { get; set; } = null;
        public MCFunction Function { get; set; } = null;

        public MCPacket()
        {

        }

    }

}
