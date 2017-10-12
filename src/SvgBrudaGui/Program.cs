using System;
using System.Windows.Forms;

namespace SvgBrudaGui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var form = new FrmMain())
            {
                Application.Run(form);
            }
        }
    }
}
