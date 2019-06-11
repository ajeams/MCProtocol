using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test.BitNumbers
{
    public class BitPrecision : BitNumber
    {
        public BitNumber BaseNumber { get; }
        public double Factor { get; }

        public double Value
        {
            get
            {
                return this.BaseNumber.ToPrecision(this.Factor);
            }

            set
            {
                this.BaseNumber.FromPrecision(value, this.Factor);
            }

        }

        public BitPrecision(BitNumber baseNumber, int dps)
            :this(baseNumber, Math.Pow(10, dps))
        {

        }

        public BitPrecision(BitNumber baseNumber, double factor)
        {
            this.BaseNumber = baseNumber;
            this.Factor = factor;
        }

        public override int ToInt32()
        {
            return this.BaseNumber.Raw32;
        }

        public override void FromInt32(int value)
        {
            this.BaseNumber.Raw32 = value;
        }

        public override long ToInt64()
        {
            return this.BaseNumber.Raw64;
        }

        public override void FromInt64(long value)
        {
            this.BaseNumber.Raw64 = value;
        }

        public override double ToPrecision(double factor)
        {
            return this.Value / factor;
        }

        public override void FromPrecision(double value, double factor)
        {
            this.Value = (int) (value * factor);
        }

    }

}
