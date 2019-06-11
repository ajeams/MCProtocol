using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitShortArray : BitNumberArray<BitShort, short>
    {
        public BitShortArray(int length)
            :base(length, (i)=> new BitShort())
        {

        }

        protected override short GetValue(BitShort bitNumber)
        {
            return bitNumber.Value;
        }

        protected override void SetValue(BitShort bitNumber, short value)
        {
            bitNumber.Value = value;
        }

    }

}

