using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCMemoryMap
    {
        private Dictionary<DeviceCode, MCMemoryPage> Map { get; }

        public MCMemoryMap()
        {
            this.Map = new Dictionary<DeviceCode, MCMemoryPage>();

            foreach (var code in Enum.GetValues(typeof(DeviceCode)) as DeviceCode[])
            {
                this.Map[code] = new MCMemoryPage();
            }

        }

        private ushort EnsureAddress(int address)
        {
            try
            {
                return Convert.ToUInt16(address);
            }
            catch (Exception)
            {
                throw new MCMemoryException("Memory Address(" + address.ToString("X4") + ") has out of range", MCErrorCode.AddressOutOfRange);
            }

        }

        public void Set(DeviceCode code, ushort offset, ushort[] values)
        {
            var page = this.Map[code];

            for (int i = 0; i < values.Length; i++)
            {
                var address = this.EnsureAddress(offset + i);
                page[address] = values[i];
            }

        }

        public ushort[] Get(DeviceCode code, ushort offset, ushort count)
        {
            var page = this.Map[code];
            var values = new ushort[count];

            for (int i = 0; i < count; i++)
            {
                var address = this.EnsureAddress(offset + i);
                values[i] = page[address];
            }

            return values;
        }

    }

}
