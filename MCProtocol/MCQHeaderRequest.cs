using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCQHeaderRequest : MCQHeader
    {
        public ushort Watchdog { get; set; } = 0;

        public MCQHeaderRequest()
        {

        }

        public override void WritePost(DataProcessor target)
        {
            target.WriteUShort(this.Watchdog);
        }

        public override void ReadPost(DataProcessor source)
        {
            this.Watchdog = source.ReadUShort();
        }

    }

}
