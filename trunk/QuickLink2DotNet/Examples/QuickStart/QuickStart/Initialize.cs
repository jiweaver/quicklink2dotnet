#region License

/* Initialize: Class to provide an easy method for initializing an eye tracker
 * device.
 *
 * Copyright (c) 2009-2012 EyeTech Digital Systems
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

/* $Id$
 *
 * Authors: Brianna Peters <bpeters@eyetechds.com>
 * Date: May 2, 2012; 10:50 AM
 * Last modified May 18, 2012; 12:22 PM
 * Copyright © 2009-2012 EyeTech Digital Systems
 * support@eyetechds.com
 * Description: Class to provide an easy method for initializing an eye tracker
 * device.
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickLink2DotNet;

namespace QuickStart
{
    public static class Initialize
    {
        public static int QL2Initialize(string path)
        {
            QLError qlerror = QLError.QL_ERROR_OK;
            int deviceId = 0;
            QLDeviceInfo deviceInfo;
            int settingsId;

            //Enumerate the bus to find out which eye trackers are connected to the computer.
            const int bufferSize = 100;
            int[] deviceIds = new int[bufferSize];
            int numDevices = bufferSize;
            qlerror = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIds);

            // If the enumeration failed then return 0.
            if (qlerror != QLError.QL_ERROR_OK)
            {
                System.Console.WriteLine("QLDevice_Enumerate() failed with error code {0}.", qlerror);
                return 0;
            }

            // If no devices were found then return 0.
            else if (numDevices == 0)
            {
                System.Console.WriteLine("No devices present.");
                return 0;
            }

            // If there was only one device connected then use it without prompting the user.
            else if (numDevices == 1)
                deviceId = deviceIds[0];

            // If there is more than one device then ask which one to use.
            else if (numDevices > 1)
            {
                System.Console.WriteLine("QLDevice_Enumerate() found {0} devices\n", numDevices);

                //Get the information for each eye tracker and print it to the screen.
                for (int i = 0; i < numDevices; i++)
                {
                    QuickLink2API.QLDevice_GetInfo(deviceIds[i], out deviceInfo);
                    System.Console.WriteLine("Device {0}", i);
                    System.Console.WriteLine("\tdeviceInfo.modelName = {0}", deviceInfo.modelName);
                    System.Console.WriteLine("\tdeviceInfo.serialNumber = {0}\n", deviceInfo.serialNumber);
                }

                //Ask which device to use
                int deviceToUse = -1;

                System.Console.WriteLine("Which device would you like to use?");
                deviceToUse = Convert.ToInt32(System.Console.ReadLine());

                //Check to make sure the user's input is valid.  If it is not valid, then return 0
                if ((deviceToUse < 0) || (deviceToUse >= numDevices))
                {
                    System.Console.WriteLine("Invalid device: {0} \n", deviceToUse);
                    return 0;
                }

                //if the device is valid then select it as the device to use.
                else
                    deviceId = deviceIds[deviceToUse];
            }

            // Create a blank settings container. QLSettings_Load() can create a
            // settings container but it won't if the file fails to load. By calling
            // QLSettings_Create() we ensure that a container is created regardless.
            qlerror = QuickLink2API.QLSettings_Create(0, out settingsId);

            //Load the file with the stored password.
            qlerror = QuickLink2API.QLSettings_Load(path, ref settingsId);

            //Get the device info so we can access the serial number.
            QuickLink2API.QLDevice_GetInfo(deviceId, out deviceInfo);

            // Create an application defined setting name using the serial number. The
            // settings containers can be used to hold settings other than the
            // QuickLink2 defined setting. Using it to store the password for future
            // use as we are doing here is a good example.
            string serialNumberName = "SN_";
            serialNumberName += deviceInfo.serialNumber;

            // Create a buffer for getting the stored password.
            const int passwordBufferSize = 128;
            System.Text.StringBuilder password = new System.Text.StringBuilder(passwordBufferSize + 1);

            // Check for the password in the settings file.
            QuickLink2API.QLSettings_GetValueString(
                settingsId,
                serialNumberName,
                passwordBufferSize, password);

            //Try setting the password for the device.
            qlerror = QuickLink2API.QLDevice_SetPassword(deviceId, password.ToString());

            if (qlerror != QLError.QL_ERROR_OK)
            {
                System.Console.WriteLine("What is the password for the device? ");
                string userPassword = System.Console.ReadLine();

                //Set the password for the device
                qlerror = QuickLink2API.QLDevice_SetPassword(deviceId, userPassword);

                //If the password is not correct then print an error and return 0.
                if (qlerror != QLError.QL_ERROR_OK)
                {
                    System.Console.WriteLine("Invalid password. Error = {0}", qlerror);
                    return 0;
                }

                // Set the password for the device in the settings container.
                QuickLink2API.QLSettings_SetValueString(settingsId, serialNumberName, userPassword);

                // Save the settings container to file.
                QuickLink2API.QLSettings_Save(path, settingsId);
            }

            return deviceId;
        }
    }
}