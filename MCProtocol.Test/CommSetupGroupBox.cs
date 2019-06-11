using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCProtocol.Test
{
    public abstract class CommSetupGroupBox : OptimizedGroupBox
    {
        private Button ApplyButton = null;

        public event EventHandler ApplyClick = null;

        public CommSetupGroupBox()
        {
            this.SuspendLayout();
            var controls = this.Controls;

            var applyButton = this.ApplyButton = new Button();
            applyButton.Text = "적용";
            applyButton.Click += this.OnApplyButtonClick;
            controls.Add(applyButton);

            this.ResumeLayout(false);
        }

        public abstract MCCommSettings GetValue();

        private void OnApplyButtonClick(object sender, EventArgs e)
        {
            var handler = this.ApplyClick;

            if (handler != null)
            {
                handler(this, e);
            }

        }

        protected virtual List<LabeledControl> GetLayoutControls()
        {
            return new List<LabeledControl>();
        }

        protected ushort EnsureValidatedPort(LabeledTextBox labeledTextBox)
        {
            ushort port = 0;

            if (ushort.TryParse(labeledTextBox.TextBox.Text, out port) == true)
            {
                return port;
            }
            else
            {
                throw new ControlInvalidatedException(labeledTextBox);
            }

        }

        protected override Dictionary<Control, Rectangle> GetPreferredBounds(Rectangle layoutBounds)
        {
            var map = base.GetPreferredBounds(layoutBounds);

            var applyButton = this.ApplyButton;
            var applyButtonSize = new Size(80, layoutBounds.Height);
            var applyButtonBounds = map[applyButton] = new Rectangle(layoutBounds.Right - applyButtonSize.Width, layoutBounds.Top, applyButtonSize.Width, applyButtonSize.Height);

            int labeledControlLeft = layoutBounds.Left;
            int labeledControlWidth = applyButtonBounds.Left - 10 - layoutBounds.Left;

            var labeledControls = this.GetLayoutControls();

            for (int i = 0; i < labeledControls.Count; i++)
            {
                var labeledControl = labeledControls[i];
                labeledControl.Label.Width = 65;
                map[labeledControl] = new Rectangle(labeledControlLeft, layoutBounds.Top + (layoutBounds.Height - labeledControl.Height) * i, labeledControlWidth, labeledControl.Height);
            }

            return map;
        }

    }

}
