using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitBooleanArray : BitNumberArray<BitBoolean, bool>
    {
        public BitBooleanArray(int length)
            :base(length, (i)=> new BitBoolean())
        {

        }

        protected override bool GetValue(BitBoolean bitNumber)
        {
            return bitNumber.Value;
        }

        protected override void SetValue(BitBoolean bitNumber, bool value)
        {
            bitNumber.Value = value;
        }

    }

}

