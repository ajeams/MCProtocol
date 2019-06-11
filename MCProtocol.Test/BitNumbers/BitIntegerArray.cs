using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitIntegerArray : BitNumberArray<BitInteger, int>
    {
        public BitIntegerArray(int length)
            :base(length, (i)=> new BitInteger())
        {

        }

        protected override int GetValue(BitInteger bitNumber)
        {
            return bitNumber.Value;
        }

        protected override void SetValue(BitInteger bitNumber, int value)
        {
            bitNumber.Value = value;
        }

    }

}

