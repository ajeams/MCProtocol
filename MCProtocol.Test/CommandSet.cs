using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class CommandSet : Command
    {
        public DeviceCode DeviceCode { get; set; } = DeviceCode.None;
        public ushort Offset { get; set; } = 0;
        public ushort[] Values { get; set; } = null;

        public CommandSet()
        {

        }

        public override void Execute(MCObject mcObject)
        {
            mcObject.SetMemoryRequest(this.DeviceCode, this.Offset, this.Values);
        }

    }

}
