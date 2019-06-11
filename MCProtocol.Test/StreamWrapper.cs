using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class StreamWrapper : Stream
    {
        private readonly Stream BaseStream = null;


        private CommunicationLog _Log = null;
        public CommunicationLog Log { get { return this._Log; } }

        public StreamWrapper(Stream baseStream)
        {
            this.BaseStream = baseStream;

            this._Log = new CommunicationLog();
        }

        public override bool CanRead { get { return this.BaseStream.CanRead; } }

        public override bool CanSeek { get { return this.BaseStream.CanSeek; } }

        public override bool CanWrite { get { return this.BaseStream.CanWrite; } }

        public override void Flush()
        {
            this.BaseStream.Flush();
        }

        public override long Length { get { return this.BaseStream.Length; } }

        public override long Position
        {
            get
            {
                return this.BaseStream.Position;
            }

            set
            {
                this.BaseStream.Position = value;
            }

        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var len = this.BaseStream.Read(buffer, offset, count);

            for (int i = 0; i < len; i++)
            {
                this.Log.ReadedBytes.Add(buffer[offset + i]);
            }

            return len;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.BaseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.BaseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.BaseStream.Write(buffer, offset, count);

            for (int i = 0; i < count; i++)
            {
                this.Log.WritedBytes.Add(buffer[offset + i]);
            }

        }

    }

}
