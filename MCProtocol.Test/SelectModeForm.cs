using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCProtocol.Test
{
    public class SelectModeForm : OptimizedForm
    {
        private List<Button> Buttons = null;

        private Form _SelectedForm = null;
        public Form SelectedForm { get { return this._SelectedForm; } }

        public SelectModeForm()
        {
            this.SuspendLayout();

            this.Text = "모드 선택";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            this.Buttons = new List<Button>();
            var list = new List<Tuple<string, Type>>();
            list.Add(new Tuple<string, Type>("마스터", typeof(MCFormMaster)));
            list.Add(new Tuple<string, Type>("슬레이브", typeof(MCFormSlave)));

            foreach (var tuple in list)
            {
                var button = new Button();
                button.Text = tuple.Item1;
                button.Tag = tuple.Item2;
                button.Click += this.OnModeButtonClick;

                this.Buttons.Add(button);
                this.Controls.Add(button);
            }

            this.ResumeLayout(false);

            this.ClientSize = new Size(330, 100);
        }

        private void OnModeButtonClick(object sender, EventArgs e)
        {
            var formType = (Type)((Button)sender).Tag;
            var form = (Form) formType.GetConstructor(new Type[0]).Invoke(new object[0]);

            this._SelectedForm = form;
            this.Close();
        }

        protected override Dictionary<Control, Rectangle> GetPreferredBounds(Rectangle layoutBounds)
        {
            var map = base.GetPreferredBounds(layoutBounds);

            var buttonSize = new Size((layoutBounds.Width - 10) / this.Buttons.Count, layoutBounds.Height);

            for (int i = 0; i < this.Buttons.Count; i++)
            {
                var button = this.Buttons[i];
                button.Location = new Point(layoutBounds.Left + (buttonSize.Width + 10) * i, layoutBounds.Top);
                button.Size = buttonSize;
            }

            return map;
        }

    }

}
