using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitLongArray : BitNumberArray<BitLong, long>
    {
        public BitLongArray(int length)
            :base(length, (i)=> new BitLong())
        {

        }

        protected override long GetValue(BitLong bitNumber)
        {
            return bitNumber.Value;
        }

        protected override void SetValue(BitLong bitNumber, long value)
        {
            bitNumber.Value = value;
        }

    }

}

