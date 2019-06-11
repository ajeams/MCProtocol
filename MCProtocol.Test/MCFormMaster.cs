using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCProtocol.Test
{
    public class MCFormMaster : MCForm
    {
        public MCFormMaster()
        {
            this.SuspendLayout();

            this.Text = "마스터";

            this.ResumeLayout(false);
        }

        protected override CommSetupGroupBox CreateCommSetupGroupBox()
        {
            return new CommSetupGroupBoxMaster();
        }

        protected override MCObject CreateMCObject()
        {
            return new MCObjectMaster();
        }

    }

}