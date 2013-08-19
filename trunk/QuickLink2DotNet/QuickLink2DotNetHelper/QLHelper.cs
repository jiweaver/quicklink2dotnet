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
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace QuickLink2DotNetHelper
{
    /// <summary>
    /// A helper library for use with the QuickLink2DotNet wrapper.  The
    /// library makes tasks like finding and initializing eye tracker devices
    /// very simple so that users can get right to their desired tasks.
    /// </summary>
    public partial class QLHelper
    {
        /// <summary>
        /// The path to the directory where the QuickLink2DotNetHelper library stores eye tracker
        /// configuration files (i.e., the settings and calibration files for each device).
        /// </summary>
        public static string ConfigDirectory
        {
            get { return QLHelper._configDirectory; }
        }
        private static string _configDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QuickLink2DotNet");

        /// <summary>
        /// The path to the settings file for this eye tracker device.
        /// </summary>
        public string SettingsFilename
        {
            get { return this._settingsFilename; }
        }
        private string _settingsFilename;
        private static string _settingsFilenamePrefix = "SN_";
        private static string _settingsFilenamePostfix = ".qls";

        /// <summary>
        /// The path to the calibration file for this eye tracker device.
        /// </summary>
        public string CalibrationFilename
        {
            get { return this._calibrationFilename; }
        }
        private string _calibrationFilename;
        private static string _calibrationFilenamePrefix = "SN_";
        private static string _calibrationFilenamePostfix = ".qlc";

        /// <summary>
        /// The <see cref="QuickLink2DotNet.QLDeviceInfo" /> returned by
        /// <see cref="QuickLink2DotNet.QuickLink2API.QLDevice_GetInfo" /> for this eye tracker device
        /// (i.e., the model, the serial number, and the sensor dimensions).
        /// </summary>
        public QLDeviceInfo DeviceInfo
        {
            get { return this._deviceInfo; }
        }
        private QLDeviceInfo _deviceInfo;

        /// <summary>
        /// The ID of this eye tracker device.  This is usually obtained by calling the
        /// <see cref="QuickLink2DotNet.QuickLink2API.QLDevice_Enumerate" /> function.
        /// </summary>
        public int DeviceId
        {
            get { return this._deviceId; }
        }
        private int _deviceId;

        private QLHelper(int deviceId, QLDeviceInfo info)
        {
            this._deviceId = deviceId;
            this._deviceInfo = info;
            this._settingsFilename = System.IO.Path.Combine(QLHelper.ConfigDirectory, string.Format("{0}{1}{2}", QLHelper._settingsFilenamePrefix, info.serialNumber, QLHelper._settingsFilenamePostfix));
            this._calibrationFilename = System.IO.Path.Combine(QLHelper.ConfigDirectory, string.Format("{0}{1}{2}", QLHelper._calibrationFilenamePrefix, info.serialNumber, QLHelper._calibrationFilenamePostfix));
        }
    }
}