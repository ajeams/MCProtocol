using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCCommunicationLogEventArgs : MCLogEvent
    {
        public CommunicationLog Log { get; }

        public MCCommunicationLogEventArgs(CommunicationLog log)
        {
            this.Log = log;
        }

    }

}
