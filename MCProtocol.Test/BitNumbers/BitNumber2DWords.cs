using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitNumber2Dwords : BitLong
    {
        public BitNumberDWord UpperWord { get; }
        public BitNumberDWord LowerWord { get; }

        public override long Value
        {
            get
            {
                long upper = this.UpperWord.Raw32 & 0xFFFFFFFF;
                long lower = this.LowerWord.Raw32 & 0xFFFFFFFF;

                return (upper << 32) | (lower << 00);
            }

            set
            {
                this.UpperWord.Raw32 = (int)((value >> 32) & 0xFFFFFFFF);
                this.LowerWord.Raw32 = (int)((value >> 00) & 0xFFFFFFFF);
            }

        }

        public BitNumber2Dwords(BitNumberDWord upper, BitNumberDWord lower)
        {
            this.UpperWord = upper;
            this.LowerWord = lower;
        }

    }

}
