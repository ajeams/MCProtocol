using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCFunctionBatchWriteRequest : MCFunctionRequest
    {
        public DeviceCode DeviceCode { get; set; } = DeviceCode.None;
        public ushort Offset { get; set; } = 0;
        public ushort[] Data { get; set; } = null;

        public MCFunctionBatchWriteRequest()
        {

        }

        public override void Write(MCDataProcessor target)
        {
            var data = this.Data;
            var count = data.Length;

            if (target.DataCode == CommunicationDataCode.BINARY)
            {
                target.WriteUShort(this.Offset);
                target.WriteByte(0);
                DeviceCodeSerializer.Write(this.DeviceCode, target);
            }
            else if (target.DataCode == CommunicationDataCode.ASCII)
            {
                DeviceCodeSerializer.Write(this.DeviceCode, target);
                target.WriteUShort(this.Offset);
                target.WriteByte(0);
            }

            target.WriteUShort((ushort)count);

            for (int i = 0; i < count; i++)
            {
                target.WriteUShort(data[i]);
            }

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
                this.Offset = source.ReadUShort();
                source.ReadByte();
            }

            int count = source.ReadUShort();
            var data = this.Data = new ushort[count];

            for (int i = 0; i < count; i++)
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
