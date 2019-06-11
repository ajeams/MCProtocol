using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCCommSettingsMaster : MCCommSettings
    {
        public string Hostname { get; set; } = null;
        public ushort Port { get; set; } = 0;

        public MCCommSettingsMaster()
        {

        }

    }

}
