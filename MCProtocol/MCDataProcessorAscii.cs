using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public class MCDataProcessorAscii : MCDataProcessor
    {
        public MCDataProcessorAscii(Stream stream)
            : base(stream)
        {

        }

        public override int LengthPerByte { get { return 2; } }

        public override byte ReadByte()
        {
            if (this.IgnoreConvert == false)
            {
                var builder = new StringBuilder();
                builder.Append((char)base.ReadByte());
                builder.Append((char)base.ReadByte());
                var hex = builder.ToString();

                return byte.Parse(hex, NumberStyles.HexNumber, null);
            }
            else
            {
                return base.ReadByte();
            }

        }

        public override int Read(byte[] bytes, int offset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                bytes[offset + i] = this.ReadByte();
            }

            return count;
        }

        public override void WriteByte(byte value)
        {
            if (this.IgnoreConvert == false)
            {
                var hex = value.ToString("X2");
                base.WriteByte((byte)hex[0]);
                base.WriteByte((byte)hex[1]);
            }
            else
            {
                base.WriteByte(value);
            }

        }

        public override void Write(byte[] bytes, int offset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.WriteByte(bytes[offset + i]);
            }

        }

        public override CommunicationDataCode DataCode { get { return CommunicationDataCode.ASCII; } }
    }

}
