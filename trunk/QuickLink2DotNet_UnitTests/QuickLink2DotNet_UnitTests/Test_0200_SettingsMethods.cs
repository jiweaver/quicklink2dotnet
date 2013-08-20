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
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using QuickLink2DotNet;
using QuickLink2DotNetHelper;

namespace QuickLink2DotNet_UnitTests
{
    [TestFixture]
    public class Test_0200_SettingsMethods
    {
        #region Basic Settings Tests

        [Test]
        public void Test_0210_QLSettings_Create()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0220_QLSettings_AddSetting()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            error = QuickLink2API.QLSettings_AddSetting(settingsId, settingName);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0230_QLSettings_RemoveSetting()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            error = QuickLink2API.QLSettings_AddSetting(settingsId, settingName);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLSettings_RemoveSetting(settingsId, settingName);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLSettings_RemoveSetting(settingsId, settingName);
            Assert.AreEqual(QLError.QL_ERROR_NOT_FOUND, error);
        }

        [Test]
        public void Test_0240_QLDevice_ImportSettings_ExportSettings()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYES_TO_FIND;

            int outSettingsID;
            QLError error = QuickLink2API.QLSettings_Create(0, out outSettingsID);
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

            error = QuickLink2API.QLDevice_ImportSettings(Test_SetUp.Helper.DeviceId, inSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.Helper.DeviceId, outSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLSettings_GetValueInt(outSettingsID, setting, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, (QLDeviceEyesToUse)outSettingValue);

            error = QuickLink2API.QLSettings_SetValueInt(inSettingsID, setting, originalSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLDevice_ImportSettings(Test_SetUp.Helper.DeviceId, inSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0250_QLSettings_Save()
        {
            string setting = "TestSettingName";
            int outSettingValue = int.MaxValue;

            int outSettingsID;
            QLError error = QuickLink2API.QLSettings_Create(0, out outSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLSettings_SetValueInt(outSettingsID, setting, outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string filename = System.IO.Path.Combine(QLHelper.ConfigDirectory, "Test_0030_QLSettings_Load_Save.txt");

            if (System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(filename);
            }

            error = QuickLink2API.QLSettings_Save(filename, outSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.True(System.IO.File.Exists(filename));

            int inSettingsID;
            error = QuickLink2API.QLSettings_Create(0, out inSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            error = QuickLink2API.QLSettings_Load(filename, ref inSettingsID);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            int inSettingValue;
            error = QuickLink2API.QLSettings_GetValueInt(inSettingsID, setting, out inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(outSettingValue, inSettingValue);

            System.IO.File.Delete(filename);
        }

        #endregion Basic Settings Tests

        #region Generic Get/Set Tests

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_String()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            string inSettingValue = "TestSettingValue";

            IntPtr inPtr = Marshal.StringToHGlobalAnsi(inSettingValue);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_STRING, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            int outBufferSize;
            error = QuickLink2API.QLSettings_GetValueStringSize(settingsId, settingName, out outBufferSize);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_STRING, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            string outSettingValue = Marshal.PtrToStringAnsi(outPtr);
            Marshal.FreeHGlobal(outPtr);

            Assert.True(outSettingValue.Equals(inSettingValue));
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_Int()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            int inSettingValue = int.MaxValue;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_INT, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            int outSettingValue = 0;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_INT, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (int)Marshal.PtrToStructure(outPtr, typeof(int));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_Int8()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            sbyte inSettingValue = sbyte.MaxValue;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_INT8, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            sbyte outSettingValue = 0;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_INT8, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (sbyte)Marshal.PtrToStructure(outPtr, typeof(sbyte));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_Int16()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            Int16 inSettingValue = Int16.MaxValue;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_INT16, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            Int16 outSettingValue = 0;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_INT16, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (Int16)Marshal.PtrToStructure(outPtr, typeof(Int16));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_Int32()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            Int32 inSettingValue = Int32.MaxValue;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_INT32, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            Int32 outSettingValue = 0;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_INT32, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (Int32)Marshal.PtrToStructure(outPtr, typeof(Int32));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_Int64()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            Int64 inSettingValue = Int64.MaxValue;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_INT64, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            Int64 outSettingValue = 0;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_INT64, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (Int64)Marshal.PtrToStructure(outPtr, typeof(Int64));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_UInt()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            uint inSettingValue = uint.MaxValue;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_UINT, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            uint outSettingValue = 0;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_UINT, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (uint)Marshal.PtrToStructure(outPtr, typeof(uint));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_UInt8()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            byte inSettingValue = byte.MaxValue;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_UINT8, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            byte outSettingValue = 0;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_UINT8, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (byte)Marshal.PtrToStructure(outPtr, typeof(byte));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_UInt16()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            UInt16 inSettingValue = UInt16.MaxValue;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_UINT16, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            UInt16 outSettingValue = 0;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_UINT16, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (UInt16)Marshal.PtrToStructure(outPtr, typeof(UInt16));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_UInt32()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            UInt32 inSettingValue = UInt32.MaxValue;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_UINT32, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            UInt32 outSettingValue = 0;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_UINT32, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (UInt32)Marshal.PtrToStructure(outPtr, typeof(UInt32));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_UInt64()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            UInt64 inSettingValue = UInt64.MaxValue;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_UINT64, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            UInt64 outSettingValue = 0;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_UINT64, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (UInt64)Marshal.PtrToStructure(outPtr, typeof(UInt64));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_Float()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            Single inSettingValue = Single.MaxValue;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_FLOAT, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            Single outSettingValue = 0;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_FLOAT, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (Single)Marshal.PtrToStructure(outPtr, typeof(Single));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_Double()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            double inSettingValue = double.MaxValue;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_DOUBLE, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            double outSettingValue = 0;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_DOUBLE, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (double)Marshal.PtrToStructure(outPtr, typeof(double));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_Bool()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            bool inSettingValue = true;
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_BOOL, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            bool outSettingValue = false;
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_BOOL, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (bool)Marshal.PtrToStructure(outPtr, typeof(bool));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0260_QLSettings_GetValue_SetValue_Void_Pointer()
        {
            // Create a new settings container.
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";

            IntPtr inSettingValue = new IntPtr(0x00001010);
            int inBufferSize = Marshal.SizeOf(inSettingValue);
            IntPtr inPtr = Marshal.AllocHGlobal(inBufferSize);
            Marshal.StructureToPtr(inSettingValue, inPtr, true);
            error = QuickLink2API.QLSettings_SetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_VOID_POINTER, inPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Marshal.FreeHGlobal(inPtr);

            IntPtr outSettingValue = new IntPtr(0);
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValue(settingsId, settingName, QLSettingType.QL_SETTING_TYPE_VOID_POINTER, outBufferSize, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (IntPtr)Marshal.PtrToStructure(outPtr, typeof(IntPtr));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        #endregion Generic Get/Set Tests

        #region Specific Get/Set Tests

        [Test]
        public void Test_0270_QLSettings_SetValueString()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            string inSettingValue = "TestSettingValue";

            error = QuickLink2API.QLSettings_SetValueString(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            int outBufferSize;
            error = QuickLink2API.QLSettings_GetValueStringSize(settingsId, settingName, out outBufferSize);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            StringBuilder sb = new StringBuilder(outBufferSize);
            error = QuickLink2API.QLSettings_GetValueString(settingsId, settingName, outBufferSize, sb);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string outSettingValue = sb.ToString();
            Assert.True(outSettingValue.Equals(inSettingValue));
        }

        [Test]
        public void Test_0270_QLSettings_SetValueInt_GetValueInt()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            int inSettingValue = int.MaxValue;

            error = QuickLink2API.QLSettings_SetValueInt(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            int outSettingValue;
            error = QuickLink2API.QLSettings_GetValueInt(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueInt8_GetValueInt8()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            sbyte inSettingValue = sbyte.MaxValue;

            error = QuickLink2API.QLSettings_SetValueInt8(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            sbyte outSettingValue;
            error = QuickLink2API.QLSettings_GetValueInt8(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueInt16_GetValueInt16()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            Int16 inSettingValue = Int16.MaxValue;

            error = QuickLink2API.QLSettings_SetValueInt16(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Int16 outSettingValue;
            error = QuickLink2API.QLSettings_GetValueInt16(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueInt32_GetValueInt32()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            Int32 inSettingValue = Int32.MaxValue;

            error = QuickLink2API.QLSettings_SetValueInt32(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Int32 outSettingValue;
            error = QuickLink2API.QLSettings_GetValueInt32(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueInt64_GetValueInt64()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            Int64 inSettingValue = Int64.MaxValue;

            error = QuickLink2API.QLSettings_SetValueInt64(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Int64 outSettingValue;
            error = QuickLink2API.QLSettings_GetValueInt64(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueUInt_GetValueUInt()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            uint inSettingValue = uint.MaxValue;

            error = QuickLink2API.QLSettings_SetValueUInt(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            uint outSettingValue;
            error = QuickLink2API.QLSettings_GetValueUInt(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueUInt8_GetValueUInt8()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            byte inSettingValue = byte.MaxValue;

            error = QuickLink2API.QLSettings_SetValueUInt8(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            byte outSettingValue;
            error = QuickLink2API.QLSettings_GetValueUInt8(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueUInt16_GetValueUInt16()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            UInt16 inSettingValue = UInt16.MaxValue;

            error = QuickLink2API.QLSettings_SetValueUInt16(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            UInt16 outSettingValue;
            error = QuickLink2API.QLSettings_GetValueUInt16(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueUInt32_GetValueUInt32()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            UInt32 inSettingValue = UInt32.MaxValue;

            error = QuickLink2API.QLSettings_SetValueUInt32(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            UInt32 outSettingValue;
            error = QuickLink2API.QLSettings_GetValueUInt32(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueUInt64_GetValueUInt64()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            UInt64 inSettingValue = UInt64.MaxValue;

            error = QuickLink2API.QLSettings_SetValueUInt64(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            UInt64 outSettingValue;
            error = QuickLink2API.QLSettings_GetValueUInt64(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueFloat_GetValueFloat()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            Single inSettingValue = Single.MaxValue;

            error = QuickLink2API.QLSettings_SetValueFloat(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Single outSettingValue;
            error = QuickLink2API.QLSettings_GetValueFloat(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueDouble_GetValueDouble()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            double inSettingValue = double.MaxValue;

            error = QuickLink2API.QLSettings_SetValueDouble(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            double outSettingValue;
            error = QuickLink2API.QLSettings_GetValueDouble(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueBool_GetValueBool()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            bool inSettingValue = true;

            error = QuickLink2API.QLSettings_SetValueBool(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            bool outSettingValue;
            error = QuickLink2API.QLSettings_GetValueBool(settingsId, settingName, out outSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        [Test]
        public void Test_0270_QLSettings_SetValueVoidPointer_GetValueVoidPointer()
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            string settingName = "TestSettingName";
            IntPtr inSettingValue = new IntPtr(0x00001100);

            error = QuickLink2API.QLSettings_SetValueVoidPointer(settingsId, settingName, inSettingValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            IntPtr outSettingValue = new IntPtr(0);
            int outBufferSize = Marshal.SizeOf(outSettingValue);
            IntPtr outPtr = Marshal.AllocHGlobal(outBufferSize);
            error = QuickLink2API.QLSettings_GetValueVoidPointer(settingsId, settingName, outPtr);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            outSettingValue = (IntPtr)Marshal.PtrToStructure(outPtr, typeof(IntPtr));
            Marshal.FreeHGlobal(outPtr);

            Assert.AreEqual(inSettingValue, outSettingValue);
        }

        #endregion Specific Get/Set Tests

        #region QL_SETTINGS Tests

        private bool SettingIsSupported(string setting)
        {
            // Run the test only if the device supports this particular setting
            QLError error = QuickLink2API.QLDevice_IsSettingSupported(Test_SetUp.Helper.DeviceId, setting);
            Assert.True(error == QLError.QL_ERROR_OK || error == QLError.QL_ERROR_NOT_SUPPORTED);
            if (error == QLError.QL_ERROR_NOT_SUPPORTED)
            {
                Console.WriteLine("Ignoring test for {0} on device {1} because the device does not support it.", setting, Test_SetUp.Helper.DeviceId);
                return false;
            }
            return true;
        }

        private void QL_SETTINGS_Test_Double(string setting, double inValue)
        {
            // The settings container that will hold the initial settings of the device
            int initialSettings;

            // The settings container used to import new settings to the device
            int importedSettings;

            // The settings container used to export settings from the device.
            int exportedSettings;

            // Clear the settings containers
            QLError error = QuickLink2API.QLSettings_Create(0, out initialSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_Create(0, out importedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_Create(0, out exportedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Add the setting being tested to each settings container
            error = QuickLink2API.QLSettings_AddSetting(initialSettings, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_AddSetting(importedSettings, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_AddSetting(exportedSettings, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Export the initial settings from the device
            error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.Helper.DeviceId, initialSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Set the value of the setting
            error = QuickLink2API.QLSettings_SetValueDouble(importedSettings, setting, inValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Import the new setting value to the device
            error = QuickLink2API.QLDevice_ImportSettings(Test_SetUp.Helper.DeviceId, importedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Export the new setting value from the device
            error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.Helper.DeviceId, exportedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Get the setting to return to the test to check that it was correctly set
            double outValue;
            error = QuickLink2API.QLSettings_GetValueDouble(exportedSettings, setting, out outValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Restore the device to its initial settings
            error = QuickLink2API.QLDevice_ImportSettings(Test_SetUp.Helper.DeviceId, initialSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        // Bounds checking for all of the double settings
        private void QL_SETTINGS_Test_Double(string setting, double min, double max, double increment)
        {
            // The settings container that will hold the initial settings of the device
            int initialSettings;

            // The settings container used to import new settings to the device
            int importedSettings;

            // The settings container used to export settings from the device.
            int exportedSettings;

            // Clear the settings containers
            QLError error = QuickLink2API.QLSettings_Create(0, out initialSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_Create(0, out importedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_Create(0, out exportedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Add the setting being tested to each settings container
            error = QuickLink2API.QLSettings_AddSetting(initialSettings, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_AddSetting(importedSettings, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_AddSetting(exportedSettings, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Export the initial settings from the device
            error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.Helper.DeviceId, initialSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            double inValue;
            double outValue;

            // Import values for each increment within the bounds.
            // The device should accept all settings within the bounds
            for (inValue = min; inValue <= max; inValue += increment)
            {
                error = QuickLink2API.QLSettings_SetValueDouble(importedSettings, setting, inValue);
                Assert.AreEqual(QLError.QL_ERROR_OK, error);
                error = QuickLink2API.QLDevice_ImportSettings(Test_SetUp.Helper.DeviceId, importedSettings);
                Assert.AreEqual(QLError.QL_ERROR_OK, error);
                error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.Helper.DeviceId, exportedSettings);
                Assert.AreEqual(QLError.QL_ERROR_OK, error);
                error = QuickLink2API.QLSettings_GetValueDouble(importedSettings, setting, out outValue);
                Assert.AreEqual(QLError.QL_ERROR_OK, error);
                Assert.AreEqual(inValue, outValue);
            }

            /*
            // Check that the correct error is returned for values outside of the bounds.
            double tooLow = min - increment;
            error = QuickLink2API.QLSettings_SetValueDouble(importedSettings, setting, tooLow);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_ImportSettings(Test_SetUp.DeviceID, importedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            //Assert.AreNotEqual(QLError.QL_ERROR_OK, error, "The device should not accept a value for this setting above {0}", max);
            error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.DeviceID, exportedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_GetValueDouble(exportedSettings, setting, out outValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreNotEqual(tooLow, outValue, "The device should not accept a value for this setting below {0}", min);

            double tooHigh = max + increment;
            error = QuickLink2API.QLSettings_SetValueDouble(importedSettings, setting, tooHigh);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLDevice_ImportSettings(Test_SetUp.DeviceID, importedSettings);
            //Assert.AreNotEqual(QLError.QL_ERROR_OK, error, "The device should not accept a value for this setting above {0}", max);
            error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.DeviceID, exportedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_GetValueDouble(exportedSettings, setting, out outValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            //Assert.AreNotEqual(tooHigh, outValue, "The device should not accept a value for this setting above {0}", max);
            */

            // Restore the device to its initial settings
            error = QuickLink2API.QLDevice_ImportSettings(Test_SetUp.Helper.DeviceId, initialSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        private void QL_SETTINGS_Test_Int(string setting, int inValue)
        {
            // The settings container that will hold the initial settings of the device
            int initialSettings;

            // The settings container used to import new settings to the device
            int importedSettings;

            // The settings container used to export settings from the device.
            int exportedSettings;

            // Clear the settings containers
            QLError error = QuickLink2API.QLSettings_Create(0, out initialSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_Create(0, out importedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_Create(0, out exportedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Add the setting being tested to each settings container
            error = QuickLink2API.QLSettings_AddSetting(initialSettings, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_AddSetting(importedSettings, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_AddSetting(exportedSettings, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Export the initial settings from the device
            error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.Helper.DeviceId, initialSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Set the value of the setting
            error = QuickLink2API.QLSettings_SetValueInt(importedSettings, setting, inValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Import the new setting value to the device
            error = QuickLink2API.QLDevice_ImportSettings(Test_SetUp.Helper.DeviceId, importedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Export the new setting value from the device
            error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.Helper.DeviceId, exportedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Get the setting to return to the test to check that it was correctly set
            int outValue;
            error = QuickLink2API.QLSettings_GetValueInt(exportedSettings, setting, out outValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Restore the device to its initial settings
            error = QuickLink2API.QLDevice_ImportSettings(Test_SetUp.Helper.DeviceId, initialSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        private void QL_SETTINGS_Test_Bool(string setting, bool value)
        {
            // The settings container that will hold the initial settings of the device
            int initialSettings;

            // The settings container used to import new settings to the device
            int importedSettings;

            // The settings container used to export settings from the device.
            int exportedSettings;

            // Clear the settings containers
            QLError error = QuickLink2API.QLSettings_Create(0, out initialSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_Create(0, out importedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_Create(0, out exportedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Add the setting being tested to each settings container
            error = QuickLink2API.QLSettings_AddSetting(initialSettings, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_AddSetting(importedSettings, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            error = QuickLink2API.QLSettings_AddSetting(exportedSettings, setting);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Export the initial settings from the device
            error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.Helper.DeviceId, initialSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Set the value of the setting
            error = QuickLink2API.QLSettings_SetValueBool(importedSettings, setting, value);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Import the new setting value to the device
            error = QuickLink2API.QLDevice_ImportSettings(Test_SetUp.Helper.DeviceId, importedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            // Export the new setting value from the device
            error = QuickLink2API.QLDevice_ExportSettings(Test_SetUp.Helper.DeviceId, exportedSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);

            bool outValue;

            // Get the setting and check that the setting was correctly set
            error = QuickLink2API.QLSettings_GetValueBool(exportedSettings, setting, out outValue);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(value, outValue);

            // Restore the device to its initial settings
            error = QuickLink2API.QLDevice_ImportSettings(Test_SetUp.Helper.DeviceId, initialSettings);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_BANDWIDTH_MODE()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_MODE;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceBandwidthMode.QL_DEVICE_BANDWIDTH_MODE_AUTO);
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceBandwidthMode.QL_DEVICE_BANDWIDTH_MODE_MANUAL);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_BANDWIDTH_PERCENT_FULL()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_PERCENT_FULL;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, 1.0, 100.0, 5.0);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_BANDWIDTH_PERCENT_TRACKING()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_PERCENT_TRACKING;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, 1.0, 100.0, 5.0);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_BINNING()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_BINNING;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Int(setting, 1);
                QL_SETTINGS_Test_Int(setting, 2);
                QL_SETTINGS_Test_Int(setting, 4);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_CALIBRATE_ENABLED()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_CALIBRATE_ENABLED;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Bool(setting, true);
                QL_SETTINGS_Test_Bool(setting, false);
                QL_SETTINGS_Test_Bool(setting, true);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_DISTANCE()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_DISTANCE;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, 50.0);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_EXPOSURE()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_EXPOSURE;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, 1.0, 50.0, 1.5);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_FLIP_X()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_FLIP_X;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Bool(setting, true);
                QL_SETTINGS_Test_Bool(setting, false);
                QL_SETTINGS_Test_Bool(setting, true);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_FLIP_Y()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_FLIP_Y;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Bool(setting, true);
                QL_SETTINGS_Test_Bool(setting, false);
                QL_SETTINGS_Test_Bool(setting, true);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_GAIN_MODE()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_GAIN_MODE;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceGainMode.QL_DEVICE_GAIN_MODE_AUTO);
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceGainMode.QL_DEVICE_GAIN_MODE_MANUAL);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_GAIN_VALUE()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_GAIN_VALUE;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, 50.0);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_GAZE_POINT_FILTER_MODE()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_MODE;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceGazePointFilterMode.QL_DEVICE_GAZE_POINT_FILTER_HEURISTIC_FRAMES);
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceGazePointFilterMode.QL_DEVICE_GAZE_POINT_FILTER_HEURISTIC_TIME);
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceGazePointFilterMode.QL_DEVICE_GAZE_POINT_FILTER_MEDIAN_FRAMES);
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceGazePointFilterMode.QL_DEVICE_GAZE_POINT_FILTER_MEDIAN_TIME);
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceGazePointFilterMode.QL_DEVICE_GAZE_POINT_FILTER_NONE);
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceGazePointFilterMode.QL_DEVICE_GAZE_POINT_FILTER_WEIGHTED_PREVIOUS_FRAME);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Int(setting, 16);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_IMAGE_PROCESSING_ENABLED()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_ENABLED;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Bool(setting, true);
                QL_SETTINGS_Test_Bool(setting, false);
                QL_SETTINGS_Test_Bool(setting, true);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_IMAGE_PROCESSING_EYES_TO_FIND()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYES_TO_FIND;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceEyesToUse.QL_DEVICE_EYES_TO_USE_LEFT);
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceEyesToUse.QL_DEVICE_EYES_TO_USE_LEFT_AND_RIGHT);
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceEyesToUse.QL_DEVICE_EYES_TO_USE_LEFT_OR_RIGHT);
                QL_SETTINGS_Test_Int(setting, (int)QLDeviceEyesToUse.QL_DEVICE_EYES_TO_USE_RIGHT);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, 0.4);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_RIGHT()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_RIGHT;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, 0.6);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_INTERPOLATE_ENABLED()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_INTERPOLATE_ENABLED;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Bool(setting, true);
                QL_SETTINGS_Test_Bool(setting, false);
                QL_SETTINGS_Test_Bool(setting, true);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_IR_LIGHT_MODE()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_IR_LIGHT_MODE;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, (int)QLDeviceIRLightMode.QL_DEVICE_IR_LIGHT_MODE_AUTO);
                QL_SETTINGS_Test_Double(setting, (int)QLDeviceIRLightMode.QL_DEVICE_IR_LIGHT_MODE_OFF);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_LENS_FOCAL_LENGTH()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_LENS_FOCAL_LENGTH;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, 2.0);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_X()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_X;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, 1.0, 50.0, 2.5);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_Y()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_Y;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, 1.0, 50.0, 2.5);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_ROI_SIZE_PERCENT_X()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_ROI_SIZE_PERCENT_X;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, 1.0, 100.0, 5.0);
            }
        }

        [Test]
        public void Test_0280_QL_SETTING_DEVICE_ROI_SIZE_PERCENT_Y()
        {
            string setting = QL_SETTINGS.QL_SETTING_DEVICE_ROI_SIZE_PERCENT_Y;
            if (SettingIsSupported(setting))
            {
                QL_SETTINGS_Test_Double(setting, 1.0, 100.0, 5.0);
            }
        }

        #endregion QL_SETTINGS Tests
    }
}