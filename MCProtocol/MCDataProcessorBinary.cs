using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCDataProcessorBinary : MCDataProcessor
    {
        public MCDataProcessorBinary(Stream stream)
            : base(stream)
        {

        }

        public override int LengthPerByte { get { return 1; } }

        public override CommunicationDataCode DataCode { get { return CommunicationDataCode.BINARY; } }

    }

}
