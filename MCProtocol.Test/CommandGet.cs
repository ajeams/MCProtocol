using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class CommandGet : Command
    {
        public DeviceCode DeviceCode { get; set; } = DeviceCode.None;
        public ushort Offset { get; set; } = 0;
        public ushort Count { get; set; } = 0;

        public CommandGet()
        {

        }

        public override void Execute(MCObject mcObject)
        {
            mcObject.GetMemoryRequest(this.DeviceCode, this.Offset, this.Count);
        }

    }

}
