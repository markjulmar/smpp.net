using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EsmeGui
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
			try
			{
				Application.Run(new mainForm());
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception: " + ex.ToString());
			}
        }
    }
}
