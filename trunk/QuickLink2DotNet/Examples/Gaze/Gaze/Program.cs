#region License

/* Gaze: Track the user's gaze, and display a red circle where they are
 * currently looking.
 *
 * Copyright © 2011-2013 Justin Weaver
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 * IN THE SOFTWARE.
 */

#endregion License

#region Header Comments

/* $Id: QLTypes.cs 38 2011-05-09 01:07:39Z piranther $
 *
 * Author: Justin Weaver
 * Homepage: http://quicklinkapi4net.googlecode.com
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using QuickLink2DotNet;
using QuickLink2DotNetHelper;

namespace Gaze
{
    class Program
    {
        #region Disable Close Button

        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern IntPtr RemoveMenu(IntPtr hMenu, uint nPosition, uint wFlags);

        private const uint SC_CLOSE = 0xF060;
        private const uint MF_GRAYED = 0x00000001;
        private const uint MF_BYCOMMAND = 0x00000000;

        private static void DisableCloseButton()
        {
            // Disable the close button, so the user must "hit a key to
            // continue."  This allows us to stop the eye tracker before
            // exiting.
            IntPtr hMenu = Process.GetCurrentProcess().MainWindowHandle;
            IntPtr hSystemMenu = GetSystemMenu(hMenu, false);
            EnableMenuItem(hSystemMenu, SC_CLOSE, MF_GRAYED);
            RemoveMenu(hSystemMenu, SC_CLOSE, MF_BYCOMMAND);
        }

        #endregion Disable Close Button

        private static void DoTask()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            QLHelper helper = QLHelper.ChooseDevice();
            if (helper == null)
            {
                return;
            }

            if (!helper.SetupPassword())
            {
                return;
            }

            if (!helper.SetupCalibration())
            {
                return;
            }

            // Start the device.
            QLError error = QuickLink2API.QLDevice_Start(helper.DeviceId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_Start() returned {0} for device {1}.", error.ToString(), helper.DeviceId);
                return;
            }
            Console.WriteLine("Device has been started.");
            Console.WriteLine();

            using (GazeForm mainForm = new GazeForm(helper.DeviceId))
            {
                mainForm.ShowDialog();
            }

            // Stop the device.
            error = QuickLink2API.QLDevice_Stop(helper.DeviceId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_Stop() returned {0} for device {1}.", error.ToString(), helper.DeviceId);
                return;
            }
            Console.WriteLine("Stopped Device.");
        }

        private static void Main(string[] args)
        {
            DisableCloseButton();

            DoTask();

            // Flush the input buffer.
            while (Console.KeyAvailable) { Console.ReadKey(true); }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}