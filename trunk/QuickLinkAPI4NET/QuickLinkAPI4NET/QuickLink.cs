/* QuickLinkAPI4NET : A .NET wrapper (in C#) for EyeTech's QuickLink API.
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

/* File: QuickLink.cs
 * Author: Justin Weaver (Jul 2010)
 * Revision: $Rev$
 * Description: A .NET wrapper for EyeTech's QuickLink API. The extensive
 * inline documentation (i.e. comments) has been cut & pasted from the 
 * QuickLinkAPI.h C++ header file for convenient reference.
 * 
 * ---------
 * Important: 
 * 
 * - Most QuickLink API functions require EyeTech's QuickGlance 
 * software to be running.
 * 
 * For the latest QuickGlance, browse to 
 * http://eyetechds.com/support/downloads/index.html
 * 
 * -------------------------------------------------------------------------
 * Internal Implementation Details (or "Win32 Stuff I Learned the Hard Way"):
 * 
 * You don't need to worry about this section if you are planning on using the
 * API in your project.  This is merely info about the internal workings of the
 * wrapper.
 * 
 * 1.   A long in C++ is 4-bytes, but long in C# is 8-bytes.  So, long maps to
 *      int.
 * 
 * 2.   A bool in C++ is 4-bytes, but bool in C# is 1-byte.  Additionally, when
 *      a bool is passed as a parameter, it is passed in 2-byte variant bool 
 *      format.  So, marshalling of bool must be manually specified.
 * 
 * ----------------------------------------------------------
 * This is from the original "QuickLinkAPI.h" C++ header file:
 * 
 *      // QuickLinkAPI.h
 *      // Copyright (C) EyeTech Digital Systems
 *      // Original Author: Caleb Hinton
 *      // All rights reserved.
 */

//#define SYSTEM_64BIT

#if SYSTEM_64BIT
using cpp_long = System.Int64;
using cpp_ulong = System.UInt64;
#else
using cpp_long = System.Int32;
using cpp_ulong = System.UInt32;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;
using System.ComponentModel;

namespace QuickLinkAPI4NET
{
    public enum CalibrationErrorEx
    {
        CALIBRATIONEX_OK = 0,
        CALIBRATIONEX_NO_EYE_SELECTED = 1,
        CALIBRATIONEX_NOT_INITIALIZED = 2,
        CALIBRATIONEX_NO_NEW_TARGETS = 3,
        CALIBRATIONEX_INVALID_TARGET_HANDLE = 4,
        CALIBRATIONEX_RIGHT_EYE_NOT_FOUND = 5,
        CALIBRATIONEX_LEFT_EYE_NOT_FOUND = 6,
        CALIBRATIONEX_NO_EYE_FOUND = 7,
        CALIBRATIONEX_NOT_ALL_TARGETS_CALIBRATED = 8,
        CALIBRATIONEX_INVALID_INDEX = 9,
        CALIBRATIONEX_INTERNAL_TIMEOUT = 10,
    }

    public enum CalibrationStyle
    {
        CAL_STYLE_5_POINT = 0,
        CAL_STYLE_9_POINT = 1,
        CAL_STYLE_16_POINT = 2,
    }

    public enum ClickMethod
    {
        CLICK_METHOD_NONE = 0,
        CLICK_METHOD_BLINK = 1,
        CLICK_METHOD_DWELL = 2,
    }

    public enum EyesToProcess
    {
        EYES_TO_PROC_SINGLE_LEFT = 0,
        EYES_TO_PROC_SINGLE_RIGHT = 1,
        EYES_TO_PROC_DUAL_LEFT_OR_RIGHT = 2,
        EYES_TO_PROC_DUAL_LEFT_AND_RIGHT = 3,
    }

    public enum ProcessPriority
    {
        PROC_PRIORITY_0 = 0,
        PROC_PRIORITY_1 = 1,
        PROC_PRIORITY_2 = 2,
        PROC_PRIORITY_3 = 3,
    }

    public enum CameraGainMethod
    {
        CAM_GAIN_METHOD_AUTO = 0,
        CAM_GAIN_METHOD_MANUAL = 1,
    }

    public enum CameraGPIOOutput
    {
        CAM_GPIO_OUT_CUSTOM = 0,
        CAM_GPIO_OUT_LEFT_TRACKING_STATUS = 1,
        CAM_GPIO_OUT_RIGHT_TRACKING_STATUS = 2,
    }

    public enum CameraCustomGPIOOutputValue
    {
        CAM_GPIO_OUT_VALUE_NO_CHANGE = 0,
        CAM_GPIO_OUT_VALUE_HIGH = 1,
        CAM_GPIO_OUT_VALUE_LOW = 2,
    }

    public enum ToolBarImageDisplay
    {
        IMG_DISP_LIVE_IMAGE = 0,
        IMG_DISP_PSEUDO_IMAGE = 1,
    }

    public enum QGWindowState
    {
        QGWS_HOME = 0,
        QGWS_EYE_TOOLS = 1,
        QGWS_EYE_TOOLS_IMAGE = 2,
        QGWS_HIDEN = 3,
    }

