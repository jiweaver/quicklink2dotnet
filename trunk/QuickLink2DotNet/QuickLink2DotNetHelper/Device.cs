#region License

/* QLHelper: A class library containing some helper methods for use with
 * QuickLink2DotNet.
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
using System.Linq;
using System.Text;
using QuickLink2DotNet;

namespace QuickLink2DotNetHelper
{
    public partial class QLHelper
    {
        /// <summary>
        /// Get the deviceIDs of every eye tracker device detected on the system.  When the method
        /// returns successfully, the <paramref name="deviceIds"/> array will be sized exactly to contain
        /// the number of devices detected.  So, for example, if there is one device detected then the
        /// array will have length 1; if 10 devices are detected then the array will have length 10; if
        /// no devices are detected then the array will have length 0.
        /// </summary>
        /// <param name="deviceIds">
        /// A reference to an uninitialized integer array which will contain the ID of every device
        /// detected on the system upon the method's successful return.
        /// </param>
        /// <returns>
        /// The success of the method.  Returns <see cref="QLError.QL_ERROR_OK"/> on success.
        /// </returns>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public static QLError DeviceEnumerate(out int[] deviceIds)
        {
            int numDevices = 1;
            deviceIds = new int[numDevices];

            QLError error = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIds);

            if (error == QLError.QL_ERROR_BUFFER_TOO_SMALL)
            {
                // Array too small.  Try again with a properly sized array.
                deviceIds = new int[numDevices];
                error = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIds);
            }

            if (numDevices == 0)
            {
                deviceIds = new int[0];
                return error;
            }

            for (int i = 0; i < numDevices; i++)
            {
                if (deviceIds[i] <= 0)
                {
                    Console.WriteLine("Illegal device ID detected in data returned from QLDevice_Enumerate()!  Position {0} contains ID {1}.", i, deviceIds[i]);
                    return QLError.QL_ERROR_INTERNAL_ERROR;
                }
            }

            return error;
        }

        /// <summary>
        /// Given an array populated with device IDs, print the ID number, model, and serial number of
        /// each device on the console--one device per line.  If an error is encountered, details are
        /// output to the console.  This can be used to show a list of devices so that the user may
        /// choose a specific one.
        /// </summary>
        /// <param name="deviceIds">
        /// An array containing the ID numbers of the devices to print information about.  These ID
        /// numbers are obtained by calling the function
        /// <see cref="QuickLink2DotNet.QuickLink2API.QLDevice_Enumerate" />.
        /// </param>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public static void PrintListOfDeviceInfo(int[] deviceIds)
        {
            if (deviceIds == null || deviceIds.Length == 0)
            {
                Console.WriteLine("No devices detected.");
            }

            Console.WriteLine("Detected {0} devices:{1}", deviceIds.Length, Environment.NewLine);

            for (int i = 0; i < deviceIds.Length; i++)
            {
                int deviceId = deviceIds[i];

                QLDeviceInfo info;
                QLError error = QuickLink2API.QLDevice_GetInfo(deviceId, out info);
                if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("  ID: {0}; QLDevice_GetInfo() returned {1}{2}", deviceId, error.ToString(), Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("  ID: {0}; Model: {1}; Serial: {2}{3}", deviceId, info.modelName, info.serialNumber, Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// Construct a <see cref="T:QuickLink2DotNetHelper.QLHelper" /> object using a specified device.  If an error is
        /// encountered, details are output to the console.
        /// </summary>
        /// <seealso cref="ChooseDevice" />
        /// <param name="deviceId">
        /// The ID for the device to use when creating the <see cref="T:QuickLink2DotNetHelper.QLHelper" /> object.  This ID is
        /// obtained by calling the function
        /// <see cref="QuickLink2DotNet.QuickLink2API.QLDevice_Enumerate" />.
        /// </param>
        /// <returns>
        /// A <see cref="T:QuickLink2DotNetHelper.QLHelper" /> object constructed using the chosen device.  Returns null if an
        /// error occurs, no devices are found, or the user opts to quit.
        /// </returns>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public static QLHelper FromDeviceId(int deviceId)
        {
            QLDeviceInfo info;
            QLError error = QuickLink2API.QLDevice_GetInfo(deviceId, out info);
            if (error != QLError.QL_ERROR_OK)
            {
                return null;
            }

            return new QLHelper(deviceId, info);
        }

        /// <summary>
        /// Detects all eye tracker devices on the system and lists them on the console, then prompts the
        /// user to choose a device.  A <see cref="T:QuickLink2DotNetHelper.QLHelper" /> object is then constructed using the
        /// chosen device.  If only one device is found, that device is used without prompting the user
        /// for input.  If an error is encountered, details are output to the console.
        /// </summary>
        /// <seealso cref="FromDeviceId" />
        /// <returns>
        /// A <see cref="T:QuickLink2DotNetHelper.QLHelper" /> object constructed using the chosen device.  Returns null if an
        /// error occurs, no devices are found, or the user opts to quit.
        /// </returns>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public static QLHelper ChooseDevice()
        {
            int[] deviceIds;

            QLError error = DeviceEnumerate(out deviceIds);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_Enumerate() returned {0}.", error.ToString());
                return null;
            }

            PrintListOfDeviceInfo(deviceIds);

            if (deviceIds == null || deviceIds.Length == 0)
            {
                return null;
            }

            if (deviceIds.Length == 1)
            {
                Console.WriteLine("Using detected device with ID: {0}.\n", deviceIds[0]);
                return FromDeviceId(deviceIds[0]);
            }

            int deviceId = -1;

            do
            {
                Console.Write("Enter the ID of the device to use (or enter q to quit): ");
                string answer = Console.ReadLine();

                if (answer.ToLower().Equals("q"))
                {
                    break;
                }
                else
                {
                    try
                    {
                        deviceId = Int32.Parse(answer);
                    }
                    catch (ArgumentException) { continue; }
                    catch (FormatException) { continue; }
                    catch (OverflowException) { continue; }
                }

                for (int i = 0; i < deviceIds.Length; i++)
                {
                    if (deviceId == deviceIds[i])
                    {
                        Console.WriteLine("Using detected device with ID: {0}.\n", deviceId);
                        return FromDeviceId(deviceId);
                    }
                }
            } while (deviceId < 0);

            if (deviceId > 0)
            {
                return FromDeviceId(deviceId);
            }

            return null;
        }
    }
}