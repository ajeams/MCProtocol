using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCQHeaderResponse : MCQHeader
    {
        public ushort ResultCode { get; set; } = 0;

        public MCQHeaderResponse()
        {

        }

        public override void WritePost(DataProcessor target)
        {
            target.WriteUShort(this.ResultCode);
        }

        public override void ReadPost(DataProcessor source)
        {
            this.ResultCode = source.ReadUShort();
        }

    }

}
