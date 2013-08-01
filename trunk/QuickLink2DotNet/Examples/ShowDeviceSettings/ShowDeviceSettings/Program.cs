using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickLink2DotNet;

namespace ShowDeviceSettings
{
    class Program
    {
        private class DeviceSetting
        {
            public QLSettingType SettingType;
            public bool Supported;
            public object Value;

            public DeviceSetting(QLSettingType settingType)
            {
                this.SettingType = settingType;
                this.Supported = false;
                this.Value = null;
            }
        }

        private static Dictionary<string, DeviceSetting> DeviceSettings = new Dictionary<string, DeviceSetting>
            {
                {QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_MODE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_PERCENT_FULL,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_PERCENT_TRACKING,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_BINNING,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_CALIBRATE_ENABLED,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_BOOL)},
                {QL_SETTINGS.QL_SETTING_DEVICE_DISTANCE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_EXPOSURE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_FLIP_X,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_BOOL)},
                {QL_SETTINGS.QL_SETTING_DEVICE_FLIP_Y,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_BOOL)},
                {QL_SETTINGS.QL_SETTING_DEVICE_GAIN_MODE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_GAIN_VALUE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_MODE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_ENABLED,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_BOOL)},
                {QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_FLOAT)},
                {QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_RIGHT,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_FLOAT)},
                {QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYES_TO_FIND,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_INTERPOLATE_ENABLED,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_BOOL)},
                {QL_SETTINGS.QL_SETTING_DEVICE_IR_LIGHT_MODE,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_LENS_FOCAL_LENGTH,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_X,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_Y,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_ROI_SIZE_PERCENT_X,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
                {QL_SETTINGS.QL_SETTING_DEVICE_ROI_SIZE_PERCENT_Y,new DeviceSetting(QLSettingType.QL_SETTING_TYPE_INT32)},
            };

        public static void LoadSettingsFrom(int deviceID)
        {
            int settingsID;

            // Create a new, empty container.
            QLError error = QuickLink2API.QLSettings_Create(-1, out settingsID);
            if (error != QLError.QL_ERROR_OK)
            {
                throw new Exception(string.Format("QLSettings_Create() returned {0}.", error.ToString()));
            }

            // Discover supported settings.
            foreach (KeyValuePair<string, DeviceSetting> setting in DeviceSettings)
            {
                error = QuickLink2API.QLDevice_IsSettingSupported(deviceID, setting.Key);
                if (error == QLError.QL_ERROR_OK)
                {
                    setting.Value.Supported = true;
                    error = QuickLink2API.QLSettings_AddSetting(settingsID, setting.Key);
                    if (error != QLError.QL_ERROR_OK)
                    {
                        throw new Exception(string.Format("QLSettings_AddSetting({0}, {1}) returned {2}.", settingsID, setting.Key, error.ToString()));
                    }
                }
                else if (error == QLError.QL_ERROR_NOT_SUPPORTED)
                {
                    setting.Value.Supported = false;
                    error = QuickLink2API.QLSettings_RemoveSetting(settingsID, setting.Key);
                    if (error != QLError.QL_ERROR_OK && error != QLError.QL_ERROR_NOT_FOUND)
                    {
                        throw new Exception(string.Format("QLSettings_RemoveSetting({0}, {1}) returned {2}.", settingsID, setting.Key, error.ToString()));
                    }
                }
                else
                {
                    throw new Exception(string.Format("QLSettings_IsSettingSupported({0}, {1}) returned {2}.", deviceID, setting.Key, error.ToString()));
                }
            }

            // Export supported settings to container.
            error = QuickLink2API.QLDevice_ExportSettings(deviceID, settingsID);
            if (error != QLError.QL_ERROR_OK)
            {
                throw new Exception(string.Format("QLSettings_ExportSettings({0}, {1}) returned {2}.", deviceID, settingsID, error.ToString()));
            }

            // Fill in the values for the supported settings.
            foreach (KeyValuePair<string, DeviceSetting> setting in DeviceSettings)
            {
                if (setting.Value.Supported)
                {
                    switch (setting.Value.SettingType)
                    {
                        case QLSettingType.QL_SETTING_TYPE_BOOL:
                            bool boolValue;
                            error = QuickLink2API.QLSettings_GetValueBool(settingsID, setting.Key, out boolValue);
                            if (error != QLError.QL_ERROR_OK)
                            {
                                throw new Exception(string.Format("QLSettings_GetValueBool({0}, {1}, out boolValue) returned {2}.", settingsID, setting.Key, error.ToString()));
                            }
                            setting.Value.Value = (object)boolValue;
                            break;

                        case QLSettingType.QL_SETTING_TYPE_FLOAT:
                            System.Single floatValue;
                            error = QuickLink2API.QLSettings_GetValueFloat(settingsID, setting.Key, out floatValue);
                            if (error != QLError.QL_ERROR_OK)
                            {
                                throw new Exception(string.Format("QLSettings_GetValueFloat({0}, {1}, out floatValue) returned {2}.", settingsID, setting.Key, error.ToString()));
                            }
                            setting.Value.Value = (object)floatValue;
                            break;

                        case QLSettingType.QL_SETTING_TYPE_INT32:
                            System.Int32 int32Value;
                            error = QuickLink2API.QLSettings_GetValueInt32(settingsID, setting.Key, out int32Value);
                            if (error != QLError.QL_ERROR_OK)
                            {
                                throw new Exception(string.Format("QLSettings_GetValueInt32({0}, {1}, out int32Value) returned {2}.", settingsID, setting.Key, error.ToString()));
                            }
                            setting.Value.Value = (object)int32Value;
                            break;

                        default:
                            throw new Exception(string.Format("SettingType {0} not recognized.", setting.Value.SettingType.ToString()));
                    }
                }
            }
        }

        public static void PrintSettings()
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
                        Console.WriteLine("  {0}: {1}", setting.Key, singleValue.ToString());
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
                            Console.WriteLine("  {0}: {1}", setting.Key, int32Value.ToString());
                        }
                        break;

                    default:
                        throw new Exception(string.Format("Unrecognized setting type: {0}.", setting.Value.SettingType.ToString()));
                }
            }
        }

        static void Main(string[] args)
        {
            int deviceID = QLHelper.ConsoleInteractive_GetDeviceID();

            if (deviceID == -1)
            {
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                return;
            }

            QLDeviceInfo info;
            QLError error = QuickLink2API.QLDevice_GetInfo(deviceID, out info);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_GetInfo() returned {0}.", error.ToString());

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Showing setting for device with ID:{0}; Model:{1}; Serial:{2}", deviceID, info.modelName, info.serialNumber);

            LoadSettingsFrom(deviceID);

            PrintSettings();

            Console.WriteLine();

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}