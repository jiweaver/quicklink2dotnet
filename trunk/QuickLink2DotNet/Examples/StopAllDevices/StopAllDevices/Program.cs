#region License

/* QuickLink2DotNet StopAllDevices Example: Stops all the EyeTech eye tracker
 * devices on the system without prompting.  Requires password file.
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
using System.IO;
using System.Linq;
using System.Text;
using QLExampleHelper;
using QuickLink2DotNet;

namespace StopAllDevices
{
    class Program
    {
        private static string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"QuickLink2DotNet\qlsettings.txt");

        static void Main(string[] args)
        {
            int[] deviceIDs = QLHelper.GetDeviceIDs();

            if (deviceIDs.Length == 0)
            {
                Console.WriteLine("No eye trackers found.");
                return;
            }
            else
            {
                Console.WriteLine("Detected {0} devices:", deviceIDs.Length);
                for (int i = 0; i < deviceIDs.Length; i++)
                {
                    int deviceID = deviceIDs[i];

                    QLDeviceInfo info;
                    QLError error = QuickLink2API.QLDevice_GetInfo(deviceID, out info);
                    if (error != QLError.QL_ERROR_OK)
                    {
                        Console.WriteLine("QLDevice_GetInfo() returned {0} for device {1}.", error.ToString(), deviceID);
                        continue;
                    }

                    Console.WriteLine("  ID: {0}; Model: {1}; Serial: {2}", deviceID, info.modelName, info.serialNumber);
                }

                Console.WriteLine();

                for (int i = 0; i < deviceIDs.Length; i++)
                {
                    int deviceID = deviceIDs[i];

                    string password = QLHelper.ReadPasswordFromFile(deviceID, filename);
                    if (password == null)
                    {
                        Console.WriteLine("No password found for device {0}.", deviceID);
                        continue;
                    }

                    QLError error = QuickLink2API.QLDevice_SetPassword(deviceID, password);
                    if (error != QLError.QL_ERROR_OK)
                    {
                        Console.WriteLine("QLDevice_SetPassword() returned {0} for device {1}.", error.ToString(), deviceID);
                        continue;
                    }

                    error = QuickLink2API.QLDevice_Start(deviceID);
                    if (error != QLError.QL_ERROR_OK)
                    {
                        Console.WriteLine("QLDevice_Start() returned {0} for device {1}.", error.ToString(), deviceID);
                        continue;
                    }

                    error = QuickLink2API.QLDevice_Stop(deviceID);
                    if (error != QLError.QL_ERROR_OK)
                    {
                        Console.WriteLine("QLDevice_Stop() returned {0} for device {1}.", error.ToString(), deviceID);
                        continue;
                    }

                    Console.WriteLine("Device {0} stopped.", deviceID);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}