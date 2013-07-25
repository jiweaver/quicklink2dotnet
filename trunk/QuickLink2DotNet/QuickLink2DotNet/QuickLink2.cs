#region License

/* QuickLink2DotNet : A .NET wrapper (in C#) for EyeTech's QuickLink2 API.
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

/* $Id: QuickLink2.cs 38 2011-05-09 01:07:39Z piranther $
 *
 * Description: This file exposes the API functions available through EyeTech's
 * QuickLink2.dll for use in managed code like C# and Visual Basic.
 *
 * ATTENTION!!: This wrapper requires that you place the QuickLink2.dlls in the
 * same directory as your program executable.  You can download QuickLink2 from
 * http://www.eyetechds.com/support/downloads.
 *
 * The extensive API documentation is based upon the API documentation provided
 * by EyeTech.
 */

#endregion Header Comments

using System;
using System.Runtime.InteropServices;

namespace QuickLink2DotNet
{
    /// <summary>
    /// A class that exposes the QuickLink2 API functions for use with
    /// Microsoft's .NET.
    /// </summary>
    public static class QuickLink2API
    {
        #region Group: API

        /// <summary>
        /// <para>
        /// This function retrieves the version string of the API.
        /// </para>
        /// </summary>
        /// <seealso cref="QLError"/>
        /// <param name="bufferSize">
        /// The size of the output buffer in bytes.
        /// </param>
        /// <param name="buffer">
        /// An initialized <see cref="System.Text.StringBuilder" /> that receives the version string.
        /// </param>
        /// <returns>
        /// The success of the function.
        /// </returns>
        /// <example>
        /// <code>
        /// System.Text.StringBuilder buffer = new System.Text.StringBuilder(8);
        /// QLError error = QuickLink2API.QLAPI_GetVersion(8, buffer);
        /// if (error == case QLError.QL_ERROR_OK)
        /// {
        ///         System.Console.WriteLine("Version: {0}", buffer.ToString());
        /// }
        /// else
        /// {
        ///         System.Console.WriteLine("Error: {0}", error.ToString());
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLAPI_GetVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLAPI_GetVersion(
               System.Int32 bufferSize,
               System.Text.StringBuilder buffer);

        /// <summary>
        /// <para>
        /// This function exports the current system level API settings values to the specified settings
        /// container.  It will only export the values for setting that have been added to the settings
        /// container.  To add a setting to the settings container use the function
        /// <see cref="QLSettings_AddSetting" /> or the function <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS"/>
        /// <seealso cref="QLAPI_ImportSettings"/>
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting"/>
        /// <seealso cref="QLSettings_Create"/>
        /// <seealso cref="QLSettings_GetValue"/>
        /// <seealso cref="QLSettings_Load"/>
        /// <seealso cref="QLSettings_SetValue"/>
        /// <param name="settingsID">
        /// A settingsID is a unique identifier that is used to specify a settings container.  A
        /// settingsID is initially obtained by calling either the function
        /// <see cref="QLSettings_Create" /> or the function <see cref="QLSettings_Load" />.  In
        /// particular, this parameter specifies which settings container will receive the exported
        /// settings values from the API.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the settings values were successfully exported from the API.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLAPI_ExportSettings", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLAPI_ExportSettings(
               System.Int32 settingsID);

