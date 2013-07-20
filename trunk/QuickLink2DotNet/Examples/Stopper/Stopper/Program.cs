#region License

/* Stopper: Stops all eye tracker devices connected to the system.
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
 * Description: Stops all eye tracker devices connected to the system.
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickLink2DotNet;

namespace Stopper
{
    class Program
    {
        private static string dirname = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QuickLink2DotNet");

        // The file used to store the password.
        private static string filename_Password = System.IO.Path.Combine(dirname, "qlsettings.txt");

        static void Main(string[] args)
        {
            QLError qlerror = QLError.QL_ERROR_OK;

            // Enumerate the bus to find out which eye trackers are connected to the computer.
            const int bufferSize = 100;
            int[] deviceIds = new int[bufferSize];
            int numDevices = bufferSize;
            qlerror = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIds);

            if (qlerror != QLError.QL_ERROR_OK)
            {
                System.Console.WriteLine("QLDevice_Enumerate() failed with error code {0}.", qlerror);
            }
            else if (numDevices == 0)
            {
                System.Console.WriteLine("No devices present.");
            }
            else
            {
                // Stop each device.
                for (int x = 0; x < numDevices; x++)
                {
                    int devId = deviceIds[x];
                    System.Console.WriteLine("Attempting to stop device: {0}.", devId);

                    int deviceId = Initialize.QL2Initialize(filename_Password);
                    if (deviceId == 0)
                    {
                        System.Console.WriteLine("Failed to initialize device {0}.", devId);
                    }
                    else
                    {
                        qlerror = QuickLink2API.QLDevice_Start(deviceId);
                        if (qlerror != QLError.QL_ERROR_OK)
                        {
                            System.Console.WriteLine("QLDevice_Start() failed with error code {0}.", qlerror);
                        }
                        else
                        {
                            qlerror = QuickLink2API.QLDevice_Stop(devId);
                            if (qlerror != QLError.QL_ERROR_OK)
                            {
                                System.Console.WriteLine("QLDevice_Stop() failed with error code {0}.", qlerror);
                            }
                        }
                    }
                }
                System.Console.WriteLine("Done.  Press any key to exit.");
                System.Console.ReadKey();
            }
        }
    }
}