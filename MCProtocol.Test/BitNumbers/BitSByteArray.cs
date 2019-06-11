using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitSByteArray : BitNumberArray<BitSByte, sbyte>
    {
        public BitSByteArray(int length)
            :base(length, (i)=> new BitSByte())
        {

        }

        protected override sbyte GetValue(BitSByte bitNumber)
        {
            return bitNumber.Value;
        }

        protected override void SetValue(BitSByte bitNumber, sbyte value)
        {
            bitNumber.Value = value;
        }

    }

}

