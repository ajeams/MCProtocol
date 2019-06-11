using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public abstract class MCQHeader
    {
        public byte NetworkNumber { get; set; } = 0;
        public byte PLCNumber { get; set; } = 0;
        public ushort IONumber { get; set; } = 0;
        public byte StationNumber { get; set; } = 0;

        public MCQHeader()
        {

        }

        public abstract void WritePost(DataProcessor target);

        public abstract void ReadPost(DataProcessor source);

        public void WritePre(DataProcessor target)
        {
            target.WriteByte(this.NetworkNumber);
            target.WriteByte(this.PLCNumber);
            target.WriteUShort(this.IONumber);
            target.WriteByte(this.StationNumber);
        }

        public void ReadPre(DataProcessor source)
        {
            this.NetworkNumber = source.ReadByte();
            this.PLCNumber = source.ReadByte();
            this.IONumber = source.ReadUShort();
            this.StationNumber = source.ReadByte();
        }

    }

}
