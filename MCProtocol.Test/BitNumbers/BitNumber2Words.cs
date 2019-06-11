using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitNumber2Words : BitInteger
    {
        public BitNumberWord UpperWord { get; }
        public BitNumberWord LowerWord { get; }

        public override int Value
        {
            get
            {
                int upper = this.UpperWord.Raw32 & 0xFFFF;
                int lower = this.LowerWord.Raw32 & 0xFFFF;

                return (upper << 16) | (lower << 00);
            }

            set
            {
                this.UpperWord.Raw32 = (short)((value >> 16) & 0xFFFF);
                this.LowerWord.Raw32 = (short)((value >> 00) & 0xFFFF);
            }

        }

        public BitNumber2Words(BitNumberWord upper, BitNumberWord lower)
        {
            this.UpperWord = upper;
            this.LowerWord = lower;
        }

    }

}
