using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitPrecisionArray : BitNumberArray<BitPrecision, double>
    {
        public BitPrecisionArray(int length, int dps)
            : base(length, (i) => new BitPrecision(new BitInteger(), dps))
        {

        }

        public BitPrecisionArray(BitNumberArray bitNumbers, int dps)
            : base(bitNumbers.Length, (i) => new BitPrecision(bitNumbers.GetBitNumber(i), dps))
        {

        }

        public BitPrecisionArray(BitNumber[] bitNumbers, int dps)
            : base(bitNumbers.Length, (i) => new BitPrecision(bitNumbers[i], dps))
        {

        }

        protected override double GetValue(BitPrecision bitNumber)
        {
            return bitNumber.Value;
        }

        protected override void SetValue(BitPrecision bitNumber, double value)
        {
            bitNumber.Value = value;
        }

    }

}

