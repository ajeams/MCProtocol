using Giselle.Commons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCProtocol.Test
{
    public abstract class MCForm : OptimizedForm
    {
        private ListBox _ListBox = null;
        protected ListBox ListBox { get { return this._ListBox; } }

        private ComboBox _CommandTextBox = null;
        protected ComboBox CommandTextBox { get { return this._CommandTextBox; } }

        private CommSetupGroupBox _CommunicationSetupGroupBox = null;
        protected CommSetupGroupBox CommunicationSetupGroupBox { get { return this._CommunicationSetupGroupBox; } }

        private MCObject MCObject = null;

        private List<string> LogQueue = null;
        private Timer Timer = null;

        public MCForm()
        {
            this.SuspendLayout();

            this.StartPosition = FormStartPosition.CenterScreen;

            this._ListBox = new ListBox();
            this.Controls.Add(this.ListBox);

            var commandTextBox = this._CommandTextBox = new ComboBox();
            commandTextBox.KeyDown += this.OnCommandTextBoxKeyDown;
            this.Controls.Add(commandTextBox);

            var communicationSetupGroupBox = this._CommunicationSetupGroupBox = this.CreateCommSetupGroupBox();
            communicationSetupGroupBox.ApplyClick += this.OnCommunicationSetupGroupBoxApplyClick;
            this.Controls.Add(communicationSetupGroupBox);

            this.ResumeLayout(false);

            this.ClientSize = new Size(600, 480);

            this.LogQueue = new List<string>();
            this.Timer = new Timer();
            this.Timer.Tick += this.OnTimerTick;
            this.Timer.Interval = 50;
            this.Timer.Start();

            var mcObject = this.CreateMCObject();
            mcObject.Protocol.DataCode = CommunicationDataCode.BINARY;
            mcObject.Logger.MessageLog += this.OnMCMesssageLog;
            mcObject.Logger.CommunicationLog += this.OnMCCommunicationLog;
            this.MCObject = mcObject;

        }

        protected abstract CommSetupGroupBox CreateCommSetupGroupBox();

        private string GetLayerToString(MCLogEvent e)
        {
            var layers = e.Layers;

            if (layers.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                return EnumerableUtils.Join(e.Layers, "", o => $"[{o}]") + " ";
            }

        }

        private void OnMCCommunicationLog(object sender, MCCommunicationLogEventArgs e)
        {
            var prefix = this.GetLayerToString(e);
            var builder = new StringBuilder();
            builder.AppendLine(prefix + "Comm");
            builder.AppendLine(prefix + "W : " + string.Join(",", e.Log.WritedBytes.Select(x => x.ToString("X2"))));
            builder.AppendLine(prefix + "R : " + string.Join(",", e.Log.ReadedBytes.Select(x => x.ToString("X2"))));

            lock (this.LogQueue)
            {
                this.LogQueue.Add(builder.ToString());
            }

        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            string[] logs = null;

            lock (this.LogQueue)
            {
                logs = this.LogQueue.ToArray();
                this.LogQueue.Clear();
            }

            if (logs.Length > 0)
            {
                var listBox = this.ListBox;
                listBox.BeginUpdate();

                var items = listBox.Items;
                listBox.Items.AddRange(logs);

                while (items.Count > 9999)
                {
                    listBox.Items.RemoveAt(0);
                }

                listBox.EndUpdate();
                listBox.SelectedIndex = listBox.Items.Count - 1;

                for (int i = 0; i < logs.Length; i++)
                {
                    var m = logs[i];
                    Console.ForegroundColor = this.GetConsoleColor(m);
                    Console.WriteLine(m);
                }

            }

        }

        private ConsoleColor[] CommConsoleColors = new ConsoleColor[] { ConsoleColor.Magenta, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.White };

        protected virtual ConsoleColor GetConsoleColor(string m)
        {
            if (m.Contains("Get") == true || m.Contains("Set") == true)
            {
                return ConsoleColor.Yellow;
            }

            var csPrefix = "[CS";
            var csSuffix = "]";
            var si = m.IndexOf(csPrefix);

            if (si > -1)
            {
                var ei = m.IndexOf(csSuffix, si);
                var s = m.Substring(si + csPrefix.Length, ei - si - csPrefix.Length);
                var index = NumberUtils.ToInt(s);

                return this.CommConsoleColors[index % this.CommConsoleColors.Length];
            }

            return ConsoleColor.Gray;
        }

        private void OnMCMesssageLog(object sender, MCMessageLogEventArgs e)
        {
            lock (this.LogQueue)
            {
                this.LogQueue.Add(this.GetLayerToString(e) + e.Message);
            }

        }

        private void OnCommandTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }


            var commandTextBox = this.CommandTextBox;
            var text = commandTextBox.Text;

            if (string.IsNullOrWhiteSpace(text) == true)
            {
                return;
            }

            var command = CommandParser.Parse(text);

            if (command == null)
            {
                return;
            }

            var mcObject = this.MCObject;

            if (mcObject.ExecuteCommand(command) == false)
            {
                return;
            }

            commandTextBox.Text = string.Empty;

            var items = commandTextBox.Items;
            items.Insert(0, text);

            if (items.Count > 10)
            {
                items.RemoveAt(items.Count - 1);
            }

        }

        private void OnCommunicationSetupGroupBoxApplyClick(object sender, EventArgs e)
        {
            try
            {
                var communicationSetupGroupBox = this.CommunicationSetupGroupBox;
                var value = communicationSetupGroupBox.GetValue();

                var mcObject = this.MCObject;
                mcObject.Stop();

                mcObject.Bind(value);
                mcObject.Start();
            }
            catch (ControlInvalidatedException ex)
            {
                var control = ex.Control;

                if (control != null)
                {
                    control.Select();
                }

                return;
            }

        }

        protected abstract MCObject CreateMCObject();

        protected override Dictionary<Control, Rectangle> GetPreferredBounds(Rectangle layoutBounds)
        {
            var map = base.GetPreferredBounds(layoutBounds);

            var bottomSize = new Size(layoutBounds.Width, 90);
            int bottomTop = layoutBounds.Bottom - bottomSize.Height;

            var communicationSetupGroupBox = this.CommunicationSetupGroupBox;
            map[communicationSetupGroupBox] = new Rectangle(new Point(layoutBounds.Left, bottomTop), bottomSize);

            var commandTextBox = this.CommandTextBox;
            map[commandTextBox] = DrawingUtils.PlaceByDirection2(map[communicationSetupGroupBox], new Size(layoutBounds.Width, 20), PlaceDirection.Top);

            var list = this.ListBox;
            map[list] = new Rectangle(new Point(layoutBounds.Left, layoutBounds.Top), new Size(layoutBounds.Width, map[commandTextBox].Top - 10 - layoutBounds.Top));

            return map;
        }

    }

}
