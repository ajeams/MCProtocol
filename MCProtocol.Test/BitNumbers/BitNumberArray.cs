using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public interface BitNumberArray
    {
        int Length { get; }
        BitNumber GetBitNumber(int index);
        BitNumber[] ToBitNumbers();
    }

    public abstract class BitNumberArray<T, V> : BitNumberArray, IEnumerable<T> where T : BitNumber
    {
        private readonly T[] Values = null;
        public int Length { get { return this.Values.Length; } }

        int BitNumberArray.Length { get { return this.Length; } }

        public V this[int index]
        {
            get
            {
                return this.GetValue(index);
            }

            set
            {
                this.SetValue(index, value);
            }


        }

        public BitNumberArray(int length, Func<int, T> initializer)
        {
            this.Values = new T[length];

            for (int i = 0; i < length; ++i)
            {
                this.Values[i] = initializer(i);
            }

        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SimpleEnumerator<T>((index) => this.Values[index], this.Values.Length);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        
        BitNumber BitNumberArray.GetBitNumber(int index)
        {
            return this.Values[index];
        }

        public T GetBitNumber(int index)
        {
            return this.Values[index];
        }

        BitNumber[] BitNumberArray.ToBitNumbers()
        {
            return (T[])this.Values.Clone();
        }
        public T[] ToBitNumbers()
        {
            return (T[])this.Values.Clone();
        }

        public V[] ToValues()
        {
            V[] values = new V[this.Length];

            for (int i = 0; i < values.Length; ++i)
            {
                values[i] = this[i];
            }

            return values;
        }

        public V GetValue(int index)
        {
            return this.GetValue(this.Values[index]);
        }

        public void SetValue(int index, V value)
        {
            this.SetValue(this.Values[index], value);
        }

        public void Fill(V value)
        {
            for (int i = 0; i < this.Length; ++i)
            {
                this[i] = value;
            }

        }

        protected abstract V GetValue(T bitNumber);
        protected abstract void SetValue(T bitNumber, V value);

        public int[] ToRaw32()
        {
            int[] raw = new int[this.Values.Length];
            this.ToRaw32(0, raw, 0, raw.Length);
            return raw;
        }

        public void ToRaw32(int srcOffset, int[] raw, int dstOffset, int length)
        {
            for (int i = 0; i < length; ++i)
            {
                raw[i + dstOffset] = this.Values[i + srcOffset].Raw32;
            }

        }

        public void FromRaw32(int[] raw)
        {
            this.FromRaw32(0, raw, 0, raw.Length);
        }

        public void FromRaw32(int dstOffset, int[] raw, int srcOffset, int length)
        {
            for (int i = 0; i < length; ++i)
            {
                this.Values[i + dstOffset].Raw32 = raw[i + srcOffset];
            }

        }

        public void FromRaw32(BitNumberArray array)
        {
            this.FromRaw32(0, array, 0, array.Length);
        }

        public void FromRaw32(int dstOffset, BitNumberArray array, int srcOffset, int length)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                this.Values[i + dstOffset].Raw32 = array.GetBitNumber(i + srcOffset).Raw32;
            }

        }

    }

}
