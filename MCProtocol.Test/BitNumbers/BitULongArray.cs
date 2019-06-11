using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitULongArray : BitNumberArray<BitULong, ulong>
    {
        public BitULongArray(int length)
            :base(length, (i)=> new BitULong())
        {

        }

        protected override ulong GetValue(BitULong bitNumber)
        {
            return bitNumber.Value;
        }

        protected override void SetValue(BitULong bitNumber, ulong value)
        {
            bitNumber.Value = value;
        }

    }

}

