using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCCommSettingsSlave : MCCommSettings
    {
        public ushort Port1 { get; set; } = 0;
        public ushort Port2 { get; set; } = 0;

        public MCCommSettingsSlave()
        {

        }

    }

}
