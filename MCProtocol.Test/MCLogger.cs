using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCLogger
    {
        public MCLogger Parent { get; }
        public string Name { get; set; } = null;

        public event EventHandler<MCMessageLogEventArgs> MessageLog;
        public event EventHandler<MCCommunicationLogEventArgs> CommunicationLog;

        public MCLogger()
            : this(null)
        {

        }

        public MCLogger(MCLogger parent)
        {
            this.Parent = parent;
        }

        protected virtual void AddLayer(MCLogEvent e)
        {
            var name = this.Name;

            if (name != null)
            {
                e.Layers.Insert(0, this.Name);
            }

        }

        protected virtual void OnMessageLog(MCMessageLogEventArgs e)
        {
            this.AddLayer(e);

            var parent = this.Parent;

            if (parent != null)
            {
                parent.OnMessageLog(e);
            }
            else
            {
                this.MessageLog?.Invoke(this, e);
            }

        }

        protected virtual void OnCommunicationLog(MCCommunicationLogEventArgs e)
        {
            this.AddLayer(e);

            var parent = this.Parent;

            if (parent != null)
            {
                parent.OnCommunicationLog(e);
            }
            else
            {
                this.CommunicationLog?.Invoke(this, e);
            }

        }

        public virtual void OnCommunicationLog(CommunicationLog log)
        {
            var e = new MCCommunicationLogEventArgs(log);
            this.OnCommunicationLog(e);
        }

        public virtual void OnMessageLog(string message)
        {
            var e = new MCMessageLogEventArgs(message);
            this.OnMessageLog(e);
        }

    }

}
