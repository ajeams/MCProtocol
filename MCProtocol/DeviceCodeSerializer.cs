using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol
{
    public static class DeviceCodeSerializer
    {
        public static void Write(DeviceCode code, MCDataProcessor target)
        {
            if (target.DataCode == CommunicationDataCode.BINARY)
            {
                target.WriteByte((byte)code);
            }
            else if (target.DataCode == CommunicationDataCode.ASCII)
            {
                bool prev = target.IgnoreConvert;
                try
                {
                    target.IgnoreConvert = true;
                    var bytes = Encoding.Default.GetBytes(code.ToString().PadRight(2, '*'));

                    target.WriteBytes(bytes);
                }
                finally
                {
                    target.IgnoreConvert = prev;
                }

            }

        }

        public static DeviceCode Read(MCDataProcessor source)
        {
            if (source.DataCode == CommunicationDataCode.BINARY)
            {
                var code = (DeviceCode)source.ReadByte();
                return code;
            }
            else if (source.DataCode == CommunicationDataCode.ASCII)
            {
                bool prev = source.IgnoreConvert;

                try
                {
                    source.IgnoreConvert = true;
                    var bytes = source.ReadBytes(2);
                    var toString = Encoding.Default.GetString(bytes);

                    return Enum.TryParse(toString.Replace("*", ""), out DeviceCode code) ? code : DeviceCode.None;
                }
                finally
                {
                    source.IgnoreConvert = prev;
                }

            }

            return DeviceCode.None;
        }

    }

}
