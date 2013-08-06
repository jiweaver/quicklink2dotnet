#region License

/* QuickLink2DotNet Example Helper: A class library containing some convenience
 * methods used in a number of the example programs to reduce code duplication.
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

/* $Id: QLTypes.cs 38 2011-05-09 01:07:39Z piranther $
 *
 * Author: Justin Weaver
 * Homepage: http://quicklinkapi4net.googlecode.com
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickLink2DotNet;

namespace QLExampleHelper
{
    public static class QLHelper
    {
        public static int[] GetDeviceIDs()
        {
            int numDevices = 1;
            int[] deviceIDs = new int[numDevices];

            QLError error = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIDs);

            if (error == QLError.QL_ERROR_BUFFER_TOO_SMALL)
            {
                // Array too small.  Try again with a properly sized array.
                deviceIDs = new int[numDevices];
                error = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIDs);
            }

            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_Enumerate() returned {0}.", error.ToString());
                return null;
            }

            return deviceIDs;
        }

        public static int ChooseDevice(int[] deviceIDs)
        {
            int deviceID = -1;

            if (deviceIDs == null || deviceIDs.Length == 0)
            {
                Console.WriteLine("No devices detected.");
                return -1;
            }

            Console.WriteLine("Found devices:");
            for (int i = 0; i < deviceIDs.Length; i++)
            {
                QLDeviceInfo info;
                QLError error = QuickLink2API.QLDevice_GetInfo(deviceIDs[i], out info);
                if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("  Error {0} while getting info for device {1}", error.ToString(), deviceIDs[i]);
                }
                else
                {
                    Console.WriteLine("  ID: {0}; Model: {1}; Serial: {2}", deviceIDs[i], info.modelName, info.serialNumber);
                }
            }

            Console.WriteLine();

            if (deviceIDs.Length == 1)
            {
                Console.WriteLine("Using detected device with ID: {0}.", deviceIDs[0]);
                return deviceIDs[0];
            }

            do
            {
                Console.Write("Enter the ID of the device to use (or enter q to quit): ");
                string answer = Console.ReadLine();

                if (answer.Equals("q") || answer.Equals("Q"))
                {
                    break;
                }
                else
                {
                    try
                    {
                        deviceID = Int32.Parse(answer);
                    }
                    catch (ArgumentException) { continue; }
                    catch (FormatException) { continue; }
                    catch (OverflowException) { continue; }
                }

                for (int i = 0; i < deviceIDs.Length; i++)
                {
                    if (deviceID == deviceIDs[i])
                    {
                        Console.WriteLine("Using detected device with ID: {0}.", deviceID);
                        return deviceID;
                    }
                }
            } while (deviceID < 0);

            return -1;
        }

        public static string ReadPasswordFromFile(int deviceID, string filename)
        {
            // Read the settings out of a file into a new setting container.
            int settingsID = -1;
            QLError error = QuickLink2API.QLSettings_Load(filename, ref settingsID);
            if (error == QLError.QL_ERROR_INVALID_PATH)
            {
                Console.WriteLine("The specified file does not exist", "filename");
                return null;
            }
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_Load() returned {0}.", error.ToString());
                return null;
            }

            // Get the device's serial number.
            QLDeviceInfo devInfo;
            error = QuickLink2API.QLDevice_GetInfo(deviceID, out devInfo);
            if (error == QLError.QL_ERROR_INVALID_DEVICE_ID)
            {
                Console.WriteLine("Invalid device ID", "deviceID");
                return null;
            }
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_GetInfo() returned {0}.", error.ToString());
                return null;
            }

            string passwordSettingName = "SN_" + devInfo.serialNumber;

            // Check for the device password already in settings and fetch its size.
            int passwordSettingSize;
            error = QuickLink2API.QLSettings_GetValueStringSize(settingsID, passwordSettingName, out passwordSettingSize);
            if (error == QLError.QL_ERROR_NOT_FOUND)
            {
                Console.WriteLine("The device password does not exist in the specified file", "filename");
                return null;
            }
            else if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_GetValueStringSize() returned {0}.", error.ToString());
                return null;
            }

            // Load the password from the settings file.
            System.Text.StringBuilder password = new System.Text.StringBuilder(passwordSettingSize);
            error = QuickLink2API.QLSettings_GetValueString(settingsID, passwordSettingName, passwordSettingSize, password);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_GetValueString() returned {0}.", error.ToString());
                return null;
            }

            // Return the password.
            return password.ToString();
        }
    }
}