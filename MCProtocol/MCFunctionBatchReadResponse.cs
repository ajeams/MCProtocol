using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCFunctionBatchReadResponse : MCFunctionResponse
    {
        public MCFunctionBatchReadRequest Request { get; }
        public ushort[] Data { get; set; } = null;

        public MCFunctionBatchReadResponse(MCFunctionBatchReadRequest request)
        {
            this.Request = request;
        }

        public override void Write(MCDataProcessor target)
        {
            var data = this.Data;

            for (int i = 0; i < data.Length; i++)
            {
                target.WriteUShort(data[i]);
            }

        }

        public override void Read(MCDataProcessor source)
        {
            var length = this.Request.Count;
            var data = this.Data = new ushort[length];

            for (int i = 0; i < length; i++)
            {
                data[i] = source.ReadUShort();
            }

        }

        public override ushort GetSubCommandCode()
        {
            return 0;
        }

    }

}
