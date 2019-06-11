using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public abstract class Command
    {
        public Command()
        {

        }

        public abstract void Execute(MCObject mcObject);

    }

}
