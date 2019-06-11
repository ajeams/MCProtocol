using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCFormSlave : MCForm
    {
        public MCFormSlave()
        {
            this.SuspendLayout();

            this.Text = "슬레이브";

            this.ResumeLayout(false);
        }

        protected override CommSetupGroupBox CreateCommSetupGroupBox()
        {
            return new CommSetupGroupBoxSlave();
        }

        protected override MCObject CreateMCObject()
        {
            return new MCObjectSlave();
        }

    }

}