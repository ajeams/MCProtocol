using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test
{
    public class MCMessageLogEventArgs : MCLogEvent
    {
        public string Message { get; }

        public MCMessageLogEventArgs(string message)
        {
            this.Message = message;
        }

    }

}
