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
using NUnit.Framework;
using QuickLink2DotNet;
using QuickLink2DotNetHelper;

namespace QuickLink2DotNet_UnitTests
{
    class Test_0300_CalibrationMethods
    {
        [Test]
        public void Test_0300_QLCalibration_Create()
        {
            int calibrationId;
            QLError error = QuickLink2API.QLCalibration_Create(0, out calibrationId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0310_QLCalibration_Initialize()
        {
            int calibrationId;
            QLError error = QuickLink2API.QLCalibration_Create(0, out calibrationId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_Start(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLCalibration_Initialize(Test_SetUp.Helper.DeviceId, calibrationId, QLCalibrationType.QL_CALIBRATION_TYPE_16);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_Stop(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0320_QLCalibration_Cancel()
        {
            int calibrationId;
            QLError error = QuickLink2API.QLCalibration_Create(0, out calibrationId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_Start(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLCalibration_Initialize(Test_SetUp.Helper.DeviceId, calibrationId, QLCalibrationType.QL_CALIBRATION_TYPE_16);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLCalibration_Cancel(calibrationId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_Stop(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        // Currently, to perform this test, a calibration needs to already be saved
        [Test]
        public void Test_0330_QLCalibration_Save_Load()
        {
            int calibrationId;
            QLError error = QuickLink2API.QLCalibration_Create(0, out calibrationId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Try to load non-existent calibration data into the container.
            // Check that the correct error is returned.
            string fakeCalibrationFilename = System.IO.Path.Combine(QLHelper.ConfigDirectory, "qlcalibrationFake.qlc");
            error = QuickLink2API.QLCalibration_Load(fakeCalibrationFilename, ref calibrationId);
            Assert.AreNotEqual(QLError.QL_ERROR_OK, error);

            // Try to load a previously saved calibration file into the container.
            // Check that the correct error is returned.
            error = QuickLink2API.QLCalibration_Load(Test_SetUp.Helper.CalibrationFilename, ref calibrationId);
            Assert.AreNotEqual(QLError.QL_ERROR_INVALID_PATH, error, "Calibration file not found.  You may need to run the SetupDevice example first.");
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Save the calibration to a different path and check that the two files are the same
            string duplicateCalibrationFilename = System.IO.Path.Combine(QLHelper.ConfigDirectory, "qlcalibrationDuplicate.qlc");
            if (System.IO.File.Exists(duplicateCalibrationFilename))
            {
                System.IO.File.Delete(duplicateCalibrationFilename);
            }
            error = QuickLink2API.QLCalibration_Save(duplicateCalibrationFilename, calibrationId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            string calibration1 = System.IO.File.ReadAllText(Test_SetUp.Helper.CalibrationFilename);
            string calibration2 = System.IO.File.ReadAllText(duplicateCalibrationFilename);
            Assert.AreEqual(calibration1, calibration2);
            System.IO.File.Delete(duplicateCalibrationFilename);
        }

        // Currently, to perform this test, a calibration needs to already be saved
        [Test]
        public void Test_0340_QLCalibration_AddBias()
        {
            int calibrationId;
            QLError error = QuickLink2API.QLCalibration_Create(0, out calibrationId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLCalibration_Load(Test_SetUp.Helper.CalibrationFilename, ref calibrationId);
            Assert.AreNotEqual(QLError.QL_ERROR_INVALID_PATH, error, "Calibration file not found.  You may need to run the SetupDevice example first.");
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLCalibration_AddBias(calibrationId, QLEyeType.QL_EYE_TYPE_LEFT, .2f, .2f);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLCalibration_AddBias(calibrationId, QLEyeType.QL_EYE_TYPE_RIGHT, .2f, .2f);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0350_QLCalibration_GetTargets()
        {
            int calibrationId;
            QLError error = QuickLink2API.QLCalibration_Create(0, out calibrationId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_Start(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLCalibration_Initialize(Test_SetUp.Helper.DeviceId, calibrationId, QLCalibrationType.QL_CALIBRATION_TYPE_16);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            int numTargets = 16;
            QLCalibrationTarget[] targets = new QLCalibrationTarget[numTargets];
            error = QuickLink2API.QLCalibration_GetTargets(calibrationId, ref numTargets, targets);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(16, numTargets);

            error = QuickLink2API.QLCalibration_Cancel(calibrationId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_Stop(Test_SetUp.Helper.DeviceId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        // Currently, to perform this test, a calibration needs to already be saved
        [Test]
        public void Test_0360_QLDevice_ApplyCalibration()
        {
            int calibrationId;
            QLError error = QuickLink2API.QLCalibration_Create(0, out calibrationId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLCalibration_Load(Test_SetUp.Helper.CalibrationFilename, ref calibrationId);
            Assert.AreNotEqual(QLError.QL_ERROR_INVALID_PATH, error, "Calibration file not found.  You may need to run the SetupDevice example first.");
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_ApplyCalibration(Test_SetUp.Helper.DeviceId, calibrationId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        //TODO QuickLink2API.QLCalibration_Finalize;
        //TODO QuickLink2API.QLCalibration_GetScoring;
        //TODO QuickLink2API.QLCalibration_GetStatus;
    }
}