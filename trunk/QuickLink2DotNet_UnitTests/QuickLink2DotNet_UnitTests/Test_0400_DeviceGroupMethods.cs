#region License

/* Description: QuickLink2DotNet Unit Tests
 * Unit Test Authors: Brianna Peters, Maulik Mistry, Justin Weaver
 *
 * Copyright © 2009-2012 EyeTech Digital Systems <support@eyetechds.com>
 * Copyright © 2013 Justin Weaver
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

/* Note: These unit tests are part of QuickLink2DotNet, which is a .NET wrapper
 * for the QuickLink2 API used to control eye trackers produced by EyeTech
 * Digital Systems, Inc. <http://www.eyetechds.com>.  QuickLink2DotNet is not a
 * product of EyeTech Digital Systems, Inc.
 *
 * QuickLink2DotNet Homepage: http://quicklinkapi4net.googlecode.com
 * QuickLink2DotNet Copyright © 2011-2013 Justin Weaver
 */

#endregion License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using QuickLink2DotNet;

namespace QuickLink2DotNet_UnitTests
{
    class Test_0400_DeviceGroupMethods
    {
        [Test]
        public void Test_0410_QLDeviceGroup_Create()
        {
            int deviceGroupId;
            QLError error = QuickLink2API.QLDeviceGroup_Create(out deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.Greater(deviceGroupId, 0);
        }

        [Test]
        public void Test_0420_QLDeviceGroup_AddDevice()
        {
            // Note: This only uses the initially chosen device.

            int deviceGroupId;
            QLError error = QuickLink2API.QLDeviceGroup_Create(out deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.Greater(deviceGroupId, 0);

            error = QuickLink2API.QLDeviceGroup_AddDevice(deviceGroupId, Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0430_QLDeviceGroup_Enumerate()
        {
            // Note: This only uses the initially chosen device.

            int deviceGroupId;
            QLError error = QuickLink2API.QLDeviceGroup_Create(out deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.Greater(deviceGroupId, 0);

            error = QuickLink2API.QLDeviceGroup_AddDevice(deviceGroupId, Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            int numDevices = 1;
            int[] deviceIds = new int[numDevices];
            error = QuickLink2API.QLDeviceGroup_Enumerate(deviceGroupId, ref numDevices, deviceIds);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(1, numDevices);
            Assert.AreEqual(Test_SetUp.Helper.DeviceId, deviceIds[0]);
        }

        [Test]
        public void Test_0440_QLDeviceGroup_RemoveDevice()
        {
            // Note: This only uses the initially chosen device.

            int deviceGroupId;
            QLError error = QuickLink2API.QLDeviceGroup_Create(out deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.Greater(deviceGroupId, 0);

            error = QuickLink2API.QLDeviceGroup_AddDevice(deviceGroupId, Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            int numDevices = 1;
            int[] deviceIds = new int[numDevices];
            error = QuickLink2API.QLDeviceGroup_Enumerate(deviceGroupId, ref numDevices, deviceIds);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(1, numDevices);
            Assert.AreEqual(Test_SetUp.Helper.DeviceId, deviceIds[0]);

            QLFrameData frameData = new QLFrameData();
            error = QuickLink2API.QLDevice_GetFrame(Test_SetUp.Helper.DeviceId, 2000, ref frameData);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDeviceGroup_RemoveDevice(deviceGroupId, Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDeviceGroup_Enumerate(deviceGroupId, ref numDevices, deviceIds);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(0, numDevices);
        }

        [Test]
        public void Test_0450_QLDevice_Start_Stop_UsingDeviceGroupId()
        {
            // Note: This only uses the initially chosen device.

            int deviceGroupId;
            QLError error = QuickLink2API.QLDeviceGroup_Create(out deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.Greater(deviceGroupId, 0);

            error = QuickLink2API.QLDeviceGroup_AddDevice(deviceGroupId, Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_Start(deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            QLDeviceStatus status;
            error = QuickLink2API.QLDevice_GetStatus(Test_SetUp.Helper.DeviceId, out status);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(QLDeviceStatus.QL_DEVICE_STATUS_STARTED, status);

            error = QuickLink2API.QLDevice_Stop(deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_GetStatus(Test_SetUp.Helper.DeviceId, out status);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(QLDeviceStatus.QL_DEVICE_STATUS_STOPPED, status);
        }

        [Test]
        public void Test_0460_QLDevice_GetFrame_UsingDeviceGroupId()
        {
            // Note: This only uses the initially chosen device.

            // Note: This could be much more robust (i.e., by checking the data
            // in the received frame for correctness).

            int deviceGroupId;
            QLError error = QuickLink2API.QLDeviceGroup_Create(out deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.Greater(deviceGroupId, 0);

            error = QuickLink2API.QLDeviceGroup_AddDevice(deviceGroupId, Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_Start(deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            QLFrameData frameData = new QLFrameData();
            error = QuickLink2API.QLDevice_GetFrame(deviceGroupId, 2000, ref frameData);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(Test_SetUp.Helper.DeviceId, frameData.DeviceId);

            error = QuickLink2API.QLDevice_Stop(deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0470_QLDeviceGroup_GetFrame()
        {
            // Note: This only uses the initially chosen device.

            // Note: This could be much more robust (i.e., by checking the data
            // in the received frame for correctness).

            int deviceGroupId;
            QLError error = QuickLink2API.QLDeviceGroup_Create(out deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.Greater(deviceGroupId, 0);

            error = QuickLink2API.QLDeviceGroup_AddDevice(deviceGroupId, Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_Start(deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            int numFrames = 1;
            QLFrameData[] frameDataArray = new QLFrameData[numFrames];
            frameDataArray[0] = new QLFrameData();
            error = QuickLink2API.QLDeviceGroup_GetFrame(deviceGroupId, 2000, ref numFrames, frameDataArray);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(1, numFrames);

            Assert.AreEqual(Test_SetUp.Helper.DeviceId, frameDataArray[0].DeviceId);

            error = QuickLink2API.QLDevice_Stop(deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0480_QLDevice_SetIndicator_UsingDeviceGroupId()
        {
            // Note: This only uses the initially chosen device.

            int deviceGroupId;
            QLError error = QuickLink2API.QLDeviceGroup_Create(out deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.Greater(deviceGroupId, 0);

            error = QuickLink2API.QLDeviceGroup_AddDevice(deviceGroupId, Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_Start(deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // The initial IndicatorModes for the right and left indicators
            QLIndicatorMode initialLeftMode;
            QLIndicatorMode initialRightMode;

            // Get the initial modes
            error = QuickLink2API.QLDevice_GetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_LEFT, out initialLeftMode);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_GetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_RIGHT, out initialRightMode);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Set the indicators to an arbitrary mode
            error = QuickLink2API.QLDevice_SetIndicator(deviceGroupId, QLIndicatorType.QL_INDICATOR_TYPE_LEFT,
                QLIndicatorMode.QL_INDICATOR_MODE_RIGHT_EYE_STATUS);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_SetIndicator(deviceGroupId, QLIndicatorType.QL_INDICATOR_TYPE_RIGHT,
                QLIndicatorMode.QL_INDICATOR_MODE_LEFT_EYE_STATUS);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            QLIndicatorMode leftMode = QLIndicatorMode.QL_INDICATOR_MODE_OFF;
            QLIndicatorMode rightMode = QLIndicatorMode.QL_INDICATOR_MODE_OFF;

            // Get the indicators modes and check that they were set correctly
            error = QuickLink2API.QLDevice_GetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_LEFT, out leftMode);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_GetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_RIGHT, out rightMode);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(QLIndicatorMode.QL_INDICATOR_MODE_RIGHT_EYE_STATUS, leftMode);
            Assert.AreEqual(QLIndicatorMode.QL_INDICATOR_MODE_LEFT_EYE_STATUS, rightMode);

            // Set the indicators
            error = QuickLink2API.QLDevice_SetIndicator(deviceGroupId, QLIndicatorType.QL_INDICATOR_TYPE_LEFT,
                QLIndicatorMode.QL_INDICATOR_MODE_LEFT_EYE_STATUS_FILTERED);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_SetIndicator(deviceGroupId, QLIndicatorType.QL_INDICATOR_TYPE_RIGHT,
                QLIndicatorMode.QL_INDICATOR_MODE_RIGHT_EYE_STATUS_FILTERED);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Get the indicators modes and check that they were set correctly
            error = QuickLink2API.QLDevice_GetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_LEFT, out leftMode);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_GetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_RIGHT, out rightMode);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(QLIndicatorMode.QL_INDICATOR_MODE_LEFT_EYE_STATUS_FILTERED, leftMode);
            Assert.AreEqual(QLIndicatorMode.QL_INDICATOR_MODE_RIGHT_EYE_STATUS_FILTERED, rightMode);

            // Set the indicators
            error = QuickLink2API.QLDevice_SetIndicator(deviceGroupId, QLIndicatorType.QL_INDICATOR_TYPE_LEFT,
                QLIndicatorMode.QL_INDICATOR_MODE_ON);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_SetIndicator(deviceGroupId, QLIndicatorType.QL_INDICATOR_TYPE_RIGHT,
                QLIndicatorMode.QL_INDICATOR_MODE_OFF);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Get the indicator modes and check that they were set correctly
            error = QuickLink2API.QLDevice_GetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_LEFT, out leftMode);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_GetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_RIGHT, out rightMode);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(QLIndicatorMode.QL_INDICATOR_MODE_ON, leftMode);
            Assert.AreEqual(QLIndicatorMode.QL_INDICATOR_MODE_OFF, rightMode);

            // Restore device to initial indicator modes
            error = QuickLink2API.QLDevice_SetIndicator(deviceGroupId, QLIndicatorType.QL_INDICATOR_TYPE_LEFT, initialLeftMode);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_SetIndicator(deviceGroupId, QLIndicatorType.QL_INDICATOR_TYPE_RIGHT, initialRightMode);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_Stop(deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0490_QLDevice_ImportSettings_UsingDeviceGroupId()
        {
            // Note: This only uses the initially chosen device.

            string setting = QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYES_TO_FIND;

            int deviceGroupId;
            QLError error = QuickLink2API.QLDeviceGroup_Create(out deviceGroupId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.Greater(deviceGroupId, 0);

            error = QuickLink2API.QLDeviceGroup_AddDevice(deviceGroupId, Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            int outSettingsID;
            error = QuickLink2API.QLSettings_Create(0, out outSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLSettings_AddSetting(outSettingsID, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.Helper.DeviceId, outSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            int outSettingValue;
            error = QuickLink2API.QLSettings_GetValueInt(outSettingsID, setting, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            int originalSettingValue = outSettingValue;

            int inSettingsID;
            error = QuickLink2API.QLSettings_Create(0, out inSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            QLDeviceEyesToUse inSettingValue = ((QLDeviceEyesToUse)outSettingValue != QLDeviceEyesToUse.QL_DEVICE_EYES_TO_USE_LEFT) ? QLDeviceEyesToUse.QL_DEVICE_EYES_TO_USE_LEFT : QLDeviceEyesToUse.QL_DEVICE_EYES_TO_USE_RIGHT;

            error = QuickLink2API.QLSettings_SetValueInt(inSettingsID, setting, (int)inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_ImportSettings(deviceGroupId, inSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.Helper.DeviceId, outSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLSettings_GetValueInt(outSettingsID, setting, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, (QLDeviceEyesToUse)outSettingValue);

            error = QuickLink2API.QLSettings_SetValueInt(inSettingsID, setting, originalSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_ImportSettings(deviceGroupId, inSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }
    }
}