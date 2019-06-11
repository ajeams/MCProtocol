using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class CommunicationLog
    {
        public List<byte> WritedBytes { get; }
        public List<byte> ReadedBytes { get; }

        public CommunicationLog()
        {
            this.WritedBytes = new List<byte>();
            this.ReadedBytes = new List<byte>();
        }

    }

}
