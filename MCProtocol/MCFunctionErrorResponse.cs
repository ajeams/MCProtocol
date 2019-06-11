using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCFunctionErrorResponse : MCFunctionResponse
    {
        public byte NetworkNumber { get; set; } = 0;
        public byte PLCNumber { get; set; } = 0;
        public ushort IONumber { get; set; } = 0;
        public byte StationNumber { get; set; } = 0;
        public ushort CommandCode { get; set; } = 0;
        public ushort SubCommandCode { get; set; } = 0;

        public MCFunctionErrorResponse()
        {

        }

        public override void Write(MCDataProcessor target)
        {
            target.WriteByte(this.NetworkNumber);
            target.WriteByte(this.PLCNumber);
            target.WriteUShort(this.IONumber);
            target.WriteByte(this.StationNumber);

            target.WriteUShort(this.CommandCode);
            target.WriteUShort(this.SubCommandCode);
        }

        public override void Read(MCDataProcessor source)
        {
            this.NetworkNumber = source.ReadByte();
            this.PLCNumber = source.ReadByte();
            this.IONumber = source.ReadUShort();
            this.StationNumber = source.ReadByte();

            this.CommandCode = source.ReadUShort();
            this.SubCommandCode = source.ReadUShort();
        }

        public override ushort GetSubCommandCode()
        {
            return 0;
        }

    }

}
