using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public abstract class MCDataProcessor : DataProcessor
    {
        public bool ForceBigEndian { get; set; }

        public bool IgnoreConvert { get; set; }

        public MCDataProcessor(Stream stream)
            : base(stream)
        {
            this.ForceBigEndian = false;
            this.IgnoreConvert = false;
        }

        public abstract CommunicationDataCode DataCode { get; }

        public override void FlipCheck(byte[] bytes)
        {
            if (this.ForceBigEndian == true)
            {
                var prev = this.LittleEndian;

                try
                {
                    this.LittleEndian = false;

                    base.FlipCheck(bytes);
                }
                finally
                {
                    this.LittleEndian = prev;
                }

            }
            else
            {
                base.FlipCheck(bytes);
            }

        }


        public abstract int LengthPerByte { get; }
    }

}
