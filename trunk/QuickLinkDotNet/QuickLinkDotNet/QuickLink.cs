#region License

/* QuickLinkDotNet : A .NET wrapper (in C#) for EyeTech's QuickLink API.
 *
 * Copyright (c) 2010 Justin Weaver
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

#endregion License

#region Header Comments

/* $Id$
 *
 * Description: This class will find and load the QuickLink DLL's into the
 * program address space, and provide access to their API operations.
 */

#endregion Header Comments

#region 64Bit Support

#if ISX64
using cpp_long = System.Int64;

#else
using cpp_long = System.Int32;
using cpp_ulong = System.UInt32;

#endif

#endregion 64Bit Support

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace QuickLinkDotNet
{
    public class QuickLink : IDisposable
    {
        #region Fields

        /* These handles are the return values from calls to LoadLibrary.  We
         * use them to free the libraries when we dispose.
         */
        private IntPtr dfHandle, qlHandle;

        #endregion Fields

        #region Constructor

        public QuickLink()
        {
            FindAndLoadQuickLinkDLLs();
        }

        ~QuickLink()
        {
            Dispose(false);
        }

        private void FindAndLoadQuickLinkDLLs()
        {
            RegistryKey reg = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);

            RegistryKey qlRegKeyLoc = null;
            foreach (string s in QuickConstants.QuickLinkRegistryKeyLocations)
            {
                // Try each possible key location.
                qlRegKeyLoc = reg.OpenSubKey(s);
                if (qlRegKeyLoc != null)
                    // Found the key.
                    break;

                if (qlRegKeyLoc == null)
                    // Key not found.
                    continue;
            }

            if (qlRegKeyLoc == null)
            {
                reg = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);

                qlRegKeyLoc = null;
                foreach (string s in QuickConstants.QuickLinkRegistryKeyLocations)
                {
                    // Try each possible key location.
                    qlRegKeyLoc = reg.OpenSubKey(s);
                    if (qlRegKeyLoc != null)
                        // Found the key.
                        break;

                    if (qlRegKeyLoc == null)
                        // Key not found.
                        continue;
                }
            }

            if (qlRegKeyLoc == null)
                // Key not found!
                throw new Exception("The QuickLinkDLL registry key could not be found.");

            // Path to the QuickLink DLL.
            string qlPath = qlRegKeyLoc.GetValue("Path").ToString();
            if (qlPath == null)
                // Value not found!
                throw new Exception("The QuickLinkDLL path could not be retrieved from the registry.");

            // The path to the Dragonfly capture DLL.
            string dfPath = Path.Combine(Path.GetDirectoryName(qlPath), QuickConstants.PGRFlyCaptureDLLName);

            /* Now we load the DLLs into our address space.  This allows us to
             * use DLLImport without knowing the full path to the DLLs until
             * load time.
             */

            this.dfHandle = Win32LibraryControl.Load(dfPath);
            if ((cpp_long)this.dfHandle == 0)
                // Unable to load the library!
                throw new Win32Exception(Marshal.GetLastWin32Error());

            this.qlHandle = Win32LibraryControl.Load(qlPath);
            if ((cpp_long)this.qlHandle == 0)
                // Unable to load the library!
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        private void FreeQuickLinkDLLs()
        {
            Win32LibraryControl.Free(this.qlHandle);
            Win32LibraryControl.Free(this.dfHandle);
        }

        /* If you create an instance of the QuickLink class, the constructor will
         * look through the system registry, and find and load the QuickLink DLLs
         * from inside QuickGlance's installation folder.  The alternative is to
         * place copies of QuickGlance.DLL and PGRFlycapture.dll into the directory
         * with your executable; which will allow you to forgo instantiating
         * this QuickLink class before calling any of the API methods.
         */

        #endregion Constructor

        #region QuickLink API Methods

        public bool GetQGOnFlag()
        {
            return QuickLinkDotNet.QuickLinkAPI.GetQGOnFlag();
        }

        public void ExitQuickGlance()
        {
            QuickLinkDotNet.QuickLinkAPI.ExitQuickGlance();
        }

        public void SetEyeControl(bool Enable)
        {
            QuickLinkDotNet.QuickLinkAPI.SetEyeControl(Enable);
        }

        public bool GetEyeControl()
        {
            return QuickLinkDotNet.QuickLinkAPI.GetEyeControl();
        }

        public void SetMWState(QGWindowState MWState)
        {
            QuickLinkDotNet.QuickLinkAPI.SetMWState(MWState);
        }

        public void ToggleLargeImage()
        {
            QuickLinkDotNet.QuickLinkAPI.ToggleLargeImage();
        }

        public void ShowLargeImage()
        {
            QuickLinkDotNet.QuickLinkAPI.ShowLargeImage();
        }

        public void HideLargeImage()
        {
            QuickLinkDotNet.QuickLinkAPI.HideLargeImage();
        }

        public void MoveLeftRight()
        {
            QuickLinkDotNet.QuickLinkAPI.MoveLeftRight();
        }

        public void MoveUpDown()
        {
            QuickLinkDotNet.QuickLinkAPI.MoveUpDown();
        }

        public double GetSDKVersion()
        {
            return QuickLinkDotNet.QuickLinkAPI.GetSDKVersion();
        }

        public void SetCopyImageFlag(bool CopyImage)
        {
            QuickLinkDotNet.QuickLinkAPI.SetCopyImageFlag(CopyImage);
        }

        public bool GetImageData(uint MaxTimeout, ref ImageData Data)
        {
            return QuickLinkDotNet.QuickLinkAPI.GetImageData(MaxTimeout, ref Data);
        }

        public bool GetImageDataAndLatency(uint MaxTimeout, ref ImageData Data, out double Latency)
        {
            return QuickLinkDotNet.QuickLinkAPI.GetImageDataAndLatency(MaxTimeout, ref Data, out Latency);
        }

        public void StartBulkCapture(uint NumPoints)
        {
            QuickLinkDotNet.QuickLinkAPI.StartBulkCapture(NumPoints);
        }

        public void StopBulkCapture()
        {
            QuickLinkDotNet.QuickLinkAPI.StopBulkCapture();
        }

        public bool QueryBulkCapture(out uint NumCaptured, out uint NumNotCaptured, out uint NumRead, out uint NumNotRead, out bool Capturing)
        {
            return QuickLinkDotNet.QuickLinkAPI.QueryBulkCapture(out NumCaptured, out NumNotCaptured, out NumRead, out NumNotRead, out Capturing);
        }

        public bool ReadBulkCapture(uint MaxTimeout, ref ImageData Data)
        {
            return QuickLinkDotNet.QuickLinkAPI.ReadBulkCapture(MaxTimeout, ref Data);
        }

        public void InternalCalibration1()
        {
            QuickLinkDotNet.QuickLinkAPI.InternalCalibration1();
        }

        public CalibrationErrorEx InitializeCalibrationEx(uint CalibrationIndex)
        {
            return QuickLinkDotNet.QuickLinkAPI.InitializeCalibrationEx(CalibrationIndex);
        }

        public CalibrationErrorEx GetNewTargetPositionEx(out int X, out int Y, out int TargetHandle)
        {
            return QuickLinkDotNet.QuickLinkAPI.GetNewTargetPositionEx(out X, out Y, out TargetHandle);
        }

        public CalibrationErrorEx GetWorstTargetPositionEx(out int X, out int Y, out int TargetHandle)
        {
            return QuickLinkDotNet.QuickLinkAPI.GetWorstTargetPositionEx(out X, out Y, out TargetHandle);
        }

        public CalibrationErrorEx CalibrateEx(int TargetHandle)
        {
            return QuickLinkDotNet.QuickLinkAPI.CalibrateEx(TargetHandle);
        }

        public CalibrationErrorEx GetScoreEx(out double left, out double right)
        {
            return QuickLinkDotNet.QuickLinkAPI.GetScoreEx(out left, out right);
        }

        public CalibrationErrorEx ApplyCalibrationEx()
        {
            return QuickLinkDotNet.QuickLinkAPI.ApplyCalibrationEx();
        }

        public void CalibrationBiasEx(int DeltaX, int DeltaY)
        {
            QuickLinkDotNet.QuickLinkAPI.CalibrationBiasEx(DeltaX, DeltaY);
        }

        public CalibrationErrorEx OpenCalibrationEx(uint CalibrationIndex)
        {
            return QuickLinkDotNet.QuickLinkAPI.OpenCalibrationEx(CalibrationIndex);
        }

        public bool GetClickingOptions(ref ClickingOptions Options)
        {
            return QuickLinkDotNet.QuickLinkAPI.GetClickingOptions(ref Options);
        }

        public bool GetCalibrationOptions(ref CalibrationOptions Options)
        {
            return QuickLinkDotNet.QuickLinkAPI.GetCalibrationOptions(ref Options);
        }

        public bool GetProcessingOptions(ref ProcessingOptions Options)
        {
            return QuickLinkDotNet.QuickLinkAPI.GetProcessingOptions(ref Options);
        }

        public bool GetCameraOptions(ref CameraOptions Options)
        {
            return QuickLinkDotNet.QuickLinkAPI.GetCameraOptions(ref Options);
        }

        public bool GetToolbarOptions(ref ToolbarOptions Options)
        {
            return QuickLinkDotNet.QuickLinkAPI.GetToolbarOptions(ref Options);
        }

        public bool SetClickingOptions(ClickingOptions Options)
        {
            return QuickLinkDotNet.QuickLinkAPI.SetClickingOptions(Options);
        }

        public bool SetCalibrationOptions(CalibrationOptions Options)
        {
            return QuickLinkDotNet.QuickLinkAPI.SetCalibrationOptions(Options);
        }

        public bool SetProcessingOptions(ProcessingOptions Options)
        {
            return QuickLinkDotNet.QuickLinkAPI.SetProcessingOptions(Options);
        }

        public bool SetCameraOptions(CameraOptions Options)
        {
            return QuickLinkDotNet.QuickLinkAPI.SetCameraOptions(Options);
        }

        public bool SetToolbarOptions(ToolbarOptions Options)
        {
            return QuickLinkDotNet.QuickLinkAPI.SetToolbarOptions(Options);
        }

        public void RegisterClickEvent(ref IntPtr WindowHandle, uint PrimaryMessage, uint SecondaryMessage)
        {
            QuickLinkDotNet.QuickLinkAPI.RegisterClickEvent(ref WindowHandle, PrimaryMessage, SecondaryMessage);
        }

        public bool GetSerialNumber(out cpp_long SerialNumber)
        {
            return QuickLinkDotNet.QuickLinkAPI.GetSerialNumber(out SerialNumber);
        }

        public bool SetCustomGPIO(CameraCustomGPIOOutputValue GPIO_1, CameraCustomGPIOOutputValue GPIO_2, CameraCustomGPIOOutputValue GPIO_3)
        {
            return QuickLinkDotNet.QuickLinkAPI.SetCustomGPIO(GPIO_1, GPIO_2, GPIO_3);
        }

        #endregion QuickLink API Methods

        #region IDisposable

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed == false)
            {
                FreeQuickLinkDLLs();
            }

            // Mark the unmanaged resources as freed.
            this.disposed = true;
        }

        #endregion IDisposable
    }
}