        /// <summary>
        /// <para>
        /// This function imports settings values to the system level of the API from the specified
        /// settings container.  The new settings values will take effect immediately.
        /// </para>
        /// <para>
        /// Not all settings are supported at the system level of the API.  Only settings values that are
        /// supported will be imported from the settings container.  All other settings in the container
        /// will be ignored.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS"/>
        /// <seealso cref="QLAPI_ExportSettings"/>
        /// <seealso cref="QLAPI_ImportSettings"/>
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting"/>
        /// <seealso cref="QLSettings_Create"/>
        /// <seealso cref="QLSettings_GetValue"/>
        /// <seealso cref="QLSettings_Load"/>
        /// <seealso cref="QLSettings_SetValue"/>
        /// <param name="settingsID">
        /// A settingsID is a unique identifier that is used to specify a settings container.  A
        /// settingsID is initially obtained by calling either the function
        /// <see cref="QLSettings_Create" /> or the function <see cref="QLSettings_Load" />.  In
        /// particular, this parameter specifies which settings container will supply the settings values
        /// that will be imported to the API.
        /// </param>
        /// <returns>
        /// The success of the function. If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the settings values were successfully imported to the API.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLAPI_ImportSettings", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLAPI_ImportSettings(
               System.Int32 settingsID);

        #endregion Group: API

        #region Group: Device

        /// <summary>
        /// <para>
        /// This function creates an ID for each EyeTech device connected to the system.
        /// </para>
        /// <para>
        /// Each deviceID is a unique integer that is used in other API functions to reference a specific
        /// device on the system.  Once QuickLink2.dll is loaded a deviceID will always refer to the same
        /// device until QuickLink2.dll is unloaded.  This is true even if the device is disconnected
        /// from the computer and then later reconnected.  A device is not guaranteed to have the same
        /// deviceID value from one loading of QuickLink2.dll to the next.
        /// </para>
        /// </summary>
        /// <seealso cref="QLError"/>
        /// <param name="numDevices">
        /// A reference to an initialized <see cref="System.Int32" /> object.  When this function is
        /// called, the value contained in this parameter must be set to the length of the
        /// <paramref name="deviceBuffer" /> array; however, when the function returns successfully, the
        /// value will be the actual number of devices that were found on the bus.
        /// </param>
        /// <param name="deviceBuffer">
        /// A initialized integer array that will receive the IDs of the devices that are connected to
        /// the system.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the bus was successfully enumerated and the <paramref name="deviceBuffer" /> contains IDs for
        /// all the EyeTech devices that were detected.
        /// <para>
        /// If the return value is <see cref="QLError.QL_ERROR_BUFFER_TOO_SMALL" /> then
        /// <paramref name="deviceBuffer" /> was too small to contain ID values for all the devices that
        /// are attached to the computer, and <paramref name="numDevices" /> will contain the actual
        /// number of devices detected.  In this case, the <paramref name="deviceBuffer" /> array should
        /// be resized to be at least as large as the value contained in <paramref name="numDevices" />,
        /// and the function should be called again.
        /// </para>
        /// </returns>
        /// <example>
        /// <code>
        /// int numDevices = 8;
        /// int[] deviceIDs = new int[numDevices];
        /// QLError error = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIDs);
        /// if (error == QLError.QL_ERROR_BUFFER_TOO_SMALL)
        /// {
        ///     /* Try again with a properly sized array. */
        ///     deviceIDs = new int[numDevices];
        ///     error = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIDs);
        /// }
        /// if (error == QLError.QL_ERROR_OK)
        /// {
        ///    System.Console.WriteLine("Number of Device Detected: {0}", numDevices);
        ///    System.Console.Write("Detected Device IDs: ");
        ///    for (int x=0; x&lt;numDevices; x++)
        ///    {
        ///        System.Console.Write("{0} ", deviceIDs[x]);
        ///    }
        ///    System.Console.WriteLine();
        /// }
        /// else
        /// {
        ///    System.Console.WriteLine("Error: {0}.", error.ToString());
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_Enumerate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_Enumerate(
               ref System.Int32 numDevices,
               [In, Out] System.Int32[] deviceBuffer);

        /// <summary>
        /// <para>
        /// Get device specific information for a device.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Enumerate"/>
        /// <seealso cref="QLDeviceInfo"/>
        /// <seealso cref="QLError"/>
        /// <param name="deviceID">
        /// The ID of the device from which to get information.  This ID is obtained by calling the
        /// function <see cref="QLDevice_Enumerate" />.
        /// </param>
        /// <param name="info">
        /// A reference to an uninitialized <see cref="QLDeviceInfo" /> object, which will receive
        /// the information about the device.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the <paramref name="info" /> object contains the information about the device.
        /// </returns>
        /// <example>
        /// <code>
        /// int deviceID = 1;   // NOTE: Get this value by calling QLDevice_Enumerate().
        /// QLDeviceInfo info;
        /// QLError error = QuickLink2API.QLDevice_GetInfo(deviceID, out info);
        /// if (error == QLError.QL_ERROR_OK)
        /// {
        ///         System.Console.WriteLine("Model Name: {0}", info.modelName.ToString());
        ///         System.Console.WriteLine("Serial Number: {0}", info.serialNumber.ToString());
        ///         System.Console.WriteLine("Sensor Height: {0}", info.sensorHeight.ToString());
        ///         System.Console.WriteLine("Sensor Width: {0}", info.sensorWidth.ToString());
        /// }
        /// else
        /// {
        ///     System.Console.WriteLine("Error: {0}", error.ToString());
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_GetInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_GetInfo(
               System.Int32 deviceID,
               out QLDeviceInfo info);

        /// <summary>
        /// <para>
        /// Get the status of a device.
        /// </para>
        /// <para>
        /// This function causes momentary slowness of the device while trying to determine the status.
        /// It can greatly affect frame rates and should not be called when capture times are critical.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Enumerate"/>
        /// <seealso cref="QLDeviceStatus" />
        /// <seealso cref="QLError"/>
        /// <param name="deviceID">
        /// The ID of the device from which to get information.  This ID is obtained by calling the
        /// function <see cref="QLDevice_Enumerate" />.
        /// </param>
        /// <param name="status">
        /// A reference to an uninitialized <see cref="QLDeviceStatus" /> object, which will receive the
        /// status of the device.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the <paramref name="status" /> parameter contains the status of the device.
        /// </returns>
        /// <example>
        /// <code>
        /// int deviceID = 1;   // NOTE: Get this value by calling QLDevice_Enumerate().
        /// QLDeviceStatus status;
        /// QLError error = QuickLink2API.QLDevice_GetStatus(deviceID, out status);
        /// if (error == QLError.QL_ERROR_OK)
        /// {
        ///     System.Console.WriteLine(status.ToString());
        /// }
        /// else
        /// {
        ///     System.Console.WriteLine("Error: {0}", error.ToString());
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_GetStatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_GetStatus(
               System.Int32 deviceID,
               out QLDeviceStatus status);

        /// <summary>
        /// <para>
        /// This function exports the settings values that are currently being used by the specified
        /// device.  The values are exported to the specified settings container.
        /// </para>
        /// <para>
        /// Note that this function only exports values for setting that have been previously added to
        /// the settings container.  To add a setting to the settings container use the function
        /// <see cref="QLSettings_AddSetting" /> or the function <see cref="QLSettings_SetValue" />.
        /// </para>
        /// <para>
        /// Not all settings are supported by all devices.  To determine if a setting is supported by a
        /// particular device use the function <see cref="QLDevice_IsSettingSupported" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS"/>
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLDevice_Enumerate"/>
        /// <seealso cref="QLDevice_ImportSettings"/>
        /// <seealso cref="QLDevice_IsSettingSupported" />
        /// <seealso cref="QLDeviceGroup_Create"/>
        /// <seealso cref="QLSettings_AddSetting"/>
        /// <seealso cref="QLSettings_Create"/>
        /// <seealso cref="QLSettings_GetValue"/>
        /// <seealso cref="QLSettings_Load"/>
        /// <seealso cref="QLSettings_SetValue"/>
        /// <param name="deviceID">
        /// The ID of the device from which to export the settings.  This ID is obtained by calling the
        /// function <see cref="QLDevice_Enumerate" />.
        /// </param>
        /// <param name="settingsID">
        /// The ID of the settings container that will receive the exported values from the device.  This
        /// ID is obtained by calling either the function <see cref="QLSettings_Create" /> or the
        /// function <see cref="QLSettings_Load" />.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the settings values were successfully exported from the device.
        /// </returns>
        /// <example>
        /// <code>
        /// int deviceID = 1;   // NOTE: Get this value by calling QLDevice_Enumerate().
        /// int settingsID = 2; // NOTE: Get this value by calling QLSettings_Create() or QLSettings_Load().
        /// error = QuickLink2API.QLDevice_ExportSettings(deviceID, settingsID);
        /// if (error == QLError.QL_ERROR_OK)
        /// {
        ///     System.Console.WriteLine("Settings for device {0} successfully exported to container {1}.", deviceID, settingsID);
        /// } else
        /// {
        ///     System.Console.WriteLine("Error: {1}", error.ToString());
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_ExportSettings", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLDevice_ExportSettings(
               System.Int32 deviceID,
               System.Int32 settingsID);

        /// <summary>
        /// <para>
        /// This function imports settings values to the specified device or group from the specified
        /// settings container and makes those settings active.  This operation is synchronous with the
        /// device.  The new settings values will take effect after the device has finished processing
        /// its current frame, resulting in a latency of one frame.
        /// </para>
        /// <para>
        /// Not all settings are supported by all devices.  Only settings values that are supported by
        /// the specified device (or devices in a group) will be imported from the settings container.
        /// All other settings in the settings container will be ignored.  To determine if a setting is
        /// supported by a particular device use the function <see cref="QLDevice_IsSettingSupported" />.
        /// </para>
        /// <para>
        /// Calling this function using a device group will cause the settings to be imported to all
        /// devices in the group.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS"/>
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLDevice_Enumerate"/>
        /// <seealso cref="QLDevice_ExportSettings"/>
        /// <seealso cref="QLDevice_IsSettingSupported" />
        /// <seealso cref="QLDeviceGroup_Create"/>
        /// <seealso cref="QLSettings_AddSetting"/>
        /// <seealso cref="QLSettings_Create"/>
        /// <seealso cref="QLSettings_GetValue"/>
        /// <seealso cref="QLSettings_Load"/>
        /// <seealso cref="QLSettings_SetValue"/>
        /// <param name="deviceOrGroupID">
        /// The ID of the device or device group in which to import the settings.  This ID is obtained by
        /// calling either the function <see cref="QLDevice_Enumerate" /> or the
        /// function <see cref="QLDeviceGroup_Create" />.
        /// </param>
        /// <param name="settingsID">
        /// The ID of the settings container that will supply the settings values to be imported to the
        /// device or group.  This ID is obtained by calling either the function
        /// <see cref="QLSettings_Create" /> or the function <see cref="QLSettings_Load" />.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the settings values were successfully imported to the device.
        /// </returns>
        /// <example>
        /// <code>
        /// int deviceOrGroupID = 1;   // NOTE: Get this value by calling QLDevice_Enumerate() or QLDeviceGroup_Create().
        /// int settingsID = 2; // NOTE: Get this value by calling QLSettings_Create() or QLSettings_Load().
        /// // Here, we would set some values in the container by calling any of the QLSettings_SetValue() family of functions.
        /// error = QuickLink2API.QLDevice_ImportSettings(deviceOrGroupID, settingsID);
        /// if (error == QLError.QL_ERROR_OK)
        /// {
        ///     System.Console.WriteLine("Settings for device or group {0} successfully imported from container {1}.", deviceOrGroupID, settingsID);
        /// } else
        /// {
        ///     System.Console.WriteLine("Error: {0}", error.ToString());
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_ImportSettings", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_ImportSettings(
               System.Int32 deviceOrGroupID,
               System.Int32 settingsID);

        /// <summary>
        /// <para>
        /// This function determines if a setting is supported by a given device.  Not all settings are
        /// supported by all devices.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS"/>
        /// <seealso cref="QLDevice_Enumerate"/>
        /// <seealso cref="QLError"/>
        /// <param name="deviceID">
        /// The ID of the device to check for setting support.  This ID is obtained by calling the
        /// function <see cref="QLDevice_Enumerate" />.
        /// </param>
        /// <param name="settingName">
        /// A member of the <see cref="QL_SETTINGS" /> class, or a string containing the name of the
        /// setting to check for in the device.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting is supported by the specified device; however, if the return value is
        /// <see cref="QLError.QL_ERROR_NOT_SUPPORTED" />, then the setting is not supported by the
        /// device.
        /// </returns>
        /// <example>
        /// <code>
        /// int deviceID = 1;   // NOTE: Get this value by calling QLDevice_Enumerate().
        /// QLError error = QuickLink2API.QLDevice_IsSettingSupported(deviceID, QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_ENABLED);
        /// switch (error)
        /// {
        ///     case QLError.QL_ERROR_OK:
        ///         System.Console.WriteLine("Feature is supported");
        ///         break;
        ///     case QLError.QL_ERROR_NOT_SUPPORTED:
        ///         System.Console.WriteLine("Feature is not supported");
        ///         break;
        ///     default:
        ///         System.Console.WriteLine("Error: {0}", error.ToString());
        ///         break;
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_IsSettingSupported", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLDevice_IsSettingSupported(
                System.Int32 deviceID,
                [In] System.String settingName);

        /// <summary>
        /// <para>
        /// This function sets the password for the specified device.  The password is usually found on a
        /// label affixed to the device.  If the password is not known for a device then contact EyeTech
        /// Digital Systems to obtain it.  The password must be set for most functions to work properly.
        /// </para>
        /// <para>
        /// If the password is not set then the functions <see cref="QLDevice_Start" />,
        /// <see cref="QLDevice_GetFrame" />, <see cref="QLDevice_GetStatus" /> and
        /// <see cref="QLCalibration_Initialize" /> will return the value
        /// <see cref="QLError.QL_ERROR_INVALID_PASSWORD" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_Initialize" />
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLDevice_GetFrame" />
        /// <seealso cref="QLDevice_GetStatus" />
        /// <seealso cref="QLDevice_Start" />
        /// <seealso cref="QLError"/>
        /// <param name="deviceID">
        /// The ID of the device for which we want to set the password.  This ID is obtained by calling
        /// the function <see cref="QLDevice_Enumerate" />.
        /// </param>
        /// <param name="password">
        /// A string containing the password for the device.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the password was accepted by the device.
        /// </returns>
        /// <example>
        /// <code>
        /// int deviceID = 1;   // NOTE: Get this value by calling QLDevice_Enumerate().
        /// string password = "12345";  // This is usually printed on the back of the eye tracker.
        /// QLError error = QuickLink2API.QLDevice_SetPassword(deviceID, password);
        /// if (error == QLError.QL_ERROR_OK)
        /// {
        ///     System.Console.WriteLine("Password for device {0} successfully set.", deviceID);
        /// }
        /// else
        /// {
        ///     System.Console.WriteLine("Error: {0}", error.ToString());
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_SetPassword", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLDevice_SetPassword(
                System.Int32 deviceID,
                [In] System.String password);

        /// <summary>
        /// <para>
        /// This function causes a device to start running.  Once running, the device will grab and
        /// process frames as fast as the current settings allow.  Calling this function on a device that
        /// has already been started will not restart the device.  <see cref="QLDevice_Stop" /> must be
        /// called before a device can be restarted.
        /// </para>
        /// <para>
        /// Calling this function using a device group will cause all devices in the group to start.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLDevice_Stop"/>
        /// <seealso cref="QLDevice_Stop_All"/>
        /// <seealso cref="QLDeviceGroup_Create" />
        /// <seealso cref="QLError"/>
        /// <param name="deviceOrGroupID">
        /// The ID of the device or device group to start.  This ID is obtained by calling either the
        /// function <see cref="QLDevice_Enumerate" /> or the function
        /// <see cref="QLDeviceGroup_Create" />.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the device or group was started successfully.
        /// </returns>
        /// <example>
        /// <code>
        /// int deviceOrGroupID = 1;    // NOTE: Get this value by calling QLDevice_Enumerate() or QLDeviceGroup_Create().
        /// QLError error = QuickLink2API.QLDevice_Start(deviceOrGroupID);
        /// if (error == QLError.QL_ERROR_OK)
        /// {
        ///     System.Console.WriteLine("Device or group {0} has been started successfully.", deviceOrGroupID);
        /// }
        /// else
        /// {
        ///     System.Console.WriteLine("Error: {0}", error.ToString());
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_Start", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_Start(
               System.Int32 deviceOrGroupID);

        /// <summary>
        /// <para>
        /// This function causes the device or group to stop running.  Before unloading the library,
        /// this function must be called for each device or group that has been started using the
        /// function <see cref="QLDevice_Start" />.  Undefined behavior could result if the library is
        /// unloaded before a device is stopped.
        /// </para>
        /// <para>
        /// It is best if this function is called before a process has been signaled to exit.  This will
        /// give a device sufficient time to clean up its memory and close its thread before the process'
        /// exit procedure unloads the library automatically.
        /// </para>
        /// <para>
        /// Calling this function using a device group will cause all devices in the group to stop.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLDevice_Start"/>
        /// <seealso cref="QLDevice_Stop_All"/>
        /// <seealso cref="QLDeviceGroup_Create" />
        /// <seealso cref="QLError"/>
        /// <param name="deviceOrGroupID">
        /// The ID of the device or device group to stop.  This ID is obtained by calling either the
        /// function <see cref="QLDevice_Enumerate" /> or the function
        /// <see cref="QLDeviceGroup_Create" />.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the device or group was stopped successfully and all system resources used by the device(s)
        /// have been closed.
        /// </returns>
        /// <example>
        /// <code>
        /// int deviceOrGroupID = 1;    // NOTE: Get this value by calling QLDevice_Enumerate() or QLDeviceGroup_Create().
        /// QLError error = QuickLink2API.QLDevice_Stop(deviceOrGroupID);
        /// if (error == QLError.QL_ERROR_OK)
        /// {
        ///     System.Console.WriteLine("Device or group {0} has been stopped successfully.", deviceOrGroupID);
        /// }
        /// else
        /// {
        ///     System.Console.WriteLine("Error: {0}", error.ToString());
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_Stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_Stop(
               System.Int32 deviceOrGroupID);

        /// <summary>
        /// <para>
        /// This function causes all devices to stop running.  Before unloading the library, each device
        /// that has been started by using the function <see cref="QLDevice_Start" /> must be stopped.
        /// This can be done by calling <see cref="QLDevice_Stop" /> for each device that has been
        /// started, or this function can be called to stop all devices.  Undefined behavior could result
        /// if the library is unloaded before all devices are stopped.
        /// </para>
        /// <para>
        /// It is best if each device is stopped before a process has been signaled to exit.  This will
        /// give a device sufficient time to clean up its memory and close its thread before the process
        /// exit procedure unloads the library automatically.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Start"/>
        /// <seealso cref="QLDevice_Stop"/>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// all devices were stopped successfully and all system resources used by the devices have been
        /// closed.
        /// </returns>
        /// <example>
        /// <code>
        /// QLError error = QuickLink2API.QLDevice_Stop_All();
        /// if (error == QLError.QL_ERROR_OK)
        /// {
        ///     System.Console.WriteLine("All devices have been stopped.");
        /// }
        /// else
        /// {
        ///     System.Console.WriteLine("Error: {0}", error.ToString());
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_Stop_All", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_Stop_All();

        /// <summary>
        /// <para>
        /// This function sets the mode for the indicator lights on the front of the device.
        /// </para>
        /// <para>
        /// Calling this function using a device group will sets the mode for the indicator lights on the
        /// front of all devices in the group.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLDevice_GetIndicator"/>
        /// <seealso cref="QLDeviceGroup_Create" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLIndicatorMode"/>
        /// <seealso cref="QLIndicatorType"/>
        /// <param name="deviceOrGroupID">
        /// The ID of the device or device group whose indicator lights should be set.  This ID is
        /// obtained by calling either the function <see cref="QLDevice_Enumerate" /> or the
        /// function <see cref="QLDeviceGroup_Create" />.
        /// </param>
        /// <param name="type">
        /// The <see cref="QLIndicatorType" /> of the indicator to set.
        /// </param>
        /// <param name="mode">
        /// The <see cref="QLIndicatorMode" /> to set the indicator to.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the indicator was set to the desired mode for the specified device or group.
        /// </returns>
        /// <example>
        /// <code>
        /// int deviceOrGroupID = 1;    // NOTE: Get this value by calling QLDevice_Enumerate() or QLDeviceGroup_Create().
        /// QLError error = QuickLink2API.QLDevice_SetIndicator(deviceOrGroupID, QLIndicatorType.QL_INDICATOR_TYPE_LEFT, QLIndicatorMode.QL_INDICATOR_MODE_ON);
        /// if (error == QLError.QL_ERROR_OK)
        /// {
        ///     System.Console.WriteLine("Left indicator of device or group {0} has been turned on.", deviceOrGroupID);
        /// }
        /// else
        /// {
        ///     System.Console.WriteLine("Error: {0}", error.ToString());
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_SetIndicator", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_SetIndicator(
               System.Int32 deviceOrGroupID,
               QLIndicatorType type,
               QLIndicatorMode mode);

        /// <summary>
        /// <para>
        /// This function gets the current mode for the specified indicator light on the front of the
        /// specified device.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLDevice_SetIndicator"/>
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLIndicatorMode"/>
        /// <seealso cref="QLIndicatorType"/>
        /// <param name="deviceID">
        /// The ID of the device from which to get the indicator mode. This ID is obtained by calling the
        /// function <see cref="QLDevice_Enumerate" />.
        /// </param>
        /// <param name="type">
        /// The <see cref="QLIndicatorType" /> of the indicator to get.
        /// </param>
        /// <param name="mode">
        /// A reference to an uninitialized <see cref="QLIndicatorMode" /> object. This object will
        /// receive the mode of the indicator type for the specified device.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the indicator mode was retrieved from the specified device.
        /// </returns>
        /// <example>
        /// <code>
        /// int deviceID = 1;   // NOTE: Get this value by calling QLDevice_Enumerate().
        /// QLIndicatorMode mode;
        /// QLError error = QuickLink2API.QLDevice_GetIndicator(deviceID, QLIndicatorType.QL_INDICATOR_TYPE_LEFT, out mode);
        /// if (error == QLError.QL_ERROR_OK)
        /// {
        ///     System.Console.WriteLine("Left indicator of device {0} is set to mode {1}", deviceID, mode.ToString());
        /// }
        /// else
        /// {
        ///     System.Console.WriteLine("Error: {0}", error.ToString());
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_GetIndicator", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_GetIndicator(
               System.Int32 deviceID,
               QLIndicatorType type,
               out QLIndicatorMode mode);

        /// <summary>
        /// <para>
        /// This function gets the most recent frame from the device.  It is a blocking function which
        /// will wait for a frame to become available if there is not one ready.  Waiting does not use
        /// CPU time.
        /// </para>
        /// <para>
        /// Only the most recent frame can be retrieved from the device.  To ensure that no frames are
        /// dropped this function needs to be called at least as fast as the frame rate of the device.  A
        /// good way to use this function that will help ensure the retrieval of every frame is to use it
        /// in a loop.  The blocking nature of this function will ensure that the loop will only run as
        /// fast as the device can produce frames.  If other processing in the loop does not exceed the
        /// time that it takes for the device to make another frame available then this function will
        /// sync the loop to the approximate frame rate of the device.
        /// </para>
        /// <para>
        /// Calling this function using a device group will get a frame from only one device in the
        /// group.  If there are no devices that are tracking eyes then the image that is returned
        /// rotates between all devices in the group.  Each device will be selected for 3 seconds before
        /// rotating to the next device.  The order of rotation is based on the order in which the
        /// devices were added to the group.  If a device in the group is tracking eyes then that device
        /// becomes the selected device.  It will stay the selected device until the eyes are lost.  Once
        /// a device begins tracking eyes and becomes the selected device, eye tracking is disabled on
        /// all other devices in the group until the selected device loses the eyes.  If two devices find
        /// eyes at the same time then the device that has eyes closest to the center of the image
        /// becomes the selected device.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLDeviceGroup_Create" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLFrameData"/>
        /// <param name="deviceOrGroupID">
        /// The ID of the device or device group from which to get the most recent frame. This ID is
        /// obtained by calling either the function <see cref="QLDevice_Enumerate" /> or the function
        /// <see cref="QLDeviceGroup_Create" />.
        /// </param>
        /// <param name="waitTime">
        /// The maximum time in milliseconds to wait for a new frame.
        /// </param>
        /// <param name="frame">
        /// A reference to an allocated <see cref="QLFrameData" /> object.  This object receives the data
        /// for the most recent frame.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the most recent frame was successfully retrieved from the device or group.
        /// </returns>
        /// <example>
        /// <code>
        /// int deviceOrGroupID = 1;    // NOTE: Get this value by calling QLDevice_Enumerate() or QLDeviceGroup_Create().
        /// QLFrameData frameData = new QLFrameData();  // Create and allocate a new, empty frame structure to hold the data we read.
        /// // Read 10 frames and output whether the left eye is found in each frame.
        /// for (int i = 0; i&lt;10; i++)
        /// {
        ///     QLError error = QuickLink2API.QLDevice_GetFrame(deviceOrGroupID, 10000, ref frameData);
        ///     if (error == QLError.QL_ERROR_OK)
        ///     {
        ///         System.Console.WriteLine("Left eye found: {0}.", frameData.LeftEye.Found.ToString());
        ///     }
        ///     else
        ///     {
        ///         System.Console.WriteLine("Error: {0}", error.ToString());
        ///     }
        /// }
        /// </code>
        /// </example>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_GetFrame", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_GetFrame(
               System.Int32 deviceOrGroupID,
               System.Int32 waitTime,
               ref QLFrameData frame);

        /// <summary>
        /// <para>
        /// This function applies a calibration container to a device.  If a properly prepared
        /// calibration container is not applied to a device then the gaze point can not be calculated.
        /// Applying an unprepared calibration container will clear the calibration from the specified
        /// device.
        /// </para>
        /// <para>
        /// For greatest accuracy each device should be calibrated for each user, and for each physical
        /// setup of the device and monitor.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_AddBias" />
        /// <seealso cref="QLCalibration_Calibrate" />
        /// <seealso cref="QLCalibration_Cancel" />
        /// <seealso cref="QLCalibration_Create" />
        /// <seealso cref="QLCalibration_Finalize" />
        /// <seealso cref="QLCalibration_GetScoring" />
        /// <seealso cref="QLCalibration_GetStatus" />
        /// <seealso cref="QLCalibration_GetTargets" />
        /// <seealso cref="QLCalibration_Initialize" />
        /// <seealso cref="QLCalibration_Load" />
        /// <seealso cref="QLCalibration_Save" />
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLError"/>
        /// <param name="deviceID">
        /// The ID of the device that is to be calibrated.  This ID is obtained by calling the function
        /// <see cref="QLDevice_Enumerate" />.
        /// </param>
        /// <param name="calibrationID">
        /// The ID of the calibration object to apply.  This ID is obtained by calling either the
        /// function <see cref="QLCalibration_Create" /> or the function
        /// <see cref="QLCalibration_Load" />.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the calibration was successfully applied to the device.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_ApplyCalibration", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_ApplyCalibration(
               System.Int32 deviceID,
               System.Int32 calibrationID);

        /// <summary>
        /// <para>
        /// This function uses the measured distance to the user in order to calculate the radius of the
        /// cornea for each eye.  Using the radii returned by this function as the values for the settings
        /// <see cref="QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT" /> and
        /// <see cref="QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_RIGHT" /> will result in
        /// greater accuracy of the distance output for each frame.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLDevice_ExportSettings"/>
        /// <seealso cref="QLDevice_ImportSettings"/>
        /// <seealso cref="QLDevice_IsSettingSupported" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting"/>
        /// <seealso cref="QLSettings_Create"/>
        /// <seealso cref="QLSettings_GetValue"/>
        /// <seealso cref="QLSettings_Load"/>
        /// <seealso cref="QLSettings_SetValue"/>
        /// <param name="deviceID">
        /// The ID of the device from which to calibrate the eye radius.  This ID is obtained by calling
        /// the function <see cref="QLDevice_Enumerate" />.
        /// </param>
        /// <param name="distance">
        /// The actual measured distance in centimeters from the device to the user at the time this
        /// function is called.
        /// </param>
        /// <param name="leftRadius">
        /// A reference to an uninitialized float that will receive the radius in centimeters of the
        /// cornea of the left eye.
        /// </param>
        /// <param name="rightRadius">
        /// A reference to an uninitialized float that will receive the radius in centimeters of the
        /// cornea of the right eye.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the measuring of the radius of the left and right eyes was successful.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_CalibrateEyeRadius", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError QLDevice_CalibrateEyeRadius(
            System.Int32 deviceID,
            System.Single distance,
            out System.Single leftRadius,
            out System.Single rightRadius);

        /// <summary>
        /// <para>
        /// This function creates a new device group.  A device group is used to perform actions on a
        /// number of devices at once instead of having to do it for each device individually.  Many
        /// functions that accept a device ID can also be used with a group ID.  See individual function
        /// documentation.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLDeviceGroup_AddDevice" />
        /// <seealso cref="QLDeviceGroup_Enumerate" />
        /// <seealso cref="QLDeviceGroup_GetFrame" />
        /// <seealso cref="QLDeviceGroup_RemoveDevice" />
        /// <seealso cref="QLError"/>
        /// <param name="deviceGroupID">
        /// A reference to an uninitialized <see cref="System.Int32" /> object.  This object will receive
        /// the ID of the newly created device group. The ID is a unique identifier that is used to
        /// access the newly created group of devices.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the device group was created successfully.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDeviceGroup_Create", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError QLDeviceGroup_Create(
            out System.Int32 deviceGroupID);

        /// <summary>
        /// <para>
        /// This function adds a device to the given device group.  The order of the devices in the group
        /// is based on the order in which they were added to the group.  Adding a device that is already
        /// in the group will cause it to be removed from its previous position and added to the end of
        /// the device list.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLDeviceGroup_Create" />
        /// <seealso cref="QLDeviceGroup_Enumerate" />
        /// <seealso cref="QLDeviceGroup_GetFrame" />
        /// <seealso cref="QLDeviceGroup_RemoveDevice" />
        /// <seealso cref="QLError"/>
        /// <param name="deviceGroupID">
        /// The ID of the device group to which the device should be added.  This ID is obtained by
        /// calling the function <see cref="QLDeviceGroup_Create" />.
        /// </param>
        /// <param name="deviceID">
        /// The ID of the device that will be added to the device group.  This ID is obtained by calling
        /// the function <see cref="QLDevice_Enumerate" />.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the device was successfully added to the device group.  <see cref="QLError.QL_ERROR_OK" />
        /// will also be returned if the device was already in the group.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDeviceGroup_AddDevice", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError QLDeviceGroup_AddDevice(
            System.Int32 deviceGroupID,
            System.Int32 deviceID);

        /// <summary>
        /// <para>
        /// This function removes a device from the given device group.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLDeviceGroup_AddDevice" />
        /// <seealso cref="QLDeviceGroup_Create" />
        /// <seealso cref="QLDeviceGroup_Enumerate" />
        /// <seealso cref="QLDeviceGroup_GetFrame" />
        /// <seealso cref="QLError"/>
        /// <param name="deviceGroupID">
        /// The ID of the device group from which the device should be removed.  This ID is obtained by
        /// calling the function <see cref="QLDeviceGroup_Create" />.
        /// </param>
        /// <param name="deviceID">
        /// The ID of the device that will be removed from the device group.  This ID is obtained by
        /// calling the function <see cref="QLDevice_Enumerate" />.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the device was successfully removed from the device group. <see cref="QLError.QL_ERROR_OK" />
        /// will also be returned if the device was already missing from the group.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDeviceGroup_RemoveDevice", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError QLDeviceGroup_RemoveDevice(
            System.Int32 deviceGroupID,
            System.Int32 deviceID);

        /// <summary>
        /// <para>
        /// This function enumerates the device group and retrieves a list of device IDs that are part of
        /// the group.  The order of the devices in the buffer is based on the order in which the devices
        /// were added to the group.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLDeviceGroup_AddDevice" />
        /// <seealso cref="QLDeviceGroup_Create" />
        /// <seealso cref="QLDeviceGroup_GetFrame" />
        /// <seealso cref="QLDeviceGroup_RemoveDevice" />
        /// <seealso cref="QLError"/>
        /// <param name="deviceGroupID">
        /// The ID of the device group to enumerate.  This ID is obtained by calling the function
        /// <see cref="QLDeviceGroup_Create" />.
        /// </param>
        /// <param name="numDevices">
        /// A reference to an initialized <see cref="System.Int32" /> object.  When this function is
        /// called, the value contained in this parameter must be set to the length of the
        /// <paramref name="deviceBuffer" /> array; however, when the function returns successfully, the
        /// value will be the actual number of devices that were found in the group.
        /// </param>
        /// <param name="deviceBuffer">
        /// A initialized integer array that will receive the IDs of the devices that are in the group.
        /// </param>
        /// <returns>
        /// <para>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the group was successfully enumerated and the <paramref name="deviceBuffer" /> contains IDs
        /// for all the EyeTech devices in the group.
        /// </para>
        /// <para>
        /// If the return value is <see cref="QLError.QL_ERROR_BUFFER_TOO_SMALL" /> then
        /// <paramref name="deviceBuffer" /> was too small to contain ID values for all the devices that
        /// are in the group, and <paramref name="numDevices" /> will contain the actual number of
        /// devices in the group.  In this case, the <paramref name="deviceBuffer" /> array should
        /// be resized to be at least as large as the value contained in <paramref name="numDevices" />,
        /// and the function should be called again.
        /// </para>
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDeviceGroup_Enumerate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError QLDeviceGroup_Enumerate(
            System.Int32 deviceGroupID,
            ref System.Int32 numDevices,
            [In, Out] System.Int32[] deviceBuffer);

        /// <summary>
        /// <para>
        /// This function gets the most recent frame from each device in the device group. It is a
        /// blocking function which will wait for a frame to become available on each device before
        /// returning. Waiting does not use CPU time. The ordering of the frames will be based on the
        /// the order in which the devices were added to the group. The order can be recalled by
        /// enumerating the group with the function <see cref="QLDeviceGroup_Enumerate" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDevice_Enumerate" />
        /// <seealso cref="QLDeviceGroup_AddDevice" />
        /// <seealso cref="QLDeviceGroup_Create" />
        /// <seealso cref="QLDeviceGroup_Enumerate" />
        /// <seealso cref="QLDeviceGroup_RemoveDevice" />
        /// <seealso cref="QLError"/>
        /// <param name="deviceGroupID">
        /// The ID of the device or device group from which to get the most recent frame. This ID is
        /// obtained by calling the function <see cref="QLDeviceGroup_Create" />.
        /// </param>
        /// <param name="waitTime">
        /// The maximum time in milliseconds to wait for a new frame from a single device.  This time is
        /// used for blocking each device which means that the total time the function could possibly
        /// block can be larger than this time.
        /// </param>
        /// <param name="numFrames">
        /// A reference to an initialized <see cref="System.Int32" /> object containing the size of frame
        /// buffer.  The value should be at least as large as the number of devices in the group.  When
        /// the function returns the integer contains the number of frames that were filled in the frame
        /// buffer array (which will equal the number of devices in the group).
        /// </param>
        /// <param name="frameBuffer">
        /// A initialized <see cref="QLFrameData" /> array that will receive the data for the most recent
        /// frame from each device.
        /// </param>
        /// <returns>
        /// <para>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the frames were successfully collected from all devices in the group.
        /// </para>
        /// <para>
        /// If the return value is <see cref="QLError.QL_ERROR_BUFFER_TOO_SMALL" /> then
        /// <paramref name="frameBuffer" /> was too small to contain all the frames from all the device
        /// in the group, and <paramref name="numFrames" /> will contain the actual number of devices in
        /// the group.  In this case, the <paramref name="frameBuffer" /> array should be resized to be
        /// at least as large as the value contained in <paramref name="numFrames" />, and the function
        /// should be called again.
        /// </para>
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLDeviceGroup_GetFrame", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError QLDeviceGroup_GetFrame(
            System.Int32 deviceGroupID,
            System.Int32 waitTime,
            ref System.Int32 numFrames,
            [In, Out] QLFrameData[] frameBuffer);

        #endregion Group: Device

        #region Group: Settings

        /// <summary>
        /// <para>
        /// This function loads settings from a file into a settings container.  If there are settings in
        /// the file that are currently in the settings container then the values from the file will
        /// overwrite the current values.  If a setting is in the file multiple times with different
        /// values, the last entry takes precedence.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="path">
        /// [in] A NULL-terminated string containing the full pathname of the settings
        /// file.
        /// </param>
        /// <param name="settingsID">
        /// A settingsID is a unique identifier that is used to specify a settings container.  In
        /// particular, this parameter is a reference to an initialized <see cref="System.Int32" />
        /// object.  If the value contained in <paramref name="settingsID" /> refers to a valid settings
        /// container then this function call will load the settings into the specified container;
        /// However, if the the value contained in <paramref name="settingsID" /> does not refer to a
        /// valid settings container, then a new settings container will be created and its ID will be
        /// written into the <paramref name="settingsID" /> parameter when the call successfully returns.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the settings were successfully loaded.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_Load", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_Load(
               [In] System.String path,
               ref System.Int32 settingsID);

        /// <summary>
        /// <para>
        /// This function saves the settings of a settings container to a file. If the file already
        /// exists then its contents are modified. Only the values of the settings that are in the
        /// settings container are changed. New settings are added to the end of the file. If the file
        /// did not previously exist then a new file is created.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="path">
        /// A string containing the full pathname of the settings file.
        /// </param>
        /// <param name="settingsID">
        /// The ID of the settings container containing the settings to save.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the settings were successfully saved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_Save", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_Save(
               [In] System.String path,
               System.Int32 settingsID);

        /// <summary>
        /// <para>
        /// This function creates a new settings container. If the source ID references a valid settings
        /// container then its contents will be copied into the newly created settings container. If the
        /// source ID does not reference a valid settings container then the new settings container will
        /// be empty.
        /// </para>
        /// <para>
        /// If settings ID pointer references a QLSettingsId that has already been created then the
        /// container associated with this ID will be cleared first.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="sourceID">
        /// The ID of the settings container from which to copy settings.  This ID is obtained by calling
        /// either this function or the function <see cref="QLSettings_Load" />.  To create an empty
        /// settings container, set this value to zero.
        /// </param>
        /// <param name="settingsID">
        /// A settingsID is a unique identifier that is used to specify a settings container.  In
        /// particular, this parameter is a reference to an uninitialized <see cref="System.Int32" />
        /// object, which will receive the ID of the new settings container.
        /// </param>
        /// <returns>
        /// The success of the function. If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the new settings container was successfully created.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_Create", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_Create(
               System.Int32 sourceID,
               out System.Int32 settingsID);

        /// <summary>
        /// <para>
        /// This function adds a setting to a settings container and gives it an initial value of zero.
        /// If the setting already exists in the settings container then its value remains unchanged.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that will receive the new setting.  This ID is obtained by
        /// calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// A <see cref="QL_SETTINGS"/> member, or a string containing the name of the setting to add to
        /// the settings container.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting was successfully added to the settings container or the setting was already in
        /// the settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_AddSetting", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_AddSetting(
               System.Int32 settingsID,
               [In] System.String settingName);

        /// <summary>
        /// <para>
        /// This function removes a setting from a settings container.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container from which the setting will be removed.  This ID is obtained
        /// by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// A <see cref="QL_SETTINGS"/> member, or a string containing the name of the setting that will
        /// be removed from the settings container.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting was successfully removed from the settings container. If the return value is
        /// <see cref="QLError.QL_ERROR_NOT_FOUND" /> then the setting was not in the container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_RemoveSetting", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_RemoveSetting(
               System.Int32 settingsID,
               [In] System.String settingName);

        /// <summary>
        /// <para>
        /// This function sets the value of a setting in a settings container.  If the setting already
        /// existed in the settings container then its value is updated.  If the setting did not already
        /// exist in the settings container then it is added with the specified value.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValueBool" />
        /// <seealso cref="QLSettings_SetValueDouble" />
        /// <seealso cref="QLSettings_SetValueFloat" />
        /// <seealso cref="QLSettings_SetValueInt" />
        /// <seealso cref="QLSettings_SetValueInt8" />
        /// <seealso cref="QLSettings_SetValueInt16" />
        /// <seealso cref="QLSettings_SetValueInt32" />
        /// <seealso cref="QLSettings_SetValueInt64" />
        /// <seealso cref="QLSettings_SetValueString" />
        /// <seealso cref="QLSettings_SetValueUInt" />
        /// <seealso cref="QLSettings_SetValueUInt8" />
        /// <seealso cref="QLSettings_SetValueUInt16" />
        /// <seealso cref="QLSettings_SetValueUInt32" />
        /// <seealso cref="QLSettings_SetValueUInt64" />
        /// <seealso cref="QLSettings_SetValueVoidPointer" />
        /// <seealso cref="QLSettingType"/>
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="settingType">
        /// The <see cref="QLSettingType"/> of the data that is being passed in.  This tells the API how
        /// to interpret the data pointed to by <paramref name="value" />
        /// </param>
        /// <param name="value">
        /// An initialized <see cref="System.IntPtr" /> object which contains a reference to the value to
        /// write to the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValue", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_SetValue(
               System.Int32 settingsID,
               [In] System.String settingName,
               QLSettingType settingType,
               [In] System.IntPtr value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_INT" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueInt", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueInt(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Int32 value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_INT8" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueInt8", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueInt8(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.SByte value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_INT16" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueInt16", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueInt16(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Int16 value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_INT32" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueInt32", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueInt32(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Int32 value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_INT64" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueInt64", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueInt64(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Int64 value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_UINT" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueUInt", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueUInt(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.UInt32 value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_UINT8" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueUInt8", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueUInt8(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Byte value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_UINT16" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueUInt16", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueUInt16(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.UInt16 value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_UINT32" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueUInt32", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueUInt32(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.UInt32 value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_UINT64" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueUInt64", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueUInt64(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.UInt64 value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_FLOAT" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueFloat", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueFloat(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Single value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_DOUBLE" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueDouble", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueDouble(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Double value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_BOOL" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueBool", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueBool(
                System.Int32 settingsID,
                [In] System.String settingName,
                [MarshalAs(UnmanagedType.VariantBool)] System.Boolean value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_VOID_POINTER" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueVoidPointer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueVoidPointer(
                System.Int32 settingsID,
                [In] System.String settingName,
                [In] System.IntPtr value);

        /// <summary>
        /// <para>
        /// Set the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_STRING" />.  This is a wrapper for
        /// <see cref="QLSettings_SetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be altered or will
        /// receive the new setting if it did not exist previously.  This ID is obtained by calling
        /// either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// altered (or added to) the settings container.
        /// </param>
        /// <param name="value">
        /// The value to write to the specified setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the setting value was successfully updated or the setting was successfully added to the
        /// settings container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueString", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueString(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.String value);

        /// <summary>
        /// <para>
        /// This function gets the value of a setting in a settings container if the setting exists.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValueBool" />
        /// <seealso cref="QLSettings_GetValueDouble" />
        /// <seealso cref="QLSettings_GetValueFloat" />
        /// <seealso cref="QLSettings_GetValueInt" />
        /// <seealso cref="QLSettings_GetValueInt8" />
        /// <seealso cref="QLSettings_GetValueInt16" />
        /// <seealso cref="QLSettings_GetValueInt32" />
        /// <seealso cref="QLSettings_GetValueInt64" />
        /// <seealso cref="QLSettings_GetValueString" />
        /// <seealso cref="QLSettings_GetValueUInt" />
        /// <seealso cref="QLSettings_GetValueUInt8" />
        /// <seealso cref="QLSettings_GetValueUInt16" />
        /// <seealso cref="QLSettings_GetValueUInt32" />
        /// <seealso cref="QLSettings_GetValueUInt64" />
        /// <seealso cref="QLSettings_GetValueVoidPointer" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="settingType">
        /// The <see cref="QLSettingType"/> of the desired data.  This tells the API how to pass the data
        /// via the <paramref name="value" /> parameter.
        /// </param>
        /// <param name="size">
        /// If the type is <see cref="QLSettingType.QL_SETTING_TYPE_STRING" /> then this is the size of
        /// the buffer pointed to by <paramref name="value" />; otherwise, this value is ignored.
        /// </param>
        /// <param name="value">
        /// A reference to an initialized <see cref="System.IntPtr" /> object which will receive a
        /// reference to the value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValue", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_GetValue(
               System.Int32 settingsID,
               [In] System.String settingName,
               QLSettingType settingType,
               System.Int32 size,
               ref System.IntPtr value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_INT" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.Int32" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueInt", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueInt(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.Int32 value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_INT8" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.SByte" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueInt8", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueInt8(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.SByte value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_INT16" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.Int16" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueInt16", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueInt16(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.Int16 value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_INT32" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.Int32" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueInt32", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueInt32(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.Int32 value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_INT64" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.Int64" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueInt64", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueInt64(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.Int64 value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_UINT" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.UInt32" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueUInt", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueUInt(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.UInt32 value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_UINT8" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.Byte" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueUInt8", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueUInt8(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.Byte value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_UINT16" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.UInt16" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueUInt16", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueUInt16(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.UInt16 value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_UINT32" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.UInt32" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueUInt32", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueUInt32(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.UInt32 value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_UINT64" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.UInt64" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueUInt64", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueUInt64(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.UInt64 value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_FLOAT" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.Single" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueFloat", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueFloat(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.Single value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_DOUBLE" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.Double" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueDouble", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueDouble(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.Double value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_BOOL" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an uninitialized <see cref="System.Boolean" /> object which will receive the
        /// value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueBool", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueBool(
                System.Int32 settingsID,
                [In] System.String settingName,
                [MarshalAs(UnmanagedType.VariantBool)] out System.Boolean value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_VOID_POINTER" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="value">
        /// A reference to an initialized <see cref="System.IntPtr" /> object which will receive a
        /// reference to the value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueVoidPointer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueVoidPointer(
                System.Int32 settingsID,
                [In] System.String settingName,
                ref System.IntPtr value);

        /// <summary>
        /// <para>
        /// Get the value of a setting in a settings container using the setting type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_STRING" />. This is a wrapper for
        /// <see cref="QLSettings_GetValue" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_GetValueStringSize" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="size">
        /// This is the size of the buffer pointed to by <paramref name="value" />.
        /// </param>
        /// <param name="value">
        /// A reference to an initialized <see cref="System.Text.StringBuilder" /> object which will
        /// receive the value of the setting.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the value of the setting was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueString", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueString(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Int32 size,
                System.Text.StringBuilder value);

        /// <summary>
        /// <para>
        /// This function gets the size of the string, including the terminating NULL character, for the
        /// specified setting's value.  A buffer would have to be at least as large as the value returned
        /// by <paramref name="size" /> to successfully get a value of type
        /// <see cref="QLSettingType.QL_SETTING_TYPE_STRING" /> using the function
        /// <see cref="QLSettings_GetValue" />, or by using the <see cref="QLSettings_GetValueString" />
        /// function.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTINGS" />
        /// <seealso cref="QLError"/>
        /// <seealso cref="QLSettings_AddSetting" />
        /// <seealso cref="QLSettings_Create" />
        /// <seealso cref="QLSettings_GetValue" />
        /// <seealso cref="QLSettings_GetValueString" />
        /// <seealso cref="QLSettings_Load" />
        /// <seealso cref="QLSettings_Save" />
        /// <seealso cref="QLSettings_RemoveSetting" />
        /// <seealso cref="QLSettings_SetValue" />
        /// <param name="settingsID">
        /// The ID of the settings container that either contains the setting to be read.  This ID is
        /// obtained by calling either the function <see cref="QLSettings_Create" /> or the function
        /// <see cref="QLSettings_Load" />.
        /// </param>
        /// <param name="settingName">
        /// The <see cref="QL_SETTINGS" /> (or a string containing the name of the setting) that will be
        /// retrieved from the settings container.
        /// </param>
        /// <param name="size">
        /// A reference to an uninitialized <see cref="System.Int32" /> object that will receive the
        /// length of the string value.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the string length for the setting string was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueStringSize", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_GetValueStringSize(
               System.Int32 settingsID,
               [In] System.String settingName,
               out System.Int32 size);

        #endregion Group: Settings

        #region Group: Calibration

        /// <summary>
        /// <para>
        /// This function loads calibration data from a file into a calibration container.  The file must
        /// have been created by calling the function <see cref="QLCalibration_Save" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_AddBias"/>
        /// <seealso cref="QLCalibration_Calibrate"/>
        /// <seealso cref="QLCalibration_Cancel"/>
        /// <seealso cref="QLCalibration_Create"/>
        /// <seealso cref="QLCalibration_Finalize"/>
        /// <seealso cref="QLCalibration_GetScoring"/>
        /// <seealso cref="QLCalibration_GetStatus"/>
        /// <seealso cref="QLCalibration_GetTargets"/>
        /// <seealso cref="QLCalibration_Initialize"/>
        /// <seealso cref="QLCalibration_Save"/>
        /// <param name="path">
        /// A <see cref="string" /> containing the full pathname of the calibration file.
        /// </param>
        /// <param name="calibrationID">
        /// An reference to an initialized <see cref="System.Int32" /> object.  If the object refers
        /// to a valid calibration container then that calibration container will receive the loaded
        /// calibration; however, if the object does not refer to a valid calibration container then a
        /// new calibration container will be created and this object will receive its ID.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the calibration was successfully loaded.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Load", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Load(
               [In] System.String path,
               ref System.Int32 calibrationID);

        /// <summary>
        /// <para>
        /// This function saves calibration data to a file.  The calibration data can later be loaded by
        /// calling the function <see cref="QLCalibration_Load" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_AddBias"/>
        /// <seealso cref="QLCalibration_Calibrate"/>
        /// <seealso cref="QLCalibration_Cancel"/>
        /// <seealso cref="QLCalibration_Create"/>
        /// <seealso cref="QLCalibration_Finalize"/>
        /// <seealso cref="QLCalibration_GetScoring"/>
        /// <seealso cref="QLCalibration_GetStatus"/>
        /// <seealso cref="QLCalibration_GetTargets"/>
        /// <seealso cref="QLCalibration_Initialize"/>
        /// <seealso cref="QLCalibration_Load"/>
        /// <param name="path">
        /// A string containing the full pathname of the calibration file.
        /// </param>
        /// <param name="calibrationID">
        /// The ID of the calibration container whose data will be saved.  This ID is obtained by
        /// calling either the function <see cref="QLCalibration_Create" /> or the function
        /// <see cref="QLCalibration_Load" />.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the calibration was successfully saved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Save", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Save(
               [In] System.String path,
               System.Int32 calibrationID);

        /// <summary>
        /// <para>
        /// This function creates a new calibration container.  If the source ID references a valid
        /// calibration container then its data will be copied into the newly created calibration
        /// container.  If the source ID does not reference a valid settings container then the new
        /// settings container will be empty.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_AddBias"/>
        /// <seealso cref="QLCalibration_Calibrate"/>
        /// <seealso cref="QLCalibration_Cancel"/>
        /// <seealso cref="QLCalibration_Finalize"/>
        /// <seealso cref="QLCalibration_GetScoring"/>
        /// <seealso cref="QLCalibration_GetStatus"/>
        /// <seealso cref="QLCalibration_GetTargets"/>
        /// <seealso cref="QLCalibration_Initialize"/>
        /// <seealso cref="QLCalibration_Load"/>
        /// <seealso cref="QLCalibration_Save"/>
        /// <param name="sourceID">
        /// The ID of the calibration container from which to copy calibration data.  To create an empty
        /// settings container, set this value to zero.
        /// </param>
        /// <param name="calibrationID">
        /// A reference to an uninitialized <see cref="System.Int32" /> object.  This object will
        /// receive the ID of the newly created calibration container.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the new calibration container was successfully created.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Create", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Create(
               System.Int32 sourceID,
               out System.Int32 calibrationID);

        /// <summary>
        /// <para>
        /// This function initializes a calibration container which makes it ready to receive new
        /// calibration data from a device.  Any calibration data previously in the container is stored in
        /// temporary memory until <see cref="QLCalibration_Finalize" /> is called.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_AddBias"/>
        /// <seealso cref="QLCalibration_Calibrate"/>
        /// <seealso cref="QLCalibration_Cancel"/>
        /// <seealso cref="QLCalibration_Create"/>
        /// <seealso cref="QLCalibration_Finalize"/>
        /// <seealso cref="QLCalibration_GetScoring"/>
        /// <seealso cref="QLCalibration_GetStatus"/>
        /// <seealso cref="QLCalibration_GetTargets"/>
        /// <seealso cref="QLCalibration_Load"/>
        /// <seealso cref="QLCalibration_Save"/>
        /// <seealso cref="QLCalibrationType"/>
        /// <param name="deviceID">
        /// The ID of the device from which to receive calibration data.  This ID is obtained by calling
        /// the function <see cref="QLDevice_Enumerate" />.
        /// </param>
        /// <param name="calibrationID">
        /// The ID of the calibration container that will receive the new calibration data.  This ID is
        /// obtained by calling either the function <see cref="QLCalibration_Create" /> or the function
        /// <see cref="QLCalibration_Load" />.
        /// </param>
        /// <param name="type">
        /// The <see cref="QLCalibrationType" /> type of the calibration to perform.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the calibration container was successfully initialized and is now ready to receive new
        /// calibration data.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Initialize", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Initialize(
               System.Int32 deviceID,
               System.Int32 calibrationID,
               QLCalibrationType type);

        /// <summary>
        /// <para>
        /// This function gets the locations and IDs of the targets for the calibration.  It can be
        /// called to retrieve target positions from a calibration container that is calibrating or one
        /// that has already been finalized.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_AddBias"/>
        /// <seealso cref="QLCalibration_Calibrate"/>
        /// <seealso cref="QLCalibration_Cancel"/>
        /// <seealso cref="QLCalibration_Create"/>
        /// <seealso cref="QLCalibration_Finalize"/>
        /// <seealso cref="QLCalibration_GetScoring"/>
        /// <seealso cref="QLCalibration_GetStatus"/>
        /// <seealso cref="QLCalibration_Initialize"/>
        /// <seealso cref="QLCalibration_Load"/>
        /// <seealso cref="QLCalibration_Save"/>
        /// <seealso cref="QLCalibrationTarget"/>
        /// <param name="calibrationID">
        /// The ID of a calibration container from which to get the target data.  This ID is obtained by
        /// calling either the function <see cref="QLCalibration_Create" /> or the function
        /// <see cref="QLCalibration_Load" />.
        /// </param>
        /// <param name="numTargets">
        /// A reference to an initialized <see cref="System.Int32" /> object containing the size of the
        /// target buffer pointed to by <paramref name="targets" />.  When the function returns this
        /// contains the number of targets for the calibration.
        /// </param>
        /// <param name="targets">
        /// An array of initialized <see cref="QLCalibrationTarget" /> objects that will receive the data
        /// for the calibration points.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the target positions were successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_GetTargets", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_GetTargets(
               System.Int32 calibrationID,
               ref System.Int32 numTargets,
                [In, Out] QLCalibrationTarget[] targets);

        /// <summary>
        /// <para>
        /// This function causes the calibration container to collect data from a device for a specific
        /// target location.  It should be called at least once for each target that was received by
        /// calling the function <see cref="QLCalibration_GetTargets" />.  If this function is called
        /// multiple times for the same target and not all targets have been calibrated then the data
        /// collected by the last call will overwrite previous data.  If this function is called multiple
        /// times for the same target and all targets have been calibrated then the data collected by the
        /// last call will only overwrite the previous data if it improves the calibration overall.
        /// </para>
        /// <para>
        /// This function must be called for each target after a calibration has been initialized but
        /// before it has been finalized.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_AddBias"/>
        /// <seealso cref="QLCalibration_Cancel"/>
        /// <seealso cref="QLCalibration_Create"/>
        /// <seealso cref="QLCalibration_Finalize"/>
        /// <seealso cref="QLCalibration_GetScoring"/>
        /// <seealso cref="QLCalibration_GetStatus"/>
        /// <seealso cref="QLCalibration_GetTargets"/>
        /// <seealso cref="QLCalibration_Initialize"/>
        /// <seealso cref="QLCalibration_Load"/>
        /// <seealso cref="QLCalibration_Save"/>
        /// <param name="calibrationID">
        /// The ID of a calibration container which has been initialized and is ready to receive
        /// calibration data.  This ID is obtained by calling either the function
        /// <see cref="QLCalibration_Create" /> or the function <see cref="QLCalibration_Load" />.
        /// </param>
        /// <param name="targetID">
        /// The ID of a target that the user is looking at.  This ID is obtained by calling the function
        /// <see cref="QLCalibration_GetTargets" />.  Usually there should be a target drawn on the
        /// screen at the location referenced by this target before calling this function.
        /// </param>
        /// <param name="duration">
        /// The length of time that the API will collect calibration data.  For best results the user
        /// should be looking at the target position the entire time.
        /// </param>
        /// <param name="block">
        /// A flag to determine whether this function should block.  If this is true then the function
        /// will block and not return until the API is done collecting data for the calibration point.
        /// If this is false then this function will return immediately and the status of the data
        /// collection can be determined by calling the function
        /// <see cref="QLCalibration_GetStatus" />.
        /// </param>
        /// <returns>
        /// The success of the function.  If the function blocks then a return value of
        /// <see cref="QLError.QL_ERROR_OK" /> means that the duration has finished and that data was
        /// gathered for the target.  If the function does not block then a return value of
        /// <see cref="QLError.QL_ERROR_OK" /> means that calibration data collection was successfully
        /// started for the target.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Calibrate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Calibrate(
               System.Int32 calibrationID,
               System.Int32 targetID,
               System.Int32 duration,
               [MarshalAs(UnmanagedType.VariantBool)] System.Boolean block);

        /// <summary>
        /// <para>
        /// This function gets the the score of a calibration target for a particular eye.  The eye must
        /// have been detected at least once for each target of the calibration in order to produce a
        /// score.
        /// </para>
        /// <para>
        /// This function can be called before or after a calibration has been finalized.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_AddBias"/>
        /// <seealso cref="QLCalibration_Calibrate"/>
        /// <seealso cref="QLCalibration_Cancel"/>
        /// <seealso cref="QLCalibration_Create"/>
        /// <seealso cref="QLCalibration_Finalize"/>
        /// <seealso cref="QLCalibration_GetStatus"/>
        /// <seealso cref="QLCalibration_GetTargets"/>
        /// <seealso cref="QLCalibration_Initialize"/>
        /// <seealso cref="QLCalibration_Load"/>
        /// <seealso cref="QLCalibration_Save"/>
        /// <seealso cref="QLCalibrationScore" />
        /// <seealso cref="QLEyeType" />
        /// <param name="calibrationID">
        /// The ID of a calibration container whose data will be used to calculate a score.  This ID is
        /// obtained by calling either the function <see cref="QLCalibration_Create" /> or the function
        /// <see cref="QLCalibration_Load" />.
        /// </param>
        /// <param name="targetID">
        /// The ID of a target whose score will be retrieved.  This ID is obtained by calling the
        /// function <see cref="QLCalibration_GetTargets" />.
        /// </param>
        /// <param name="eye">
        /// The <see cref="QLEyeType"/> of the eye whose score should be retrieved.
        /// </param>
        /// <param name="score">
        /// A reference to an uninitialized <see cref="QLCalibrationScore" /> object that will receive
        /// the score.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the score was successfully retrieved.  If the return value is
        /// <see cref="QLError.QL_ERROR_INTERNAL_ERROR" /> then use the function
        /// <see cref="QLCalibration_GetStatus" /> to get extended error information.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_GetScoring", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_GetScoring(
               System.Int32 calibrationID,
               System.Int32 targetID,
               QLEyeType eye,
               out QLCalibrationScore score);

        /// <summary>
        /// <para>
        /// This function retrieves the status of a calibration target for a given calibration container.
        /// It can be called before or after a calibration has been finalized.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_AddBias"/>
        /// <seealso cref="QLCalibration_Calibrate"/>
        /// <seealso cref="QLCalibration_Cancel"/>
        /// <seealso cref="QLCalibration_Create"/>
        /// <seealso cref="QLCalibration_Finalize"/>
        /// <seealso cref="QLCalibration_GetScoring"/>
        /// <seealso cref="QLCalibration_GetTargets"/>
        /// <seealso cref="QLCalibration_Initialize"/>
        /// <seealso cref="QLCalibration_Load"/>
        /// <seealso cref="QLCalibration_Save"/>
        /// <seealso cref="QLCalibrationStatus" />
        /// <param name="calibrationID">
        /// The ID of a calibration container whose data will be used to determine a status for the
        /// target.  This ID is obtained by calling either the function
        /// <see cref="QLCalibration_Create" /> or the function <see cref="QLCalibration_Load" />.
        /// </param>
        /// <param name="targetID">
        /// The ID of a target whose status will be retrieved.  This ID is obtained by calling the
        /// function <see cref="QLCalibration_GetTargets" />.
        /// </param>
        /// <param name="calibrationStatus">
        /// A reference to an uninitialized <see cref="QLCalibrationStatus" /> object that will receive
        /// the status.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the calibration status for the target was successfully retrieved.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_GetStatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_GetStatus(
               System.Int32 calibrationID,
               System.Int32 targetID,
               out QLCalibrationStatus calibrationStatus);

        /// <summary>
        /// <para>
        /// This function finalizes a calibration after it is complete.  It should be called when
        /// calibration data has been successfully collected at each target position and the target
        /// scores meet the user requirements.
        /// </para>
        /// <para>
        /// This function clears any previous calibration data that was stored in the container and
        /// replaces it with the new calibration data.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_AddBias"/>
        /// <seealso cref="QLCalibration_Calibrate"/>
        /// <seealso cref="QLCalibration_Cancel"/>
        /// <seealso cref="QLCalibration_Create"/>
        /// <seealso cref="QLCalibration_GetScoring"/>
        /// <seealso cref="QLCalibration_GetStatus"/>
        /// <seealso cref="QLCalibration_GetTargets"/>
        /// <seealso cref="QLCalibration_Initialize"/>
        /// <seealso cref="QLCalibration_Load"/>
        /// <seealso cref="QLCalibration_Save"/>
        /// <param name="calibrationID">
        /// The ID of a calibration container to finalize.  This ID is obtained by calling either the
        /// function <see cref="QLCalibration_Create" /> or the function
        /// <see cref="QLCalibration_Load" />.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the calibration was successfully finalized.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Finalize", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Finalize(
               System.Int32 calibrationID);

        /// <summary>
        /// <para>
        /// This function cancels a calibration.  Recently collected calibration data is cleared and the
        /// calibration container is restored to the state it was in before
        /// <see cref="QLCalibration_Initialize" /> was called.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_AddBias"/>
        /// <seealso cref="QLCalibration_Calibrate"/>
        /// <seealso cref="QLCalibration_Create"/>
        /// <seealso cref="QLCalibration_Finalize"/>
        /// <seealso cref="QLCalibration_GetScoring"/>
        /// <seealso cref="QLCalibration_GetStatus"/>
        /// <seealso cref="QLCalibration_GetTargets"/>
        /// <seealso cref="QLCalibration_Initialize"/>
        /// <seealso cref="QLCalibration_Load"/>
        /// <seealso cref="QLCalibration_Save"/>
        /// <param name="calibrationID">
        /// The ID of a calibration container to cancel.  This ID is obtained by calling either the
        /// function <see cref="QLCalibration_Create" /> or the function
        /// <see cref="QLCalibration_Load" />.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the calibration was successfully canceled.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Cancel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Cancel(
               System.Int32 calibrationID);

        /// <summary>
        /// <para>
        /// Add a bias to the data in the calibration container.
        /// </para>
        /// </summary>
        /// <seealso cref="QLCalibration_Calibrate"/>
        /// <seealso cref="QLCalibration_Cancel"/>
        /// <seealso cref="QLCalibration_Create"/>
        /// <seealso cref="QLCalibration_Finalize"/>
        /// <seealso cref="QLCalibration_GetScoring"/>
        /// <seealso cref="QLCalibration_GetStatus"/>
        /// <seealso cref="QLCalibration_GetTargets"/>
        /// <seealso cref="QLCalibration_Initialize"/>
        /// <seealso cref="QLCalibration_Load"/>
        /// <seealso cref="QLCalibration_Save"/>
        /// <seealso cref="QLEyeType" />
        /// <param name="calibrationID">
        /// The ID of a calibration container to which the bias will be added. This ID is obtained by
        /// calling either the function <see cref="QLCalibration_Create" /> or the function
        /// <see cref="QLCalibration_Load" />.
        /// </param>
        /// <param name="eye">
        /// The <see cref="QLEyeType" /> of the eye to which the bias should be added.
        /// </param>
        /// <param name="xOffset">
        /// The percent of the screen in the x direction that the bias should affect the gaze point.
        /// Negative values will cause the resulting gaze point to be left of the current position.
        /// Positive values will cause the resulting gaze point to be right of the current position.
        /// </param>
        /// <param name="yOffset">
        /// The percent of the screen in the Y direction that the bias should affect the gaze point.
        /// Negative values will cause the resulting gaze point to be above the current position.
        /// Positive values will cause the resulting gaze point to be below the current position.
        /// </param>
        /// <returns>
        /// The success of the function.  If the return value is <see cref="QLError.QL_ERROR_OK" /> then
        /// the bias was successfully added to the calibration container.
        /// </returns>
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_AddBias", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_AddBias(
               System.Int32 calibrationID,
               QLEyeType eye,
               System.Single xOffset,
               System.Single yOffset);

        #endregion Group: Calibration
    }
}