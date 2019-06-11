using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test.BitNumbers
{
    public class BitSByte : BitNumberByte
    {
        public virtual sbyte Value { get; set; }

        public override int ToInt32()
        {
            return this.Value;
        }

        public override void FromInt32(int value)
        {
            this.Value = (sbyte)value;
        }

        public override long ToInt64()
        {
            return this.Value;
        }

        public override void FromInt64(long value)
        {
            this.Value = (sbyte)value;
        }

        public override double ToPrecision(double factor)
        {
            return this.Value / factor;
        }

        public override void FromPrecision(double value, double factor)
        {
            this.Value = (sbyte)(value * factor);
        }

    }

}
