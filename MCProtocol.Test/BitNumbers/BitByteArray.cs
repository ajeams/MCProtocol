using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitByteArray : BitNumberArray<BitByte, byte>
    {
        public BitByteArray(int length)
            :base(length, (i)=> new BitByte())
        {

        }

        protected override byte GetValue(BitByte bitNumber)
        {
            return bitNumber.Value;
        }

        protected override void SetValue(BitByte bitNumber, byte value)
        {
            bitNumber.Value = value;
        }

    }

}

