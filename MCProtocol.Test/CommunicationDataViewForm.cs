using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class CommunicationDataViewForm : OptimizedForm
    {
        private readonly MCObject MCObject = null;

        public CommunicationDataViewForm(MCObject mcObject)
        {
            this.MCObject = mcObject;

            this.SuspendLayout();

            this.ResumeLayout(false);
        }

    }

}
