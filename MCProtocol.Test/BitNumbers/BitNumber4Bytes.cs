using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitNumber4Bytes : BitInteger
    {
        public BitNumberByte Byte1 { get; }
        public BitNumberByte Byte2 { get; }
        public BitNumberByte Byte3 { get; }
        public BitNumberByte Byte4 { get; }

        public override int Value
        {
            get
            {
                int b1 = this.Byte1.Raw32 & 0xFF;
                int b2 = this.Byte2.Raw32 & 0xFF;
                int b3 = this.Byte3.Raw32 & 0xFF;
                int b4 = this.Byte4.Raw32 & 0xFF;

                return (b1 << 24) | (b2 << 16) | (b3 << 08) | (b4 << 00);
            }

            set
            {
                this.Byte1.Raw32 = (byte)((value >> 24) & 0xFFFF);
                this.Byte2.Raw32 = (byte)((value >> 16) & 0xFFFF);
                this.Byte3.Raw32 = (byte)((value >> 08) & 0xFFFF);
                this.Byte4.Raw32 = (byte)((value >> 00) & 0xFFFF);
            }

        }

        public BitNumber4Bytes(BitNumberByte byte1, BitNumberByte byte2, BitNumberByte byte3, BitNumberByte byte4)
        {
            this.Byte1 = byte1;
            this.Byte2 = byte2;
            this.Byte3 = byte3;
            this.Byte4 = byte4;
        }

    }

}
