using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace GazeInfo2
{
    static class Program
    {
        private static string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"QuickLink2DotNet\qlsettings.txt");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            int deviceID = QLHelper.ConsoleInteractive_Initialize(filename);
            if (deviceID < 0)
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return;
            }

            Application.Run(new MainForm(deviceID));
        }
    }
}