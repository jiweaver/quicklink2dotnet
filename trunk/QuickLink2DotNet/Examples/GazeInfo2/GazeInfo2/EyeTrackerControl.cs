#region License

/* EyeTrackerControl: Convenience methods for controlling an eye tracker.
 *
 * Copyright (c) 2011-2012 Justin Weaver
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
 * Description: Convenience methods for controlling an eye tracker.
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickLink2DotNet;

namespace GazeInfo2
{
    public static class EyeTrackerControl
    {
        /// <summary>
        /// Find the first eye tracker on the system.  Returns the device
        /// number.  Throws exception on error.
        /// </summary>
        public static int GetFirstDeviceID()
        {
            // Allocate a buffer for the device ID array.
            int bufferSize = 100;
            int[] deviceIds = new int[bufferSize];
            int numDevices = bufferSize;

            // Enumerate the eye tracker devices.
            QLError qlerror = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIds);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QuickLink2API.QLDevice_Enumerate() returned {0}.", qlerror.ToString()));

            if (numDevices == 0)
                // No devices detected.
                throw new Exception("No eye trackers detected.");

            // Return the ID of the first device.
            return deviceIds[0];
        }

        /// <summary>
        /// Start the eye tracker.  Throws exception on error.
        /// </summary>
        public static void StartDevice(int deviceID)
        {
            QLError qlerror;

            // Start the device.
            qlerror = QuickLink2API.QLDevice_Start(deviceID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_Start() returned {0}", qlerror.ToString()));
        }

        /// <summary>
        /// Stop the eye tracker.  Throws exception on error.
        /// </summary>
        public static void StopDevice(int deviceID)
        {
            QLError qlerror;

            // Stop the device.
            qlerror = QuickLink2API.QLDevice_Stop(deviceID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_Stop() returned {0}", qlerror.ToString()));
        }

        /// <summary>
        /// Sets the eye tracker's password.  Throws exception on error.
        /// </summary>
        public static void SetDevicePassword(int deviceID, string password)
        {
            QLError qlerror;

            // Set the password.
            qlerror = QuickLink2API.QLDevice_SetPassword(deviceID, password);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_SetPassword() returned {0}", qlerror.ToString()));
        }

        /// <summary>
        /// Loads the device password from a file.  Returns the password
        /// string.  Throws exception on error.
        /// </summary>
        public static string LoadDevicePassword(int deviceID, string loadFilename)
        {
            QLError qlerror;

            // Create a new settings container.
            int settingsID;
            qlerror = QuickLink2API.QLSettings_Create(0, out settingsID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QL_Settings_Create() returned {0}", qlerror.ToString()));

            // Read the settings out of a file.
            qlerror = QuickLink2API.QLSettings_Load(loadFilename, ref settingsID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLSettings_Load() returned {0}", qlerror.ToString()));

            // Get the device's serial number.
            QLDeviceInfo devInfo;
            qlerror = QuickLink2API.QLDevice_GetInfo(deviceID, out devInfo);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QL_Settings_Create() returned {0}", qlerror.ToString()));

            // Check for the device password already in settings.
            int buffSize = 25;
            System.Text.StringBuilder password = new System.Text.StringBuilder(buffSize + 1);
            qlerror = QuickLink2API.QLSettings_GetValueString(settingsID, "SN_" + devInfo.serialNumber, buffSize, password);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLSettings_GetValueString() returned {0}", qlerror.ToString()));

            // Set the password on the device.
            qlerror = QuickLink2API.QLDevice_SetPassword(deviceID, password.ToString());
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_SetPassword() returned {0}", qlerror.ToString()));

            // Return the password.
            return password.ToString();
        }

        /// <summary>
        /// Saves the eye tracker's password to a file.  Throws exception on
        /// error.
        /// </summary>
        public static void SaveDevicePassword(int deviceID, string password, string saveFilename)
        {
            QLError qlerror;

            // Set the password.
            qlerror = QuickLink2API.QLDevice_SetPassword(deviceID, password);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_SetPassword() returned {0}", qlerror.ToString()));

            // Get the device info so we have its serial number.
            QLDeviceInfo devInfo;
            qlerror = QuickLink2API.QLDevice_GetInfo(deviceID, out devInfo);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_GetInfo() returned {0}", qlerror.ToString()));

            // Create a new settings container.
            int settingsID;
            qlerror = QuickLink2API.QLSettings_Create(0, out settingsID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QL_Settings_Create() returned {0}", qlerror.ToString()));

            // Add the password field to the settings in our new container.
            qlerror = QuickLink2API.QLSettings_AddSetting(settingsID, "SN_" + devInfo.serialNumber);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QL_Settings_AddSetting() returned {0}", qlerror.ToString()));

            // Set the password field in our container.
            qlerror = QuickLink2API.QLSettings_SetValueString(settingsID, "SN_" + devInfo.serialNumber, password.ToString());
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QL_Settings_SetValueString() returned {0}", qlerror.ToString()));

            // Save the settings in our container to a settings file.
            qlerror = QuickLink2API.QLSettings_Save(saveFilename, settingsID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QL_Settings_Save() returned {0}", qlerror.ToString()));
        }

        /// <summary>
        /// Loads calibration from a file.  Throws exception on error.
        /// </summary>
        public static void LoadAndApplyDeviceCalibration(int deviceID, string loadFilename)
        {
            QLError qlerror;

            // Create a new calibration container.
            int calibrationID;
            qlerror = QuickLink2API.QLCalibration_Create(0, out calibrationID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLCalibration_Create() returned {0}", qlerror.ToString()));

            // Load the calibration out of a file.
            qlerror = QuickLink2API.QLCalibration_Load(loadFilename, ref calibrationID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLCalibration_Load() returned {0}", qlerror.ToString()));

            // Apply the calibration.
            qlerror = QuickLink2API.QLDevice_ApplyCalibration(deviceID, calibrationID);
            if (qlerror != QLError.QL_ERROR_OK)
                throw new Exception(string.Format("QLDevice_ApplyCalibration() returned {0}", qlerror.ToString()));
        }
    }
}