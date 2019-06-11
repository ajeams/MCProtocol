using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCProtocol.Test
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var selectForm = new SelectModeForm();
            Application.Run(selectForm);

            var selectedForm = selectForm.SelectedForm;
            Application.Run(selectedForm);
        }

    }

}
