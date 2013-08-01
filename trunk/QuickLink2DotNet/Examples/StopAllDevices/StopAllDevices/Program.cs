#region License

/* StopAllDevices: Stops all eye tracker devices connected to the system.
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
using System.IO;
using System.Linq;
using System.Text;
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
                    if (error == QLError.QL_ERROR_OK)
                    {
                        Console.WriteLine("  ID: {0}; Model: {1}; Serial: {2}", deviceID, info.modelName, info.serialNumber);
                    }
                }
                Console.WriteLine();

                for (int i = 0; i < deviceIDs.Length; i++)
                {
                    int deviceID = deviceIDs[i];
                    try
                    {
                        QLHelper.LoadDevicePasswordFromFile(deviceID, filename);
                    }
                    catch (QLErrorException e)
                    {
                        if (e.QLError == QLError.QL_ERROR_INVALID_PATH || e.QLError == QLError.QL_ERROR_NOT_FOUND)
                        {
                            Console.WriteLine("Device password not found.");
                            QLHelper.ConsoleInteractive_SetDevicePassword(deviceID, filename);
                        }
                        else if (e.QLError == QLError.QL_ERROR_INVALID_PASSWORD)
                        {
                            Console.WriteLine("Device password is invalid.");
                            QLHelper.ConsoleInteractive_SetDevicePassword(deviceID, filename, true);
                        }
                        else
                        {
                            throw;
                        }
                    }

                    QLError error = QuickLink2API.QLDevice_Stop(deviceID);
                    if (error == QLError.QL_ERROR_OK)
                    {
                        Console.WriteLine("Device {0} stopped.", deviceID);
                    }
                    else
                    {
                        Console.WriteLine("Performing QLDevice_Stop() on device {0} failed with error code {1}.", deviceID, error.ToString());
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}