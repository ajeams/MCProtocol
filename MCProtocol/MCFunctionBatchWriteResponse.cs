using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCFunctionBatchWriteResponse : MCFunctionResponse
    {
        public MCFunctionBatchWriteRequest Request { get; }

        public MCFunctionBatchWriteResponse(MCFunctionBatchWriteRequest request)
        {
            this.Request = request;
        }

        public override void Write(MCDataProcessor target)
        {

        }

        public override void Read(MCDataProcessor source)
        {

        }

        public override ushort GetSubCommandCode()
        {
            return 0;
        }

    }

}
