using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCFunctionBatchReadRequest : MCFunctionRequest
    {
        public DeviceCode DeviceCode { get; set; } = DeviceCode.None;
        public ushort Offset { get; set; } = 0;
        public ushort Count { get; set; } = 0;

        public MCFunctionBatchReadRequest()
        {

        }

        public override void Write(MCDataProcessor target)
        {
            if (target.DataCode == CommunicationDataCode.BINARY)
            {
                target.WriteUShort(this.Offset);
                target.WriteByte(0);
                DeviceCodeSerializer.Write(this.DeviceCode, target);
            }
            else if (target.DataCode == CommunicationDataCode.ASCII)
            {
                DeviceCodeSerializer.Write(this.DeviceCode, target);
                target.WriteByte(0);
                target.WriteUShort(this.Offset);
            }

            target.WriteUShort(this.Count);
        }

        public override void Read(MCDataProcessor source)
        {
            if (source.DataCode == CommunicationDataCode.BINARY)
            {
                this.Offset = source.ReadUShort();
                source.ReadByte();
                this.DeviceCode = DeviceCodeSerializer.Read(source);
            }
            else if (source.DataCode == CommunicationDataCode.ASCII)
            {
                this.DeviceCode = DeviceCodeSerializer.Read(source);
                source.ReadByte();
                this.Offset = source.ReadUShort();
            }

            this.Count = source.ReadUShort();
        }

        public override ushort GetSubCommandCode()
        {
            return 0;
        }

    }

}
