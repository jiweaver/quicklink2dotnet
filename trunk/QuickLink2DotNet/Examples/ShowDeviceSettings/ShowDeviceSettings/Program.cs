#region License

/* ShowDeviceSettings: Displays a list of available devices and prompts the
 * user to select one.  Then queries the selected device for all its settings
 * and displays their values.
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
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using QuickLink2DotNet;
using QuickLink2DotNetHelper;

namespace ShowDeviceSettings
{
    class Program
    {
        private class DeviceSetting
        {
            public QLSettingType SettingType;
            public bool Supported;
            public object Value;
            public string Units;

            public DeviceSetting(QLSettingType settingType, string units)
            {
                this.SettingType = settingType;
                this.Supported = false;
                this.Value = null;
                this.Units = units;
            }
        }

        private static Dictionary<string, DeviceSetting> DeviceSettings = new Dictionary<string, DeviceSetting>
            {
                {QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_MODE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "")},
                {QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_PERCENT_FULL,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "%")},
                {QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_PERCENT_TRACKING,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "%")},
                {QL_SETTINGS.QL_SETTING_DEVICE_BINNING,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "^2 sensors per pixel")},
                {QL_SETTINGS.QL_SETTING_DEVICE_CALIBRATE_ENABLED,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_BOOL, "")},
                {QL_SETTINGS.QL_SETTING_DEVICE_DISTANCE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "cm")},
                {QL_SETTINGS.QL_SETTING_DEVICE_EXPOSURE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "ms")},
                {QL_SETTINGS.QL_SETTING_DEVICE_FLIP_X,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_BOOL, "")},
                {QL_SETTINGS.QL_SETTING_DEVICE_FLIP_Y,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_BOOL, "")},
                {QL_SETTINGS.QL_SETTING_DEVICE_GAIN_MODE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "")},
                {QL_SETTINGS.QL_SETTING_DEVICE_GAIN_VALUE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "")},
                {QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_MODE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "")},
                {QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "")},
                {QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_ENABLED,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_BOOL, "")},
                {QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_FLOAT, "cm")},
                {QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_RIGHT,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_FLOAT, "cm")},
                {QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYES_TO_FIND,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "")},
                {QL_SETTINGS.QL_SETTING_DEVICE_INTERPOLATE_ENABLED,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_BOOL, "")},
                {QL_SETTINGS.QL_SETTING_DEVICE_IR_LIGHT_MODE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "")},
                {QL_SETTINGS.QL_SETTING_DEVICE_LENS_FOCAL_LENGTH,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "cm")},
                {QL_SETTINGS.QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_X,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "%")},
                {QL_SETTINGS.QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_Y,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "%")},
                {QL_SETTINGS.QL_SETTING_DEVICE_ROI_SIZE_PERCENT_X,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "%")},
                {QL_SETTINGS.QL_SETTING_DEVICE_ROI_SIZE_PERCENT_Y,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32, "%")},
            };

        private static bool LoadSettingsFrom(int deviceId)
        {
            int settingsID;

            // Create a new, empty container.
            QLError error = QuickLink2API.QLSettings_Create(-1, out settingsID);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_Create() returned {0} for device {1}.", error.ToString());
                return false;
            }

            // Discover supported settings.
            foreach (KeyValuePair<string, DeviceSetting> setting in DeviceSettings)
            {
                error = QuickLink2API.QLDevice_IsSettingSupported(deviceId, setting.Key);
                if (error == QLError.QL_ERROR_NOT_SUPPORTED)
                {
                    setting.Value.Supported = false;
                    error = QuickLink2API.QLSettings_RemoveSetting(settingsID, setting.Key);
                    if (error != QLError.QL_ERROR_OK && error != QLError.QL_ERROR_NOT_FOUND)
                    {
                        Console.WriteLine("QLSettings_RemoveSetting() returned {0} for key {1} device {2}.", error.ToString(), setting.Key, deviceId);
                        return false;
                    }
                }
                else if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("QLSettings_IsSettingSupported() returned {0} for key {1} on device {2}.", error.ToString(), setting.Key, deviceId);
                    return false;
                }

                setting.Value.Supported = true;
                error = QuickLink2API.QLSettings_AddSetting(settingsID, setting.Key);
                if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("QLSettings_AddSetting() returned {0} for key {1} on device {2}.", error.ToString(), setting.Key, deviceId);
                    return false;
                }
            }

            // Export supported settings to container.
            error = QuickLink2API.QLDevice_ExportSettings(deviceId, settingsID);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_ExportSettings() returned {0} for device {1}.", error.ToString(), deviceId);
                return false;
            }

            // Fill in the values for the supported settings.
            foreach (KeyValuePair<string, DeviceSetting> setting in DeviceSettings)
            {
                if (!setting.Value.Supported)
                {
                    continue;
                }

                switch (setting.Value.SettingType)
                {
                    case QLSettingType.QL_SETTING_TYPE_BOOL:
                        bool boolValue;
                        error = QuickLink2API.QLSettings_GetValueBool(settingsID, setting.Key, out boolValue);
                        if (error != QLError.QL_ERROR_OK)
                        {
                            Console.WriteLine("QLSettings_GetValueBool() returned {0} for key {1} on device {2}.", error.ToString(), setting.Key, deviceId);
                            return false;
                        }
                        setting.Value.Value = (object)boolValue;
                        break;

                    case QLSettingType.QL_SETTING_TYPE_FLOAT:
                        System.Single floatValue;
                        error = QuickLink2API.QLSettings_GetValueFloat(settingsID, setting.Key, out floatValue);
                        if (error != QLError.QL_ERROR_OK)
                        {
                            Console.WriteLine("QLSettings_GetValueFloat() returned {0} for key {1} on device {2}.", error.ToString(), setting.Key, deviceId);
                            return false;
                        }
                        setting.Value.Value = (object)floatValue;
                        break;

                    case QLSettingType.QL_SETTING_TYPE_INT32:
                        System.Int32 int32Value;
                        error = QuickLink2API.QLSettings_GetValueInt32(settingsID, setting.Key, out int32Value);
                        if (error != QLError.QL_ERROR_OK)
                        {
                            Console.WriteLine("QLSettings_GetValueInt32() returned {0} for key {1} on device {2}.", error.ToString(), setting.Key, deviceId);
                            return false;
                        }
                        setting.Value.Value = (object)int32Value;
                        break;

                    default:
                        Console.WriteLine("SettingType {0} not recognized.", setting.Value.SettingType.ToString());
                        return false;
                }
            }

            return true;
        }

        private static void PrintSettings()
        {
            foreach (KeyValuePair<string, DeviceSetting> setting in DeviceSettings)
            {
                if (!setting.Value.Supported)
                {
                    continue;
                }

                switch (setting.Value.SettingType)
                {
                    case QLSettingType.QL_SETTING_TYPE_BOOL:
                        bool boolValue = (bool)setting.Value.Value;
                        Console.WriteLine("  {0}: {1}", setting.Key, boolValue.ToString());
                        break;

                    case QLSettingType.QL_SETTING_TYPE_FLOAT:
                        System.Single singleValue = (System.Single)setting.Value.Value;
                        Console.WriteLine("  {0}: {1}{2}", setting.Key, singleValue.ToString(), setting.Value.Units);
                        break;

                    case QLSettingType.QL_SETTING_TYPE_INT32:
                        if (setting.Key.Equals(QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_MODE))
                        {
                            QLDeviceBandwidthMode bandwidthMode = (QLDeviceBandwidthMode)setting.Value.Value;
                            Console.WriteLine("  {0}: {1}", setting.Key, bandwidthMode.ToString());
                        }
                        else if (setting.Key.Equals(QL_SETTINGS.QL_SETTING_DEVICE_GAIN_MODE))
                        {
                            QLDeviceGainMode gainMode = (QLDeviceGainMode)setting.Value.Value;
                            Console.WriteLine("  {0}: {1}", setting.Key, gainMode.ToString());
                        }
                        else if (setting.Key.Equals(QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_MODE))
                        {
                            QLDeviceGazePointFilterMode gazePointFilterMode = (QLDeviceGazePointFilterMode)setting.Value.Value;
                            Console.WriteLine("  {0}: {1}", setting.Key, gazePointFilterMode.ToString());
                        }
                        else if (setting.Key.Equals(QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYES_TO_FIND))
                        {
                            QLDeviceEyesToUse eyesToUse = (QLDeviceEyesToUse)setting.Value.Value;
                            Console.WriteLine("  {0}: {1}", setting.Key, eyesToUse.ToString());
                        }
                        else if (setting.Key.Equals(QL_SETTINGS.QL_SETTING_DEVICE_IR_LIGHT_MODE))
                        {
                            QLDeviceIRLightMode irLightMode = (QLDeviceIRLightMode)setting.Value.Value;
                            Console.WriteLine("  {0}: {1}", setting.Key, irLightMode.ToString());
                        }
                        else
                        {
                            System.Int32 int32Value = (System.Int32)setting.Value.Value;
                            Console.WriteLine("  {0}: {1}{2}", setting.Key, int32Value.ToString(), setting.Value.Units);
                        }
                        break;

                    default:
                        Console.WriteLine("Unrecognized setting type: {0}.", setting.Value.SettingType.ToString());
                        break;
                }
            }
        }

        #region Disable Close Button

        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern IntPtr RemoveMenu(IntPtr hMenu, uint nPosition, uint wFlags);

        private const uint SC_CLOSE = 0xF060;
        private const uint MF_GRAYED = 0x00000001;
        private const uint MF_BYCOMMAND = 0x00000000;

        private static void DisableCloseButton()
        {
            // Disable the close button, so the user must "hit a key to
            // continue."  This allows us to stop the eye tracker before
            // exiting.
            IntPtr hMenu = Process.GetCurrentProcess().MainWindowHandle;
            IntPtr hSystemMenu = GetSystemMenu(hMenu, false);
            EnableMenuItem(hSystemMenu, SC_CLOSE, MF_GRAYED);
            RemoveMenu(hSystemMenu, SC_CLOSE, MF_BYCOMMAND);
        }

        #endregion Disable Close Button

        private static void Main(string[] args)
        {
            DisableCloseButton();

            QLHelper helper = QLHelper.ChooseDevice();

            if (helper != null)
            {
                if (LoadSettingsFrom(helper.DeviceId))
                {
                    PrintSettings();
                }
            }

            // Flush the input buffer.
            while (Console.KeyAvailable) { Console.ReadKey(true); }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}