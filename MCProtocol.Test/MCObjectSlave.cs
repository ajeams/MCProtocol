using Giselle.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCObjectSlave : MCObject
    {
        private MCMemoryMap _MemoryMap = null;
        public MCMemoryMap MemoryMap { get { return this._MemoryMap; } }

        private MCSlaveServer Server1 = null;
        private MCSlaveServer Server2 = null;

        private bool Started = false;
        private readonly object StateLock = new object();

        public MCObjectSlave()
        {
            this._MemoryMap = new MCMemoryMap();

            this.Server1 = new MCSlaveServer(this);
            this.Server1.Name = "Server1";

            this.Server2 = new MCSlaveServer(this);
            this.Server2.Name = "Server2";
        }

        public override void Bind(MCCommSettings settings)
        {
            var settings2 = (MCCommSettingsSlave)settings;
            this.Server1.Port = settings2.Port1;
            this.Server2.Port = settings2.Port2;
        }

        public override void SetMemoryRequest(DeviceCode code, ushort offset, ushort[] values)
        {
            this.MemoryMap.Set(code, offset, values);
            this.Logger.OnMessageLog("Set : " + code.ToString() + offset + ".." + values.Length + "=" + ObjectUtils.ToString(values));
        }

        public override void GetMemoryRequest(DeviceCode code, ushort offset, ushort count)
        {
            var values = this.MemoryMap.Get(code, offset, count);
            this.Logger.OnMessageLog("Get : " + code.ToString() + offset + ".." + count + "=" + ObjectUtils.ToString(values));
        }

        public override bool ExecuteCommand(Command command)
        {
            command.Execute(this);

            return true;
        }

        public override void Start()
        {
            lock (this.StateLock)
            {
                this.Stop();

                this.Started = true;

                this.Logger.OnMessageLog("Starting");

                this.Server1.Start();
                this.Server2.Start();

                this.Logger.OnMessageLog("Started");
            }

        }

        public override void Stop()
        {
            lock (this.StateLock)
            {
                if (this.Started == true)
                {
                    this.Logger.OnMessageLog("Stopping");

                    this.Server2.Stop();
                    this.Server2.Stop();

                    this.Logger.OnMessageLog("Stopped");

                    this.Started = false;
                }

            }

        }

    }

}
