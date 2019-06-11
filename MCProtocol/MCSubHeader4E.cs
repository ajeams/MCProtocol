using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCSubHeader4E : MCSubHeader
    {
        public ushort SerialNumber { get; set; } = 0;
        public ushort Reserved1 { get; set; } = 0;

        public MCSubHeader4E()
        {

        }

        public override void Write(DataProcessor target)
        {
            base.Write(target);

            target.WriteUShort(this.SerialNumber);
            target.WriteUShort(this.Reserved1);
        }

        public override void Read(DataProcessor source)
        {
            base.Read(source);

            this.SerialNumber = source.ReadUShort();
            this.Reserved1 = source.ReadUShort();
        }

    }

}
