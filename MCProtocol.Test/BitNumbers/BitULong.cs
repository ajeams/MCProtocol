using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test.BitNumbers
{
    public class BitULong : BitNumberQWord
    {
        public virtual ulong Value { get; set; }

        public override int ToInt32()
        {
            return (int) this.Value;
        }

        public override void FromInt32(int value)
        {
            this.Value = (ulong)value;
        }

        public override long ToInt64()
        {
            return (long)this.Value;
        }

        public override void FromInt64(long value)
        {
            this.Value = (ulong)value;
        }

        public override double ToPrecision(double factor)
        {
            return this.Value / factor;
        }

        public override void FromPrecision(double value, double factor)
        {
            this.Value = (ulong)(value * factor);
        }

    }

}
