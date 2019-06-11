using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCProtocol.Test
{
    public class CommSetupGroupBoxSlave : CommSetupGroupBox
    {
        private LabeledTextBox Port1Control = null;
        private LabeledTextBox Port2Control = null;

        public CommSetupGroupBoxSlave()
        {
            this.SuspendLayout();
            var controls = this.Controls;

            this.Text = "슬레이브 통신 설정";

            var port1Control = this.Port1Control = new LabeledTextBox();
            port1Control.Label.Text = "Port1";
            port1Control.TextBox.Text = "6400";
            controls.Add(port1Control);

            var port2Control = this.Port2Control = new LabeledTextBox();
            port2Control.Label.Text = "Port2";
            port2Control.TextBox.Text = "6401";
            controls.Add(port2Control);

            this.ResumeLayout(false);
        }

        protected override List<LabeledControl> GetLayoutControls()
        {
            var list = base.GetLayoutControls();
            list.Add(this.Port1Control);
            list.Add(this.Port2Control);

            return list;
        }

        public override MCCommSettings GetValue()
        {
            var value = new MCCommSettingsSlave();
            value.Port1 = this.EnsureValidatedPort(this.Port1Control);
            value.Port2 = this.EnsureValidatedPort(this.Port2Control);

            return value;
        }

    }

}
