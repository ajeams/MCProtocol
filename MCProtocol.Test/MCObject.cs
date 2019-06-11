using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public abstract class MCObject
    {
        public MCProtocol Protocol { get; }
        public MCLogger Logger { get; }

        public MCObject()
        {
            this.Protocol = new MCProtocol();
            this.Logger = new MCLogger();
        }

        public abstract void Bind(MCCommSettings settings);

        public abstract void Start();

        public abstract void Stop();

        public abstract bool ExecuteCommand(Command command);

        public abstract void SetMemoryRequest(DeviceCode code, ushort offset, ushort[] values);

        public abstract void GetMemoryRequest(DeviceCode code, ushort offset, ushort count);
    }

}
