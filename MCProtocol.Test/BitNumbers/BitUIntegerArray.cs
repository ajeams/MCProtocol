using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitUIntegerArray : BitNumberArray<BitUInteger, uint>
    {
        public BitUIntegerArray(int length)
            :base(length, (i)=> new BitUInteger())
        {

        }

        protected override uint GetValue(BitUInteger bitNumber)
        {
            return bitNumber.Value;
        }

        protected override void SetValue(BitUInteger bitNumber, uint value)
        {
            bitNumber.Value = value;
        }

    }

}