    /*-----------------------------------------------------------------------------
    //  Name: DPoint
    //
    //  Description:
    //      This structure contains an x and y value that are of type double.
    //
    //  See Also:
    //      LPoint
    //      EyeData
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct DPoint
    {
        public double x;
        public double y;
    }

    /*-----------------------------------------------------------------------------
    //  Name: LPoint
    //
    //  Description:
    //      This structure contains an x and y value that are of type long.
    //
    //  See Also:
    //      DPoint
    //      EyeData
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct LPoint
    {
        public cpp_long x;
        public cpp_long y;
    }

    /*-----------------------------------------------------------------------------
    //  Name: EyeData
    //
    //  Description:
    //      This structure is used as a container for holding gaze data and eye
    //      metrics for an individual eye. All of the members of this structure are
    //      dependant on the Found member. If the Found member is true then the
    //      other members are valid. If it is false then the other members are
    //      invalid.
    //
    //  See Also:
    //      ImageData
    //      DPoint
    //      LPoint
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct EyeData
    {
        //  The position, in pixels, of the pupil center on the image.
        public DPoint Pupil;

        //  The position, in pixels, of the center of the glints on the image. Glint
        //  asignation is arbitrary, so Glint1 could be either the left glint or the
        //  right glint. Check the x postion of the glints to determine if a glint is to
        //  the left or right of the other.
        public DPoint Glint1;
        public DPoint Glint2;

        //  The gaze position of the eye in screen pixels. When both eyes are being
        //  tracked, a more accurate single gaze point can be found by averaging the
        //  gaze points of both eyes. The GazePoint is not limited by the size of the
        //  screen, so areas off of the screen can be tracked as well. However, accuracy
        //  drops off the further away the gaze gets from the screen(the calibrated
        //  area). One example of how this could be useful is if there were areas on the
        //  bezel of the screen that activated different functions when the user looked
        //  at them. Off screen values are represented by values that are larger or
        //  smaller than the min/max boundaries of the screen. This value is affected by
        //  the Processing_SmoothingFactor setting. To get data that is truly
        //  unfiltered, the Processing_SmoothingFactor setting needs to be set to a
        //  value of 1. The eye must be calibrated for this member to be valid (see
        //  Calibrated).
        public LPoint GazePoint;

        //  The diameter of the pupil in millimeters. The diameter increments in .6mm
        //  increments.
        public double PupilDiameter;

        //  The Found member refers to whether or not the eye was found this frame. If
        //  Found is true then the other members of the EyeData structure are valid. If
        //  Found is false then the other members are invalid.
        [MarshalAs(UnmanagedType.U1)]
        public bool Found;

        //  The Calibrated member refers to whether or not the eye is calibrated. If
        //  Calibrated is true then the GazePoint member is valid. If Calibrated is
        //  false then the GazePoint member is invalid.
        [MarshalAs(UnmanagedType.U1)]
        public bool Calibrated;
    }

    /*-----------------------------------------------------------------------------
    //  Name: ImageData
    //
    //  Description:
    //      This structure is used as a container for transfering gaze data, image
    //      data, and eye metrics.
    //
    //  See Also:
    //      EyeData
    //      SetCopyImageFlag()
    //      GetImageData()
    //      GetImageDataAndLatency()
    //      ReadBulkCapture()
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct ImageData
    {
        //  The Time member contains the system time of the computer when the image was
        //  received by the camera. To get the time that image integration ended, you can
        //  subtract the data transfer time (1/fps) from this value.
        public double Time;

        //  The LeftEye and RightEye members contain all the gaze data and eye metrics
        //  for each eye. See the description of the EyeData structure for more
        //  information.
        public EyeData LeftEye;
        public EyeData RightEye;

        //  The PixelData member contains the actual pixel data of the image that was
        //  used to calculate the gaze point. It is only valid if SetCopyImageFlag() was
        //  called with a value of true passed into its argument. Also,
        //  ReadBulkCapture() does not buffer the PixelData member of the captured
        //  frames regardless of whether true or false was passed to SetCopyImageFlag().
        //  So PixelData will always be invalid when obtained by it.
        public IntPtr PixelData; // was unsigned char *

        //  The Width refers to the width in pixels of the image.
        public int Width;

        //  The Height refers to the height in pixels of the image
        public int Height;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ClickingOptions
    {
        //  Click_AudibleFeedback enables audible feedback with blink or dwell click.
        [MarshalAs(UnmanagedType.U1)]
        public bool Click_AudibleFeedback;

        //  The amount of time in milliseconds between when a blink or dwell click is
        //  initiated and when it is actually done. This parameter is not usually
        //  changed. It is mainly used for troubleshooting.
        public uint Click_Delay;

        //  The magnification power used when the zoom tool or the click confirm tool 
        //  are being used.
        public uint Click_ZoomFactor;

        //  The type of software clicking detected by Quick Glance. This determines 
        //  whether a blink or a dwell action will post a click event.
        public ClickMethod Click_Method;

        ////// Blink Clicking Options //////

        //  The time in tenths of seconds the eye must be closed to register a click. 
        //  If a user does not have their eyes closed for at least this amount of time 
        //  then no click will be registered.
        public uint Blink_PrimaryTime;

        //  The user has this amount of time in tenths of seconds to open their eyes 
        //  after the primary time has been reached in order to perform a primary 
        //  click. If the user keeps their eyes closed longer than the primary time + 
        //  the secondary time then they are able to do a secondary click. This value 
        //  is used only when Blink_EnableSecondaryClick is true.
        public uint Blink_SecondaryTime;

        //  If Blink_EnableSecondaryClick is true then the user has this amount of time
        //  in tenths of seconds to open their eyes after the primary time + the 
        //  secondary time has been reached in order to perform a secondary click. If 
        //  the user keeps their eyes closed longer than the primary time + the 
        //  secondary time + the cancel time then the blink will be voided and no click
        //  will be performed. If Blink_EnableSecondaryClick is false then this time is
        //  the amount of time a user has after the primary click to open their eyes and
        //  register a primary click. Once the primary time + the cancel time has been
        //  exceeded, the click will be voided and no click will be performed.
        public uint Blink_CancelTime;

        //  If this is false then the blink secondary time has no effect and the cancel
        //  time acts as the new timeout time for the primary click. (See
        //  Blink_SecondaryTime and Blink_CancelTime)
        [MarshalAs(UnmanagedType.U1)]
        public bool Blink_EnableSecondaryClick;

        //  Both eyes must blink for a click to be performed.
        [MarshalAs(UnmanagedType.U1)]
        public bool Blink_BothEyesRequired;

        //  Draw visual indicators on the screen when blink clicking.
        [MarshalAs(UnmanagedType.U1)]
        public bool Blink_VisualFeedback;

        ////// Dwell Clicking Options //////

        //  This is the width and the height in millimeters of a box in which the 
        //  cursor must reside in order to register a dwell click.
        public uint Dwell_BoxSize;

        //  The time in tenths of seconds in which the cursor must stay within the 
        //  DwellBoxSize in order to perform a click.
        public uint Dwell_Time;
    }

    /*-----------------------------------------------------------------------------
    //  Name: CalibrationOptions
    //
    //  Description:
    //      These options control how the calibration is performed.
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct CalibrationOptions
    {
        //  The lenght of time in tenths of seconds each target will take to calibrate.
        public int Calibration_TargetTime;

        //  The number of targets to be used during calibration. More calibration 
        //  targets will make the duration of the entire calibration process longer, 
        //  but will also usually result in a more accurate and reliable calibration. 
        public CalibrationStyle Calibration_Style;
    }

    /*-----------------------------------------------------------------------------
    //  Name: ProcessingOptions
    //
    //  Description:
    //      These options control how certain aspects of the processing are done.      
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct ProcessingOptions
    {
        //  The amount of jitter reduction performed on the gazepoint received.  Higher 
        //  numbers produce less jitter, but also introduce lag. The amount of lag in 
        //  seconds can be determined by the formula 
        //  "lag = smoothing_factor / actual_frame_rate / 2".
        public uint Processing_SmoothingFactor;

        //  This determines whether the right and/or left eye will be tracked. If
        //  EYES_TO_PROC_SINGLE_LEFT or EYES_TO_PROC_SINGLE_RIGHT is selected then only 
        //  that eye respectivly is tracked. If EYES_TO_PROC_DUAL_LEFT_OR_RIGHT is 
        //  selected then both eyes will be tracked, but tracking will still work if 
        //  only one eye is in the image. If EYES_TO_PROC_DUAL_LEFT_AND_RIGHT is 
        //  selected then tracking will not work unless both eyes are found in the 
        //  image. EYES_TO_PROC_DUAL_LEFT_OR_RIGHT is the most common processing 
        //  method. This selection also determines which eyes will be calibrated when a
        //  calibration is performed.
        public EyesToProcess Processing_EyesToProcess;

        //  This enables image capturig from the camera. This is usually set to true 
        //  and only changed for troubleshooting purposes.
        [MarshalAs(UnmanagedType.U1)]
        public bool Processing_EnableCapture;

        //  This enables processing of the image from the camera. This is usually set 
        //  to true and only changed for troubleshooting purposes.
        [MarshalAs(UnmanagedType.U1)]
        public bool Processing_EnableProcessing;

        //  This enables/disables the displaying of the eye tracker image within Quick
        //  Glance.
        [MarshalAs(UnmanagedType.U1)]
        public bool Processing_EnableDisplay;

        //  This enables/disables cursor movement acording to gazepoint. SetEyeControl()
        //  starts and stops cursor movement, but it will have no effect if this value
        //  is false.
        [MarshalAs(UnmanagedType.U1)]
        public bool Processing_EnableCursorMovement;

        //  This enables/disables the processing of dwell and blink clicks.
        [MarshalAs(UnmanagedType.U1)]
        public bool Processing_EnableClicking;

        //  This is the maximum amount of time in milliseconds the process will use
        //  without giving up some of it's time slice to other processes. This is mostly
        //  usefull on slower machines where the eye tracker is a processing burden. On
        //  slower machines where the eye tracker is a large burden this value should be
        //  set lower.
        public uint Processing_MaxProcessTime;

        //  This is the priority the eye tracker has on the system.
        public ProcessPriority Processing_ProcessPriority;
    }

    /*-----------------------------------------------------------------------------
    //  Name: CameraOptions
    //
    //  Description:
    //      These options control camera operation.
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct CameraOptions
    {
        //  This is the percentage of bus bandwidth used by the camera. This value 
        //  dirrectly affects the framerate of the camera. Smaller values result in a 
        //  lesser ammount of pixel throughput resulting in smaller number of total 
        //  frames that can be transfered per second.
        public uint Camera_BusBandwidthPercentage;

        //  This is the percentage of the image height used when the eyes are being 
        //  tracked. This value dirrectly affects the framerate of the camera. The 
        //  smaller the value, the fewer the number of pixels that have to be 
        //  transfered each frame resulting in more frames for a given bandwidth. If
        //  this value is too small then vertical head movement is limited because the
        //  eyes will move out of the region of interest before the camera has time to
        //  readjust.
        public uint Camera_ImageROIPercentage;

        //  This determines the gain method used for the camera. This should normally 
        //  be set to CAM_GAIN_METHOD_AUTO.
        public CameraGainMethod Camera_GainMethod;

        //  This is the gian value used when the gain method is set to 
        //  CAM_GAIN_METHOD_MANUAL. This is normally not used.
        public double Camera_GainValue;

        //  These values determines the output value of the GPIO Pins on the camera
        //  board. When these value are set to CAM_GPIO_OUT_CUSTOM then the value can be
        //  set by using the function SetCustomGPIO(). If this value is
        //  CAM_GPIO_OUT_LEFT_TRACKING_STATUS or CAM_GPIO_OUT_RIGHT_TRACKING_STATUS then
        //  the lock status of the left or right eye will be represented by the GPIO
        //  coresponding value.
        public CameraGPIOOutput Camera_GPIO_1;
        public CameraGPIOOutput Camera_GPIO_2;
        public CameraGPIOOutput Camera_GPIO_3;
    }

    /*-----------------------------------------------------------------------------
    //  Name: ToolbarOptions
    //
    //  Description:
    //      These options control how the toolbar looks.
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct ToolbarOptions
    {
        //  The approximate size in millimeters in the x dirrection of each toolbar
        //  button.
        public uint Toolbar_ButtonSizeX;

        //  The approximate size in millimeters in the y dirrection of each toolbar
        //  button.
        public uint Toolbar_ButtonSizeY;

        //  The way the image is displayed in the toolbar. If IMG_DISP_LIVE_IMAGE is 
        //  selected then the normal live image will be displayed in the toolbar. If 
        //  IMG_DISP_PSEUDO_IMAGE is selected then circles representing the eyes
        //  will be displayed to show the user where their eyes are in the image.
        public ToolBarImageDisplay Toolbar_ImageDisplayType;
    }

    /* If you create an instance of the QuickLink class, the constructor will 
     * look through the system registry, and find and load the QuickLink DLLs 
     * from inside QuickGlance's installation folder.  The alternative is to 
     * place copies of QuickGlance.DLL and PGRFlycapture.dll into the directory
     * with your executable; which will allow you to forgo instantiating 
     * this QuickLink class before calling any of the API methods.
     */
    public class QuickLink : IDisposable
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);

        private const string QuickLinkDllName = "QuickLinkAPI.dll";
        private const string PGRFlyCaptureDLLName = "PGRFlyCapture.dll";

        private static string[] PossibleRegistryKeyLocations = { @"SOFTWARE\EyeTech Digital Systems", @"SOFTWARE\Wow6432Node\EyeTech Digital Systems" };

        // Registry key is slightly different if 64bit support is enabled.
