using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCLogEvent : EventArgs
    {
        public List<string> Layers { get; }

        public MCLogEvent()
        {
            this.Layers = new List<string>();
        }

    }

}
