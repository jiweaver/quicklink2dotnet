#region License

/* Helper: Convenience methods for controlling an eye tracker.
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
 * Description: Convenience methods for controlling an eye tracker.
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using QuickLink2DotNet;

namespace QuickLink2APIHelper
{
    public static class Helper
    {
        /// <summary>
        /// Get the deviceIDs of every eye tracker device detected on the system.
        /// </summary>
        /// <returns>
        /// An array containing the deviceIDs of every eye tracker device on the system upon success;
        /// throws a generic Exception with an explanatory Message upon failure.
        /// </returns>
        public static int[] GetDeviceIDs()
        {
            int numDevices = 8;
            int[] deviceIDs = new int[numDevices];

            QuickLink2DotNet.QLError error = QuickLink2DotNet.QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIDs);

            if (error == QuickLink2DotNet.QLError.QL_ERROR_BUFFER_TOO_SMALL)
            {
                /* Try again with a properly sized array. */
                deviceIDs = new int[numDevices];
                error = QuickLink2DotNet.QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIDs);
            }

            if (error != QuickLink2DotNet.QLError.QL_ERROR_OK)
            {
                throw new Exception(string.Format("QuickLink2API.QLDevice_Enumerate() returned {0}.", error.ToString()));
            }

            if (numDevices == 0)
            {
                throw new Exception("No eye trackers detected.");
            }

            // Return the ID of the first device.
            return deviceIDs;
        }

        private class Setting
        {
            public string Name;
            public QLSettingType SettingType;
            public Type Type;

            public Setting(string name, QLSettingType settingType, Type type)
            {
                this.Name = name;
                this.SettingType = settingType;
                this.Type = type;
            }
        }

        private static Setting[] Settings = new Setting[] {
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_MODE, QLSettingType.QL_SETTING_TYPE_INT32, typeof(QLDeviceBandwidthMode)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_PERCENT_FULL, QLSettingType.QL_SETTING_TYPE_INT32, typeof(System.Int32)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_PERCENT_TRACKING, QLSettingType.QL_SETTING_TYPE_INT32, typeof(System.Int32)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_BINNING, QLSettingType.QL_SETTING_TYPE_INT32, typeof(System.Int32)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_CALIBRATE_ENABLED, QLSettingType.QL_SETTING_TYPE_BOOL, typeof(bool)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_DISTANCE, QLSettingType.QL_SETTING_TYPE_INT32, typeof(System.Int32)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_EXPOSURE, QLSettingType.QL_SETTING_TYPE_INT32, typeof(System.Int32)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_FLIP_X, QLSettingType.QL_SETTING_TYPE_BOOL, typeof(bool)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_FLIP_Y, QLSettingType.QL_SETTING_TYPE_BOOL, typeof(bool)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_GAIN_MODE, QLSettingType.QL_SETTING_TYPE_INT32, typeof(QLDeviceGainMode)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_GAIN_VALUE, QLSettingType.QL_SETTING_TYPE_INT32, typeof(System.Int32)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_MODE, QLSettingType.QL_SETTING_TYPE_INT32, typeof(QLDeviceGazePointFilterMode)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE, QLSettingType.QL_SETTING_TYPE_INT32, typeof(System.Int32)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_ENABLED, QLSettingType.QL_SETTING_TYPE_BOOL, typeof(bool)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYES_TO_FIND, QLSettingType.QL_SETTING_TYPE_INT32, typeof(QLDeviceEyesToUse)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT, QLSettingType.QL_SETTING_TYPE_FLOAT, typeof(System.Single)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_RIGHT, QLSettingType.QL_SETTING_TYPE_FLOAT, typeof(System.Single)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_INTERPOLATE_ENABLED, QLSettingType.QL_SETTING_TYPE_BOOL, typeof(bool)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_IR_LIGHT_MODE, QLSettingType.QL_SETTING_TYPE_INT32, typeof(QLDeviceIRLightMode)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_LENS_FOCAL_LENGTH, QLSettingType.QL_SETTING_TYPE_INT32, typeof(System.Int32)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_X, QLSettingType.QL_SETTING_TYPE_INT32, typeof(System.Int32)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_Y, QLSettingType.QL_SETTING_TYPE_INT32, typeof(System.Int32)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_ROI_SIZE_PERCENT_X, QLSettingType.QL_SETTING_TYPE_INT32, typeof(System.Int32)),
            new Setting(QL_SETTINGS.QL_SETTING_DEVICE_ROI_SIZE_PERCENT_Y, QLSettingType.QL_SETTING_TYPE_INT32, typeof(System.Int32)),
        };

        public static int GetDeviceSettings(int deviceID)
        {
            int settingsID;
            QuickLink2DotNet.QLError error = QuickLink2DotNet.QuickLink2API.QLSettings_Create(-1, out settingsID);
            if (error != QuickLink2DotNet.QLError.QL_ERROR_OK)
            {
                throw new Exception(string.Format("QLSettings_Create(-1, out settingsID) returned {0}.", error.ToString()));
            }

            foreach (Setting setting in Settings)
            {
                error = QuickLink2DotNet.QuickLink2API.QLDevice_IsSettingSupported(deviceID, setting.Name);
                if (error == QuickLink2DotNet.QLError.QL_ERROR_OK)
                {
                    error = QuickLink2DotNet.QuickLink2API.QLSettings_AddSetting(settingsID, setting.Name);
                    if (error != QuickLink2DotNet.QLError.QL_ERROR_OK)
                    {
                        throw new Exception(string.Format("QLSettings_AddSetting({0}, {1}) returned {2}.", settingsID, setting.Name, error.ToString()));
                    }
                }
                else if (error != QuickLink2DotNet.QLError.QL_ERROR_NOT_SUPPORTED)
                {
                    throw new Exception(string.Format("QLSettings_IsSettingSupported({0}, {1}) returned {2}.", deviceID, setting.Name, error.ToString()));
                }
            }

            error = QuickLink2DotNet.QuickLink2API.QLDevice_ExportSettings(deviceID, settingsID);
            if (error != QuickLink2DotNet.QLError.QL_ERROR_OK)
            {
                throw new Exception(string.Format("QLSettings_ExportSettings({0}, {1}) returned {2}.", deviceID, settingsID, error.ToString()));
            }

            return settingsID;
        }

        public static System.Single IntPtrToSingle(IntPtr intPtr)
        {
            System.Single[] values = new System.Single[1];

            var gch = GCHandle.Alloc(intPtr, GCHandleType.Pinned);
            try
            {
                var source = gch.AddrOfPinnedObject();
                Marshal.Copy(source, values, 0, 1);
            }
            catch
            {
                throw;
            }
            finally
            {
                gch.Free();
            }

            return values[0];
        }

        public static bool IntPtrToBool(IntPtr intPtr)
        {
            int value = intPtr.ToInt32();
            if (value != 0 && value != 1)
            {
                throw new Exception("Error translating IntPtr to Boolean.");
            }
            return (value == 1) ? true : false;
        }

        public static string SettingsToString(int settingsID)
        {
            System.Text.StringBuilder buffer = new System.Text.StringBuilder(8);

            foreach (Setting setting in Settings)
            {
                QuickLink2DotNet.QLError error;
                string stringValue;

                IntPtr intPtr = new IntPtr();
                error = QuickLink2DotNet.QuickLink2API.QLSettings_GetValue(settingsID, setting.Name, setting.SettingType, 0, ref intPtr);

                if (setting.Type == typeof(System.Int32))
                {
                    stringValue = intPtr.ToInt32().ToString();
                }
                else if (setting.Type == typeof(QLDeviceBandwidthMode))
                {
                    int value = intPtr.ToInt32();
                    stringValue = ((QLDeviceBandwidthMode)value).ToString();
                }
                else if (setting.Type == typeof(QLDeviceGainMode))
                {
                    int value = intPtr.ToInt32();
                    stringValue = ((QLDeviceGainMode)(intPtr.ToInt32())).ToString();
                }
                else if (setting.Type == typeof(QLDeviceGazePointFilterMode))
                {
                    int value = intPtr.ToInt32();
                    stringValue = ((QLDeviceGazePointFilterMode)(intPtr.ToInt32())).ToString();
                }
                else if (setting.Type == typeof(QLDeviceEyesToUse))
                {
                    int value = intPtr.ToInt32();
                    stringValue = ((QLDeviceEyesToUse)(intPtr.ToInt32())).ToString();
                }
                else if (setting.Type == typeof(QLDeviceIRLightMode))
                {
                    int value = intPtr.ToInt32();
                    stringValue = ((QLDeviceIRLightMode)(intPtr.ToInt32())).ToString();
                }
                else if (setting.Type == typeof(System.Boolean))
                {
                    bool value = IntPtrToBool(intPtr);
                    stringValue = value.ToString();
                }
                else if (setting.Type == typeof(System.Single))
                {
                    System.Single value = IntPtrToSingle(intPtr);
                    stringValue = value.ToString();
                }
                else
                {
                    throw new Exception(string.Format("Error: Getting setting '{0}' of type '{1}' from container #{2}.", setting.Name, setting.SettingType, settingsID));
                }

                if (error == QuickLink2DotNet.QLError.QL_ERROR_OK)
                {
                    buffer.AppendFormat("{0}: {1}{2}", setting.Name, stringValue, Environment.NewLine);
                }
                else if (error != QuickLink2DotNet.QLError.QL_ERROR_NOT_FOUND)
                {
                    throw new Exception(string.Format("Error: Getting setting '{0}' of type '{1}' from container #{2} returned {3}.", setting.Name, setting.SettingType, settingsID, error.ToString()));
                }
            }

            return buffer.ToString();
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