#if SYSTEM_64BIT
        private string QuickLinkAPIKey = "QuickLinkAPI64";
#else
        private string QuickLinkAPIKey = "QuickLinkAPI";
#endif

        /* These handles are the return values from calls to LoadLibrary.  We 
         * use them to free the libraries when we dispose. 
         */
        private IntPtr dfHandle, qlHandle;

        private bool disposed = false;

        public QuickLink()
        {
            RegistryKey reg = Registry.LocalMachine;

            RegistryKey qlRegKeyLoc = null;
            foreach (string s in PossibleRegistryKeyLocations)
            {
                // Try each possible key location.
                qlRegKeyLoc = reg.OpenSubKey(Path.Combine(s, QuickLinkAPIKey));
                if (qlRegKeyLoc != null)
                    // Found the key.
                    break;
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
            string dfPath = Path.Combine(Path.GetDirectoryName(qlPath), PGRFlyCaptureDLLName);

            /* Now we load the DLLs into our address space.  This allows us to 
             * use DLLImport without knowing the full path to the DLLs until 
             * load time.
             */

            this.dfHandle = LoadLibrary(dfPath);
            if ((int)this.dfHandle == 0)
                // Unable to load the library!
                throw new Win32Exception(Marshal.GetLastWin32Error());

            this.qlHandle = LoadLibrary(qlPath);
            if ((int)this.qlHandle == 0)
                // Unable to load the library!
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        ~QuickLink()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed == false)
            {
                FreeLibrary(this.qlHandle);
                FreeLibrary(this.dfHandle);
            }
            this.disposed = true;
        }

        /*-----------------------------------------------------------------------------
        //
        //  Name: GetQGOnFlag()
        //
        //  Description:
        //      This function determines whether Quick Glance is running.
        //
        //  Return Value:
        //      If Quick Glance is running then true is returned otherwise false is 
        //      returned.
        //
        //  See Also:
        //      ExitQuickGlance()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetQGOnFlag")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool GetQGOnFlag();


        /*-----------------------------------------------------------------------------
        //  Name: ExitQuickGlance()
        //
        //  Description:
        //      This function exits Quick Glance.
        //
        //  See Also:
        //      GetQGOnFlag()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "ExitQuickGlance")]
        public static extern
            void ExitQuickGlance();


        /*-----------------------------------------------------------------------------
        //  Name: SetEyeControl()
        //
        //  Description:
        //      This function enables or disables the cursor control within Quick
        //      Glance.
        //
        //  Arguments:
        //      Enable - The state of the eye control. If this value is true then the
        //               cursor will move with the user's gaze as long as the user is
        //               calibrated. If this value is false the cursor will stop moving
        //               with the user's gaze.
        //
        //  See Also:
        //      GetEyeControl()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "SetEyeControl")]
        public static extern
            void SetEyeControl(
                [MarshalAs(UnmanagedType.VariantBool)] bool Enable);


        /*-----------------------------------------------------------------------------
        //  Name: GetEyeControl()
        //
        //  Description:
        //      This function disables the cursor control within Quick Glance. After 
        //      this function is called the cursor will not move with the users gaze.
        //
        //  Return Value:
        //      If eye control is enabled then true will be returned. If eye control is
        //      disabled then false will be returned.
        //
        //  See Also:
        //      SetEyeControl()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetEyeControl")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool GetEyeControl();


        /*-----------------------------------------------------------------------------
        //  Name: SetMWState()
        //
        //  Description:
        //      This function sets the state of the Quick Glance window.
        //
        //  Arguments:
        //      MWState - The window state for Quick Glance.
        //
        //  See Also:
        //      MoveLeftRight()
        //      MoveUpDown()
        //      ToggleLargeImage()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "SetMWState")]
        public static extern
            void SetMWState(
                QGWindowState MWState);


        /*-----------------------------------------------------------------------------
        //  Name: ToggleLargeImage()
        //
        //  Description:
        //      If the large image is not already displayed then this function will 
        //      display it. If the large image is already displayed then this function 
        //      will hide it.
        //
        //  See Also:
        //      HideLargeImage()
        //      ShowLargeImage()
        //
        */
        [DllImport(QuickLinkDllName, EntryPoint = "ToggleLargeImage")]
        public static extern
            void ToggleLargeImage();


        /*-----------------------------------------------------------------------------
        //  Name: ShowLargeImage()
        //
        //  Description:
        //      This function will show the large image. If the large image is already 
        //      displayed then this function does nothing.
        //
        //  See Also:
        //      ToggleLargeImage()
        //      HideLargeImage()
        //
        */
        [DllImport(QuickLinkDllName, EntryPoint = "ShowLargeImage")]
        public static extern
            void ShowLargeImage();


        /*-----------------------------------------------------------------------------
        //  Name: HideLargeImage()
        //
        //  Description:
        //      This functijon hides the large image if it is displayed. If it is not 
        //      displayed then this function does nothing.
        //
        //  See Also:
        //      ToggleLargeImage()
        //      ShowLargeImage()
        //
        */
        [DllImport(QuickLinkDllName, EntryPoint = "HideLargeImage")]
        public static extern
            void HideLargeImage();


        /*-----------------------------------------------------------------------------
        //  Name: MoveLeftRight()
        //
        //  Description:
        //      When Quick Glance is in the QGWS_EYE_TOOLS or the QGWS_EYE_TOOLS_IMAGE
        //      state, it will be positioned in any of the four corners of the screen.
        //      Calling this function will toggle it from the left side of the screen to
        //      the right side of the screen.
        //
        //  See Also:
        //      MoveUpDown()
        //      SetMWState()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "MoveLeftRight")]
        public static extern
            void MoveLeftRight();


        /*-----------------------------------------------------------------------------
        //  Name: MoveUpDown()
        //
        //  Description:
        //      When Quick Glance is in the QGWS_EYE_TOOLS or the QGWS_EYE_TOOLS_IMAGE
        //      state, it will be positioned in any of the four corners of the screen.
        //      Calling this function will toggle it from the bottom of the screen to
        //      the top of the screen.
        //
        //  See Also:
        //      MoveLeftRight()
        //      SetMWState()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "MoveUpDown")]
        public static extern
            void MoveUpDown();


        /*-----------------------------------------------------------------------------
        //  Name: GetSDKVersion()
        //
        //  Description:
        //      This function gets the API version number.
        //
        //  Return Value:
        //      The API version number is returned.
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetSDKVersion")]
        public static extern
            double GetSDKVersion();


        /*-----------------------------------------------------------------------------
        //  Name: SetCopyImageFlag()
        //
        //  Description:
        //      This function enables or disables the copying of pixel data to the
        //      shared memory.
        //
        //  Arguments:
        //      CopyImage   - This determines whether or not the pixel data of the 
        //                    current image is copied into shared memory. If true then 
        //                    the image's pixel data will be accessible. If false then 
        //                    the images pixel data will not be accessible. If access 
        //                    to the image's pixel data is not required then it is 
        //                    preferable to not copy the pixel data in order to save 
        //                    processing time.
        //
        //  See Also:
        //      GetImageData()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "SetCopyImageFlag")]
        public static extern
            void SetCopyImageFlag(
                [MarshalAs(UnmanagedType.VariantBool)] bool CopyImage);


        /*-----------------------------------------------------------------------------
        //  Name: GetImageData()
        //
        //  Description:
        //      This function retrieves the real-time image data.
        //
        //  Arguments:
        //      MaxTimeout - The maximum time in milliseconds to wait for a new image. 
        //                   If a new image becomes available while waiting then the 
        //                   function returns immediately.
        //      Data       - The data relevant to the current image. This is only 
        //                   valid when the function returns true. Data->PixelData is 
        //                   only valid when the function returns true and when true 
        //                   was passed into SetCopyImageFlag().
        //
        //  Return Value:
        //      If a new image is ready then true is returned and Data is valid. If 
        //      MaxTimeout has been reached without a new image being ready then false 
        //      is returned and Data is invalid.
        //
        //  See Also:
        //      SetCopyImageFlag()
        //      GetImageDataAndLatency()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetImageData")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool GetImageData(
            cpp_ulong MaxTimeout,
            ref ImageData Data);


        /*-----------------------------------------------------------------------------
        //  Name: GetImageDataAndLatency()
        //
        //  Description:
        //      This function retrieves the real-time image data as well as the
        //      calculated pipeline latency from image acquisition to the return of the
        //      function.
        //
        //  Arguments:
        //      MaxTimeout - The maximum time in milliseconds to wait for a new image. 
        //                   If a new image becomes available while waiting then the 
        //                   function returns immediately.
        //      Data       - The data relevant to the current image. This is only 
        //                   valid when the function returns true. Data->PixelData is 
        //                   only valid when the function returns true and when true 
        //                   was passed into SetCopyImageFlag().
        //      Latency    - The calculated latency in milliseconds from the time the 
        //                   image was captured to the time this function returns.
        //
        //  Return Value:
        //      If a new image is ready then true is returned and Data and Latency are
        //      valid. If MaxTimeout has been reached without a new image being ready
        //      then false is returned and Data and Latency are invalid.
        //
        //  See Also:
        //      SetCopyImageFlag()
        //      GetImageData()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetImageDataAndLatency")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool GetImageDataAndLatency(
                cpp_ulong MaxTimeout,
                ref ImageData Data,
                out double Latency);


        /*-----------------------------------------------------------------------------
        //  Name: StartBulkCapture()
        //
        //  Description:
        //      This function starts the collecting of tracking data. It buffers the 
        //      data so the desired number of gaze points can be collected, then the 
        //      points can be read back one at a time after collection is finnished. 
        //      Data retrevial does not have to wait until collection has finnished. If
        //      data collection is in process due to a previous call to this function
        //      then a new call to this function has no effect. When data is being
        //      captured due to a call to this function then data received by a call to
        //      GetImageData will not be valid.
        //
        //  Arguments:
        //      NumPoints - The number of points to be collected. Data collection will 
        //                  stop when this number of data points has been received or 
        //                  if StopBulkCapture has been called
        //
        //  See Also:
        //      StopBulkCapture()
        //      QueryBulkCapture()
        //      ReadBulkData()
        //      SetCopyImageFlag()
        //      GetImageData()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "StartBulkCapture")]
        public static extern
            void StartBulkCapture(
                uint NumPoints);


        /*-----------------------------------------------------------------------------
        //  Name: StopBulkCapture()
        //
        //  Description:
        //      This stops the collection of tracking data. If data is not being 
        //      collected by a call to StartBulkCapture then this function has no 
        //      effect. This function does not need to be called before data can be 
        //      read by calling ReadBulkCapture. This function also does not need to be 
        //      called for data capturing to terminate. This function only needs to be 
        //      called if the data capturing needs to be stopped prior to capturing the 
        //      number of data points specified by the coresponding call to 
        //      StartBulkCapture.
        //
        //  See Also:
        //      StartBulkCapture()
        //      QueryBulkCapture()
        //      ReadBulkCapture()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "StopBulkCapture")]
        public static extern
            void StopBulkCapture();


        /*-----------------------------------------------------------------------------
        //  Name: QueryBulkCapture()
        //
        //  Description:
        //      This function retrieves the status of data capturing started by a call 
        //      to StartBulkCapture.
        //
        //  Arguments:
        //      NumCaptured    - The number of data points that have been captured since 
        //                       the data capturing was started.
        //      NumNotCaptured - The number of data points that still have to be 
        //                       captured before capturing will automatically 
        //                       terminate. This value plus NumCaptured sum to the 
        //                       value passed in to StartBulkCapture.
        //      NumRead        - The number of data points that have been read by calls 
        //                       to ReadBulkCapture.
        //      NumNotRead     - The number of data points that have been collected but 
        //                       remain to be read by calls to ReadBulkCapture. This 
        //                       value plus NumRead sum to the value NumCaptured.
        //      Capturing      - The status of the data capturing. true indicates that 
        //                       data points are still being collected and false 
        //                       indicates that no more data points will be collected.
        //
        //  Return Value:
        //      The success of the function. If false then the retrieved values are not
        //      valid.
        //
        //  See Also:
        //      StartBulkCapture()
        //      StopBulkCapture()
        //      ReadBulkCapture()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "QueryBulkCapture")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool QueryBulkCapture(
                out uint NumCaptured,
                out uint NumNotCaptured,
                out uint NumRead,
                out uint NumNotRead,
                [MarshalAs(UnmanagedType.VariantBool)] out bool Capturing);


        /*-----------------------------------------------------------------------------
        //  Name: ReadBulkCapture()
        //
        //  Description:
        //      This function is used to read data that was captured by a call to 
        //      StartBulkCapture. Capturing does not need to be finished before this 
        //      function can be called. The number of captured data points that are 
        //      available at any given time can be determined by calling 
        //      QueryBulkCapture.
        //
        //  Arguments:
        //      MaxTimeout - The amount of time in milliseconds to wait for a new data 
        //                   point to be received if there is not one available.
        //      Data       - The next available data point to be read.
        //
        //  Return Value:
        //      The success of the function. If false then the data recieved is 
        //      invalid.
        //
        //  See Also:
        //      StartBulkCapture()
        //      StopBulkCapture()
        //      QueryBulkCapture()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "ReadBulkCapture")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool ReadBulkCapture(
                cpp_ulong MaxTimeout,
                ref ImageData Data);


        /*-----------------------------------------------------------------------------
        //  Name: InternalCalibration1()
        //
        //  Description:
        //      This function initiates a calibration internally within Quick Glance. 
        //      It uses the user interface within Quick Glance to perform the 
        //      calibration. The calibration process is immediately started without 
        //      user intervention, stopping only if there is an error and when the 
        //      calibration completes to display the calibration score.
        */
        [DllImport(QuickLinkDllName, EntryPoint = "InternalCalibration1")]
        public static extern
            void InternalCalibration1();

        /*-----------------------------------------------------------------------------
        //  Name: InitializeCalibrationEx()
        //
        //  Description:
        //      This function initializes a calibration. It must be called before any 
        //      other calibration functions can be called. This calibration process 
        //      uses custom targets. The eyes that will be calibrated, the durration 
        //      for each target and the style of calibration to be performed are 
        //      determined by what is set in the user options.
        //
        //  Arguments:
        //      CalibrationIndex - The desired calibration to calibrate. If the 
        //                         calibration does not already exist it will be 
        //                         created. The value can be any number that unsigned 
        //                         int will allow. Calibrations do not have to be 
        //                         created in any specific order nor do all numbers 
        //                         have to be used between the lowest value index and 
        //                         the highest value index. No additional disk space or 
        //                         memory is used by creating, for example, three 
        //                         different calibrations with indexes 100, 1050, and 
        //                         21 as opposed to indexes 1, 2 and 3. To recal a 
        //                         saved calibration by its index use the function
        //                         OpenCalibrationEx().
        //
        //  Return Value:
        //      CALIBRATIONEX_NO_EYE_SELECTED  - The Processing_EyesToProcess parameter 
        //                                       is set to an invalid value.
        //      CALIBRATIONEX_INTERNAL_TIMEOUT - It is likely that Quick Glance is not 
        //                                       running.
        //      CALIBRATIONEX_OK               - The function completed successflly.
        //
        //  See Also:
        //      GetNewTargetPositionEx()
        //      GetWorstTargetPositionEx()
        //      CalibrateEx()
        //      GetScoreEx()
        //      ApplyCalibrationEx()
        //      OpenCalibrationEx()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "InitializeCalibrationEx")]
        public static extern
            CalibrationErrorEx InitializeCalibrationEx(
                uint CalibrationIndex);


        /*-----------------------------------------------------------------------------
        //  Name: GetNewTargetPositionEx()
        //
        //  Description:
        //      This function retrieves the position and identification handle for the 
        //      next target to be calibrated.
        //
        //  Arguments:
        //      X            - The X position in screen coordinates of where the focal 
        //                     point of the target should be placed.
        //      Y            - The Y position in screen coordinates of where the focal 
        //                     point of the target should be placed.
        //      TargetHandle - A handle used to identify the target corresponding to 
        //                     the retrieved position. This handle is used for  
        //                     CalibrateEx().
        //
        //  Return Value:
        //      CALIBRATIONEX_NOT_INITIALIZED  - The calibration has not been 
        //                                       initialized.
        //      CALIBRATIONEX_NO_NEW_TARGETS   - All targets have been calibrated.
        //      CALIBRATIONEX_INTERNAL_TIMEOUT - It is likely that Quick Glance is not 
        //                                       running.
        //      CALIBRATIONEX_OK               - The function completed successflly.
        //
        //  See Also:
        //      InitializeCalibrationEx()
        //      CalibrateEx()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetNewTargetPositionEx")]
        public static extern
            CalibrationErrorEx GetNewTargetPositionEx(
                out int X,
                out int Y,
                out int TargetHandle);


        /*-----------------------------------------------------------------------------
        //  Name: GetWorstTargetPositionEx()
        //
        //  Description:
        //      This function retrieves the position and identification handle for the 
        //      worst target that was calibrated. The calibration must be finished 
        //      before this function can be called.
        //
        //  Arguments:
        //      X            - The X position in screen coordinates of where the focal 
        //                     point of the target should be placed.
        //      Y            - The Y position in screen coordinates of where the focal 
        //                     point of the target should be placed.
        //      TargetHandle - A handle used to identify the target corresponding to 
        //                     the retrieved position. This handle is used for  
        //                     CalibrateEx().
        //
        //  Return Value:
        //      CALIBRATIONEX_NOT_INITIALIZED            - The calibration has not been 
        //                                                 initialized.
        //      CALIBRATIONEX_NOT_ALL_TARGETS_CALIBRATED - Not all targets have been 
        //                                                 calibrated.
        //      CALIBRATIONEX_INTERNAL_TIMEOUT           - It is likely that Quick 
        //                                                 Glance is not running.
        //      CALIBRATIONEX_OK                         - The function completed 
        //                                                 successflly.
        //
        //  See Also:
        //      InitializeCalibrationEx()
        //      GetNewTargetPositionEx()
        //      CalibrateEx()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "CalibrationErrorEx")]
        public static extern CalibrationErrorEx
            GetWorstTargetPositionEx(
                out int X,
                out int Y,
                out int TargetHandle);


        /*-----------------------------------------------------------------------------
        //  Name: CalibrateEx()
        //
        //  Description:
        //      This function calibrates the target with the corresponding target 
        //      handle. This function should be called after the target is displayed.
        //
        //  Arguments:
        //      TargetHandle - The handle retrieved by calling GetNewTargetPositionEx()
        //                     or GetWorstTargetPositionEx().
        //
        //  Return Value:
        //      CALIBRATIONEX_NOT_INITIALIZED       - The calibration has not been 
        //                                            initialized.
        //      CALIBRATIONEX_INVALID_TARGET_HANDLE - The target handle does not 
        //                                            correspond to any target 
        //      CALIBRATIONEX_NO_EYE_FOUND          - No eyes were found. This could 
        //                                            also mean that some of the 
        //                                            processing options are not 
        //                                            enabled correctly.
        //      CALIBRATIONEX_LEFT_EYE_NOT_FOUND    - The left eye was not found
        //      CALIBRATIONEX_RIGHT_EYE_NOT_FOUND   - The right eye was not found
        //      CALIBRATIONEX_INTERNAL_TIMEOUT      - It is likely that Quick Glance 
        //                                            is not running.
        //      CALIBRATIONEX_OK                    - The function completed 
        //                                            successflly.
        //
        //  See Also:
        //      InitializeCalibrationEx()
        //      GetNewTargetPositionEx()
        //      GetWorstTargetPositionEx()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "CalibrateEx")]
        public static extern
            CalibrationErrorEx CalibrateEx(
                int TargetHandle);


        /*-----------------------------------------------------------------------------
        //  Name: GetScoreEx()
        //
        //  Description:
        //      This function retrieves the score for a finished calibration. The 
        //      calibration process must be finished before this function can be 
        //      called.
        //
        //  Arguments:
        //      Left  - The score written to Left is only valid if the left eye was set 
        //              to calibrate in the initialization.
        //      Right - The score written to Right is only valid if the right eye was 
        //              set to calibrate in the initialization.
        //
        //  Return Value:
        //      CALIBRATIONEX_NOT_INITIALIZED            - The calibration has not been 
        //                                                 initialized.
        //      CALIBRATIONEX_NOT_ALL_TARGETS_CALIBRATED - The calibration has not 
        //                                                 completed.
        //      CALIBRATIONEX_INTERNAL_TIMEOUT           - It is likely that Quick 
        //                                                 Glance is not running.
        //      CALIBRATIONEX_OK                         - The function completed 
        //                                                 successflly.
        //
        //  See Also:
        //      InitializeCalibrationEx()
        //      CalibrateEx()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetScoreEx")]
        public static extern
            CalibrationErrorEx GetScoreEx(
                out double left,
                out double right);


        /*-----------------------------------------------------------------------------
        //  Name: ApplyCalibrationEx()
        //
        //  Description:
        //      This function applies the new calibration. The new calibration only 
        //      replaces the old calibration for the newly calibrated eye(s). If only 
        //      one eye was newly calibrated and the other eye had a previous 
        //      calibration, its calibration will be untouched. the calibration process 
        //      must be finished before this function can be called.
        //
        //  Return Value:
        //      CALIBRATIONEX_NOT_INITIALIZED            - The calibration has not been 
        //                                                 initialized.
        //      CALIBRATIONEX_NOT_ALL_TARGETS_CALIBRATED - The calibration has not 
        //                                                 completed.
        //      CALIBRATIONEX_INTERNAL_TIMEOUT           - It is likely that Quick 
        //                                                 Glance is not running.
        //                                       
        //      CALIBRATIONEX_OK                         - The function completed 
        //                                                 successflly.
        //
        //  See Also:
        //      InitializeCalibrationEx()
        //      CalibrateEx()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "ApplyCalibrationEx")]
        public static extern
            CalibrationErrorEx ApplyCalibrationEx();


        /*-----------------------------------------------------------------------------
        //  Name: CalibrationBiasEx()
        //
        //  Description:
        //      This function can be used to introduce a bias in the current 
        //      calibration. This is useful in situations when the user has a good 
        //      calibration score but the cursor may be off a little bit in one 
        //      direction. This can be used to correct that offset. This function will 
        //      not change the users calibration score. InitializeCalibrationEx() does 
        //      not need to be called before this function can be called. This function 
        //      applies the bias to the current active calibration.
        //
        //  Arguments:
        //      DeltaX - The offset in the X direction. This is calculated by the 
        //               formula:
        //               X1 = (X value of gaze point retreived from the eyetracker)
        //               X2 = (X value of the displayed target at which the user is 
        //                    looking)
        //               DeltaX = X1 - X2;
        //      DeltaY - The offset in the Y direction. This is calculated by the 
        //               formula:
        //               Y1 = (Y value of gaze point retreived from the eyetracker)
        //               Y2 = (Y value of the displayed target at which the user is 
        //                    looking)
        //               DeltaY = Y1 - Y2;
        */
        [DllImport(QuickLinkDllName, EntryPoint = "CalibrationBiasEx")]
        public static extern
            void CalibrationBiasEx(
                int DeltaX,
                int DeltaY);


        /*-----------------------------------------------------------------------------
        //  Name: OpenCalibrationEx()
        //
        //  Description:
        //      This function opens a calibration previously performed. 
        //      InitializeCalibrationEx() does not need to be called before this 
        //      function can be called, nor does a successful calibration have to be 
        //      completed prior to opening.
        //
        //  Arguments:
        //      CalibrationIndex - The index of the calibration to be retrieved. If the 
        //                         index does not exist then the function will return 
        //                         CALIBRATIONEX_INVALID_INDEX and the calibration will 
        //                         be set to uncalibrated. If it is believed that the 
        //                         specified calibration did exist then it is likely 
        //                         that the user's calibration file was deleted or 
        //                         corrupted, in which case the calibration will need to
        //                         be redone.
        //
        //  Return Value:
        //      CALIBRATIONEX_INVALID_INDEX    - The provided index could not be 
        //                                       opened.
        //      CALIBRATIONEX_INTERNAL_TIMEOUT - It is likely that Quick Glance is not 
        //                                       running.
        //      CALIBRATIONEX_OK               - The function completed successflly.
        //
        //  See Also:
        //      InitializeCalibrationEx()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "OpenCalibrationEx")]
        public static extern
            CalibrationErrorEx OpenCalibrationEx(
                uint CalibrationIndex);


        /*-----------------------------------------------------------------------------
        //  Name: GetClickingOptions()
        //
        //  Description:
        //      This Function gets the clicking related settings.
        //
        //  Arguments:
        //      Options - The options retrieved from the API. These options are only 
        //                valid if true is returned.
        //
        //  Return Value:
        //      If the function was successfull then true is returned and the data in 
        //      Options is valid. If unsuccessfull then false is returned.
        //
        //  See Also:
        //      SetClickingOptions()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetClickingOptions")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool GetClickingOptions(
            ref ClickingOptions Options);


        /*-----------------------------------------------------------------------------
        //  Name: GetCalibrationOptions()
        //
        //  Description:
        //      This Function gets the calibration related settings.
        //
        //  Arguments:
        //      Options - The options retrieved from the API. These options are only 
        //                valid if true is returned.
        //
        //  Return Value:
        //      If the function was successfull then true is returned and the data in 
        //      Options is valid. If unsuccessfull then false is returned.
        //
        //  See Also:
        //      SetCalibrationOptions()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetCalibrationOptions")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool GetCalibrationOptions(
            ref CalibrationOptions Options);


        /*-----------------------------------------------------------------------------
        //  Name: GetProcessingOptions()
        //
        //  Description:
        //      This Function gets the processing related settings.
        //
        //  Arguments:
        //      Options - The options retrieved from the API. These options are only 
        //                valid if true is returned.
        //
        //  Return Value:
        //      If the function was successfull then true is returned and the data in 
        //      Options is valid. If unsuccessfull then false is returned.
        //
        //  See Also:
        //      SetProcessingOptions()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetProcessingOptions")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool GetProcessingOptions(
            ref ProcessingOptions Options);


        /*-----------------------------------------------------------------------------
        //  Name: GetCameraOptions()
        //
        //  Description:
        //      This Function gets the camera related settings.
        //
        //  Arguments:
        //      Options - The options retrieved from the API. These options are only 
        //                valid if true is returned.
        //
        //  Return Value:
        //      If the function was successfull then true is returned and the data in 
        //      Options is valid. If unsuccessfull then false is returned.
        //
        //  See Also:
        //      SetCameraOptions()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetCameraOptions")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool GetCameraOptions(
            ref CameraOptions Options);


        /*-----------------------------------------------------------------------------
        //  Name: GetToolbarOptions()
        //
        //  Description:
        //      This Function gets the toolbar related settings.
        //
        //  Arguments:
        //      Options - The options retrieved from the API. These options are only 
        //                valid if true is returned.
        //
        //  Return Value:
        //      If the function was successfull then true is returned and the data in 
        //      Options is valid. If unsuccessfull then false is returned.
        //
        //  See Also:
        //      SetToolbarOptions()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetToolbarOptions")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool GetToolbarOptions(
            ref ToolbarOptions Options);


        /*-----------------------------------------------------------------------------
        //  Name: SetClickingOptions()
        //
        //  Description:
        //      This Function sets the clicking related settings.
        //
        //  Arguments:
        //      Options - The options being sent to the api.
        //
        //  Return Value:
        //      If the function was successfull then true is returned . If 
        //      unsuccessfull then false is returned and the new options did not take 
        //      effect.
        //
        //  See Also:
        //      GetClickingOptions()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "SetClickingOptions")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool SetClickingOptions(
                ClickingOptions Options);


        /*-----------------------------------------------------------------------------
        //  Name: SetCalibrationOptions()
        //
        //  Description:
        //      This Function sets the calibration related settings.
        //
        //  Arguments:
        //      Options - The options being sent to the api.
        //
        //  Return Value:
        //      If the function was successfull then true is returned . If 
        //      unsuccessfull then false is returned and the new options did not take 
        //      effect.
        //
        //  See Also:
        //      GetCalibrationOptions()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "SetCalibrationOptions")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool SetCalibrationOptions(
                CalibrationOptions Options);


        /*-----------------------------------------------------------------------------
        //  Name: SetProcessingOptions()
        //
        //  Description:
        //      This Function sets the processing related settings.
        //
        //  Arguments:
        //      Options - The options being sent to the api.
        //
        //  Return Value:
        //      If the function was successfull then true is returned . If 
        //      unsuccessfull then false is returned and the new options did not take 
        //      effect.
        //
        //  See Also:
        //      GetProcessingOptions()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "SetProcessingOptions")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool SetProcessingOptions(
                ProcessingOptions Options);


        /*-----------------------------------------------------------------------------
        //  Name: SetCameraOptions()
        //
        //  Description:
        //      This Function sets the camera related settings.
        //
        //  Arguments:
        //      Options - The options being sent to the api.
        //
        //  Return Value:
        //      If the function was successfull then true is returned . If 
        //      unsuccessfull then false is returned and the new options did not take 
        //      effect.
        //
        //  See Also:
        //      GetCameraOptions()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "SetCameraOptions")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool SetCameraOptions(
                CameraOptions Options);


        /*-----------------------------------------------------------------------------
        //  Name: SetToolbarOptions()
        //
        //  Description:
        //      This Function sets the toolbar related settings.
        //
        //  Arguments:
        //      Options - The options being sent to the api.
        //
        //  Return Value:
        //      If the function was successfull then true is returned . If 
        //      unsuccessfull then false is returned and the new options did not take 
        //      effect.
        //
        //  See Also:
        //      GetToolbarOptions()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "SetToolbarOptions")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool SetToolbarOptions(
                ToolbarOptions Options);


        /*-----------------------------------------------------------------------------
        //  Name: RegisterClickEvent()
        //
        //  Description:
        //      The function allows a window to be registered to receive the primary 
        //      and secondary click event. If no window handle is registered or if the 
        //      window handle is NULL then Quick Glance will perform its usual primary 
        //      or secondary click function. Likewise if NULL is passed in for either 
        //      the primary or secondary message then Quick Glance will perform its 
        //      normal function for the corresponding click. For example if the primary 
        //      message is NULL but the secondary message is not then the provided 
        //      window handle will only receive the secondary message and Quick Glance 
        //      will perform the primary click. The WPARAM argument for the message
        //      contains the x and y position of the click (x is the lo part and y is
        //      the hi part). The LPARAM argument is the timestamp of when the click
        //      occured.
        //
        //  Arguments:
        //      WindowHandle     - The handle to the window where the message will be 
        //                         posted. To unregister a previously registered window
        //                         make this argument NULL.
        //      PrimaryMessage   - The user-defined message to be posted when a primary 
        //                         click has been initiated by the eye tracker.
        //      SecondaryMessage - The user-defined message to be posted when a 
        //                         secondary click has been initiated by the eye tracker.
        */
        [DllImport(QuickLinkDllName, EntryPoint = "RegisterClickEvent")]
        public static extern
            void RegisterClickEvent(
                ref IntPtr WindowHandle,  // was void *
                cpp_ulong PrimaryMessage,
                cpp_ulong SecondaryMessage);


        /*-----------------------------------------------------------------------------
        //  Name: GetSerialNumber()
        //
        //  Description:
        //      This function retrieves the serial number from the camera connected to 
        //      the system. It is also usefull for determining if a camera is properly 
        //      installed on the system. Quick Glance does not need to be running for 
        //      this function to work successfully.
        //
        //  Arguments:
        //      SerialNumber - This value will receive the serial number of the camera.
        //
        //  Return Value:
        //      The success of the function. If false was returned then it is likely 
        //      that there is no camera connected to the system or that the drivers 
        //      have not been installed correctly.
        */
        [DllImport(QuickLinkDllName, EntryPoint = "GetSerialNumber")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool GetSerialNumber(
                out cpp_long SerialNumber);


        /*-----------------------------------------------------------------------------
        //  Name: SetCustomGPIO()
        //
        //  Description:
        //      This function sets the custom output values of the GPIO pins on the 
        //      camera. The GPIO outputs only use these values if the corresponding 
        //      GPIO in the CameraOptions is set to CAM_GPIO_OUT_CUSTOM. These outputs 
        //      will take effect the next time an image is acquired from the camera. 
        //
        //  Arguments:
        //      GPIO_1 - The custom output value for GPIO_1. If the value is 
        //               CAM_GPIO_OUT_VALUE_NO_CHANGE then the output will not be 
        //               affected.
        //      GPIO_2 - The custom output value for GPIO_2. If the value is 
        //               CAM_GPIO_OUT_VALUE_NO_CHANGE then the output will not be 
        //               affected.
        //      GPIO_3 - The custom output value for GPIO_3. If the value is 
        //               CAM_GPIO_OUT_VALUE_NO_CHANGE then the output will not be 
        //               affected.
        //
        //  Return Value:
        //      The success of the function.
        //
        //  See Also:
        //      SetCameraOptions()
        //      GetCameraOptions()
        */
        [DllImport(QuickLinkDllName, EntryPoint = "SetCustomGPIO")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern
            bool SetCustomGPIO(
                CameraCustomGPIOOutputValue GPIO_1,
                CameraCustomGPIOOutputValue GPIO_2,
                CameraCustomGPIOOutputValue GPIO_3);

    }
}
