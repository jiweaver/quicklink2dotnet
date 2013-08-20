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
    class Test_0100_DeviceMethods
    {
        [Test]
        public void Test_0110_QLDevice_GetStatus()
        {
            QLDeviceStatus status;
            QLError error = QuickLink2API.QLDevice_GetStatus(Test_SetUp.Helper.DeviceId, out status);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0120_QLDevice_Start_Stop()
        {
            QLDeviceStatus status;
            QLError error = QuickLink2API.QLDevice_GetStatus(Test_SetUp.Helper.DeviceId, out status);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(QLDeviceStatus.QL_DEVICE_STATUS_STOPPED, status);

            error = QuickLink2API.QLDevice_Start(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_GetStatus(Test_SetUp.Helper.DeviceId, out status);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(QLDeviceStatus.QL_DEVICE_STATUS_STARTED, status);

            error = QuickLink2API.QLDevice_Stop(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_GetStatus(Test_SetUp.Helper.DeviceId, out status);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(QLDeviceStatus.QL_DEVICE_STATUS_STOPPED, status);
        }

        [Test]
        public void Test_0125_QLDevice_Stop_All()
        {
            // Note: tests only that the initially chosen device was stopped.

            QLDeviceStatus status;
            QLError error = QuickLink2API.QLDevice_GetStatus(Test_SetUp.Helper.DeviceId, out status);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(QLDeviceStatus.QL_DEVICE_STATUS_STOPPED, status);

            error = QuickLink2API.QLDevice_Start(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_GetStatus(Test_SetUp.Helper.DeviceId, out status);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(QLDeviceStatus.QL_DEVICE_STATUS_STARTED, status);

            error = QuickLink2API.QLDevice_Stop_All();
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_GetStatus(Test_SetUp.Helper.DeviceId, out status);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(QLDeviceStatus.QL_DEVICE_STATUS_STOPPED, status);
        }

        [Test]
        public void Test_0130_QLDevice_GetFrame()
        {
            // Note: This could be much more robust (i.e., by checking the data
            // in the received frame for correctness).

            QLError error = QuickLink2API.QLDevice_Start(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            QLFrameData frameData = new QLFrameData();
            error = QuickLink2API.QLDevice_GetFrame(Test_SetUp.Helper.DeviceId, 2000, ref frameData);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(Test_SetUp.Helper.DeviceId, frameData.DeviceId);

            error = QuickLink2API.QLDevice_Stop(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0140_QLDevice_GetIndicator_SetIndicator()
        {
            QLError error = QuickLink2API.QLDevice_Start(Test_SetUp.Helper.DeviceId);
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
            error = QuickLink2API.QLDevice_SetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_LEFT,
                QLIndicatorMode.QL_INDICATOR_MODE_RIGHT_EYE_STATUS);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_SetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_RIGHT,
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
            error = QuickLink2API.QLDevice_SetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_LEFT,
                QLIndicatorMode.QL_INDICATOR_MODE_LEFT_EYE_STATUS_FILTERED);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_SetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_RIGHT,
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
            error = QuickLink2API.QLDevice_SetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_LEFT,
                QLIndicatorMode.QL_INDICATOR_MODE_ON);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_SetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_RIGHT,
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
            error = QuickLink2API.QLDevice_SetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_LEFT, initialLeftMode);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_SetIndicator(Test_SetUp.Helper.DeviceId, QLIndicatorType.QL_INDICATOR_TYPE_RIGHT, initialRightMode);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_Stop(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        // Not sure if this is the best way to test this method
        // Only works if the user is looking at the screen so his
        // eye radius can be calibrated.
        [Test]
        [Category("DeviceInteractive")]
        public void Test_0150_QLDevice_CalibrateEyeRadius()
        {
            QLError error = QuickLink2API.QLDevice_Start(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            float leftRadius = 0;
            float rightRadius = 0;

            // Wait so the device is completely initialized and started before calibrating
            // the eye radius
            Thread.Sleep(2000);

            // Calibrate the left and right eye radii
            error = QuickLink2API.QLDevice_CalibrateEyeRadius(Test_SetUp.Helper.DeviceId, 50, out leftRadius, out rightRadius);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Check that both radii are greater than 0;
            Assert.Greater(leftRadius, 0, "Must be looking at screen");
            Assert.Greater(rightRadius, 0, "Must be looking at screen");

            error = QuickLink2API.QLDevice_Stop(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }
    }
}