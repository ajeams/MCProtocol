using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCProtocol.Test.BitNumbers
{
    public class BitNumberList : IEnumerable<BitNumber>
    {
        private readonly List<BitNumber> List = new List<BitNumber>();

        public int this[int index]
        {
            get
            {
                return this.GetRawValue(index);
            }

            set
            {
                this.SetRawValue(index, value);
            }

        }

        public int Count
        {
            get
            {
                return this.List.Count;
            }

        }

        public BitNumberList()
        {

        }

        public void Add(BitNumber bitNumber)
        {
            this.List.Add(bitNumber);
        }

        public void Add(BitNumberArray array)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                this.Add(array.GetBitNumber(i));
            }

        }

        public void Add(BitNumberList map)
        {
            for (int i = 0; i < map.Count; ++i)
            {
                this.Add(map.GetBitNumber(i));
            }

        }

        public BitNumber GetBitNumber(int index)
        {
            return this.List[index];
        }

        public int GetRawValue(int index)
        {
            return this.List[index].Raw32;
        }

        public void SetRawValue(int index, int value)
        {
            this.List[index].Raw32 = value;
        }

        public int[] ToRaw32()
        {
            int[] raw = new int[this.Count];
            this.ToRaw32(raw, 0, raw.Length);
            return raw;
        }

        public void ToRaw32(int[] raw, int offset, int length)
        {
            for (int i = 0; i < length; ++i)
            {
                raw[i + offset] = this[i];
            }

        }

        public void FromRaw32(int[] raw)
        {
            this.FromRaw32(raw, 0, raw.Length);
        }

        public void FromRaw32(int[] raw, int offset, int length)
        {
            for (int i = 0; i < length; ++i)
            {
                this[i] = raw[i + offset];
            }

        }

        public void ClearRaw32()
        {
            for (int i = 0; i < this.Count; ++i)
            {
                this[i] = 0;
            }

        }

        public IEnumerator<BitNumber> GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }

}
