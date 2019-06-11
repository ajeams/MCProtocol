using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test.BitNumbers
{
    public abstract class BitNumber
    {
        public int Raw32 { get { return this.ToInt32(); } set { this.FromInt32(value); } }
        public long Raw64 { get { return this.ToInt64(); } set { this.FromInt64(value); } }

        public bool this[int index]
        {
            get
            {
                return this.GetBit(index);
            }

            set
            {
                this.SetBit(index, value);
            }

        }

        public bool this[Enum e]
        {
            get
            {
                return this.GetBit(e);
            }

            set
            {
                this.SetBit(e, value);
            }

        }


        public bool GetBit(Enum e)
        {
            return this.GetBit(e.GetHashCode());
        }

        public void SetBit(Enum e, bool value)
        {
            this.SetBit(e.GetHashCode(), value);
        }


        public bool GetBit(int index)
        {
            long v = 1L << index;
            long raw = this.Raw64;

            return (raw & v) == v;
        }

        public void SetBit(int index, bool value)
        {
            long v = 1L << index;

            if (value)
            {
                this.Raw64 |= (long)v;
            }
            else
            {
                this.Raw64 &= (long)~v;
            }

        }


        public abstract int ToInt32();

        public abstract void FromInt32(int value);

        public abstract long ToInt64();

        public abstract void FromInt64(long value);

        public abstract double ToPrecision(double factor);

        public abstract void FromPrecision(double value, double factor);

    }

}
