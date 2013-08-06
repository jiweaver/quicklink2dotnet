#region License

/* QuickLink2DotNet SetupDevicePassword Example: Displays a list of available
 * devices and prompts the user to select one.  Then prompts for the device's
 * password.  The password is saved in the file
 * "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlsettings.txt" for later
 * use.
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

namespace SetupDevicePassword
{
    class Program
    {
        private static string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"QuickLink2DotNet\qlsettings.txt");

        private static bool SavePasswordToFile(int deviceID, string password, string filename)
        {
            // Read the settings out the file (if it exists) into a new setting container.
            int settingsID = -1;
            QLError error = QuickLink2API.QLSettings_Load(filename, ref settingsID);
            if (error == QLError.QL_ERROR_INVALID_PATH)
            {
                // The file does not exist to load from; create a new settings container.
                error = QuickLink2API.QLSettings_Create(-1, out settingsID);
                if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("QLSettings_Create() returned {0}.", error.ToString());
                    return false;
                }
            }
            else if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_Load() returned {0}.", error.ToString());
                return false;
            }

            // Get the device's serial number.
            QLDeviceInfo devInfo;
            error = QuickLink2API.QLDevice_GetInfo(deviceID, out devInfo);
            if (error == QLError.QL_ERROR_INVALID_DEVICE_ID)
            {
                Console.WriteLine("Invalid device ID", "deviceID");
                return false;
            }
            else if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_GetInfo() returned {0}.", error.ToString());
                return false;
            }

            string passwordSettingName = "SN_" + devInfo.serialNumber;

            // Remove the password if it already exists in the container.
            error = QuickLink2API.QLSettings_RemoveSetting(settingsID, passwordSettingName);
            if (error != QLError.QL_ERROR_OK && error != QLError.QL_ERROR_NOT_FOUND)
            {
                Console.WriteLine("QLSettings_RemoveSetting() returned {0}.", error.ToString());
                return false;
            }

            // Add the password to the container.
            error = QuickLink2API.QLSettings_SetValueString(settingsID, passwordSettingName, password);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_SetValueString() returned {0}.", error.ToString());
                return false;
            }

            // Save the settings container to the file.
            error = QuickLink2API.QLSettings_Save(filename, settingsID);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_Save() returned {0}.", error.ToString());
                return false;
            }

            return true;
        }

        static void Main(string[] args)
        {
            int[] deviceIDs = QLHelper.GetDeviceIDs();
            int deviceID = QLHelper.ChooseDevice(deviceIDs);

            if (deviceID >= 0)
            {
                ConsoleKey keypress = ConsoleKey.Y;

                while (keypress == ConsoleKey.Y)
                {
                    QLDeviceInfo info;
                    QuickLink2API.QLDevice_GetInfo(deviceID, out info);
                    Console.Write("Enter password for device: ");
                    string password = Console.ReadLine();

                    QLError error = QuickLink2API.QLDevice_SetPassword(deviceID, password);
                    if (error == QLError.QL_ERROR_INVALID_PASSWORD)
                    {
                        Console.WriteLine("Password is invalid. Try again? (y/n): ");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        keypress = keyInfo.Key;
                        continue;
                    }
                    else if (error != QLError.QL_ERROR_OK)
                    {
                        Console.WriteLine("QLDevice_SetPassword() returned {0}.", error.ToString());
                        break;
                    }
                    else
                    {
                        SavePasswordToFile(deviceID, password, filename);
                        Console.WriteLine("New password saved.");
                        break;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}