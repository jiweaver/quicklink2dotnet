#region License

/* SetDevicePassword: Saves the device password to a settings file so that it
 * can be automatically loaded by other programs in the future.
 *
 * Copyright (c) 2011-2013 Justin Weaver
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

/* $Id: MainForm.cs 38 2011-05-09 01:07:39Z piranther $
 *
 * Description: Saves the device password to a settings file so that it
 * can be automatically loaded by other programs in the future.
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using QuickLink2DotNet;

namespace SetupDevicePassword
{
    class Program
    {
        private static string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"QuickLink2DotNet\qlsettings.txt");

        static void Main(string[] args)
        {
            int deviceID = QLHelper.ConsoleInteractive_GetDeviceID();

            if (deviceID >= 0)
            {
                QLHelper.ConsoleInteractive_SetDevicePassword(deviceID, filename);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}