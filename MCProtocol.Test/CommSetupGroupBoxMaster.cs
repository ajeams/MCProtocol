using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCProtocol.Test
{
    public class CommSetupGroupBoxMaster : CommSetupGroupBox
    {
        private LabeledTextBox HostnameControl = null;
        private LabeledTextBox PortControl = null;

        public CommSetupGroupBoxMaster()
        {
            this.SuspendLayout();
            var controls = this.Controls;

            this.Text = "마스터 통신 설정";

            var hostnameControl = this.HostnameControl = new LabeledTextBox();
            hostnameControl.Label.Text = "Hostname";
            hostnameControl.TextBox.Text = "127.0.0.1";
            controls.Add(hostnameControl);

            var portControl = this.PortControl = new LabeledTextBox();
            portControl.Label.Text = "Port";
            portControl.TextBox.Text = "6400";
            controls.Add(portControl);

            this.ResumeLayout(false);
        }

        protected override List<LabeledControl> GetLayoutControls()
        {
            var list = base.GetLayoutControls();
            list.Add(this.HostnameControl);
            list.Add(this.PortControl);

            return list;
        }

        public override MCCommSettings GetValue()
        {
            var value = new MCCommSettingsMaster();
            value.Hostname = this.HostnameControl.TextBox.Text;
            value.Port = this.EnsureValidatedPort(this.PortControl);

            return value;
        }

    }

}
