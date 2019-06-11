using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitUShortArray : BitNumberArray<BitUShort, ushort>
    {
        public BitUShortArray(int length)
            :base(length, (i)=> new BitUShort())
        {

        }

        protected override ushort GetValue(BitUShort bitNumber)
        {
            return bitNumber.Value;
        }

        protected override void SetValue(BitUShort bitNumber, ushort value)
        {
            bitNumber.Value = value;
        }

    }

}

