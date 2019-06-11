using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCMemoryPage
    {
        private ushort[] Page { get; }

        public MCMemoryPage()
        {
            this.Page = new ushort[ushort.MaxValue];
        }

        public ushort this[ushort offset]
        {
            get
            {
                return this.Page[offset];
            }

            set
            {
                this.Page[offset] = value;
            }

        }

    }

}
