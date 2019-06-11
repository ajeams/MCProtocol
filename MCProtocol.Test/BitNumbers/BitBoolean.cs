using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test.BitNumbers
{
    public class BitBoolean : BitNumber
    {
        public virtual bool Value { get; set; }

        public override int ToInt32()
        {
            return this.Value ? 1 : 0;
        }

        public override void FromInt32(int value)
        {
            this.Value = value > 0 ? true : false;
        }

        public override long ToInt64()
        {
            return this.Value ? 1 : 0;
        }

        public override void FromInt64(long value)
        {
            this.Value = value > 0 ? true : false;
        }

        public override double ToPrecision(double factor)
        {
            return this.ToInt32() / factor;
        }

        public override void FromPrecision(double value, double factor)
        {
            this.Value = value > 0 ? true : false;
        }

    }

}
