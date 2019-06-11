using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class SimpleEnumerator<T> : IEnumerator<T>
    {
        private Func<int, T> ValueProvider { get; }

        private T _Current = default(T);
        private int Index = 0;
        private int Count = 0;

        public T Current
        {
            get { return this._Current; }
        }

        public SimpleEnumerator(Func<int, T> valueProvider, int count)
        {
            this.ValueProvider = valueProvider;
            this.Count = count;
        }

        ~SimpleEnumerator()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        object System.Collections.IEnumerator.Current
        {
            get { return this._Current; }
        }

        public bool MoveNext()
        {
            if (this.Index >= this.Count)
            {
                this._Current = default;
                return false;
            }
            else
            {
                this._Current = this.ValueProvider(this.Index);
                this.Index++;
            }

            return true;
        }

        public void Reset()
        {
            this.Index = 0;
            this._Current = default;
        }

    }

}
