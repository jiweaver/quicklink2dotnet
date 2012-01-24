#region License

/* QuickLink2DotNet : A .NET wrapper (in C#) for EyeTech's QuickLink2 API.
 *
 * Copyright (c) 2011 Justin Weaver
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

/* $Id: QuickLink2API.cs 38 2011-05-09 01:07:39Z piranther $
 *
 * Description: A .NET wrapper for EyeTech's QuickLink2 API.
 *
 * This wrapper requires that you place the QuickLink2.dlls in the same
 * directory as your program executable.  You can download QuickLink2 from
 * http://www.eyetechds.com/support/downloads
 *
 * The extensive inline documentation has been cut & pasted from the
 * QLTypes.h and QuickLink2.h C++ header files for convenient reference. Those
 * original files are Copyright (C) 1996 - 2012 EyeTech Digital Systems.
 */

#endregion Header Comments

using System;
using System.Runtime.InteropServices;

namespace QuickLink2DotNet
{
    #region QuickLink2 Setting Strings

    public static class QL_SETTINGS
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @ingroup Settings
        /// @{
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_BANDWIDTH_PERCENT_FULL
        ///
        /// @brief  The percentage of the bus bandwidth used by the device when
        /// searching for eyes. Possible values range from 1-100.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_BANDWIDTH_PERCENT_FULL = "DeviceBandwidthPercentFull";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_BANDWIDTH_PERCENT_TRACKING
        ///
        /// @brief  The percentage of the bus bandwidth used by the device when eyes
        /// have been found and are being tracked. Possible values range from 1-100.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_BANDWIDTH_PERCENT_TRACKING = "DeviceBandwidthPercentTracking";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_BINNING
        ///
        /// @brief  The number of pixels to combine in the x and y direction.
        ///
        /// The outputted pixel values are the average of the combined pixels.
        /// Possible values are 1, 2, and 4. A value of 1 outputs one pixel of data
        /// for each pixel on the image sensor. A value of 2 outputs one pixel of
        /// data for every 4 (2 X 2) pixels on the image sensor. A value of 4 outputs
        /// one pixel of data for every 16 (4 X 4) pixels on the image sensor.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_BINNING = "DeviceBinning";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_EXPOSURE
        ///
        /// @brief  The exposure time in milliseconds for each frame. Possible values
        /// range from 1-50 ms.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_EXPOSURE = "DeviceExposure";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_FLIP_X
        ///
        /// @brief  The horizontal direction of the image.
        ///
        /// Possible values are true and false. A value of false will result in the
        /// right eye being closest to the origin (0, 0) of the image. A value of
        /// true will mirror the image and cause the left eye to be closest to the
        /// origin.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_FLIP_X = "DeviceFlipX";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_FLIP_Y
        ///
        /// @brief  The vertical direction of the image.
        ///
        /// Possible values are true and false. A value of false will result in the
        /// top of the head being closest to the origin (0, 0) of the image. A value
        /// of true will cause the bottom of the face to be closest to the origin of
        /// the image.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_FLIP_Y = "DeviceFlipY";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_DISTANCE
        ///
        /// @brief  The distance from the user to the device in centimeters.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_DISTANCE = "DeviceDistance";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_LENS_FOCAL_LENGTH
        ///
        /// @brief  The focal length of the lens in centimeters.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_LENS_FOCAL_LENGTH = "DeviceLensFocalLength";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_GAIN_MODE
        ///
        /// @brief  The gain mode the device will use.
        ///
        /// @see QLDeviceGainMode.
        /// @see QL_SETTING_DEVICE_GAIN_VALUE.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_GAIN_MODE = "DeviceGainMode";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_GAIN_VALUE
        ///
        /// @brief  The gain value the device will use when the DeviceGainMode is set
        /// to QL_DEVICE_GAIN_MODE_MANUAL.
        ///
        /// @see DeviceGainMode.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_GAIN_VALUE = "DeviceGainValue";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_X
        ///
        /// @brief  The horizontal distance in percentage of the image width that
        /// either eye can be from the left or right edge of the region of interest
        /// before the region of interest will move and try to re-center the eyes.
        ///
        /// Possible values range from 1-100.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_X = "DeviceRoiMoveThresholdPercentX";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_Y
        ///
        /// @brief  The vertical distance in percentage of the image height that
        /// either eye can be from the top or bottom edge of the region of interest
        /// before the region of interest will move and try to re-center the eyes.
        ///
        /// Possible values range from 1-100.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_Y = "DeviceRoiMoveThresholdPercentY";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_ROI_SIZE_PERCENT_X
        ///
        /// @brief  The width of the region of interest in percentage of the
        /// horizontal sensor size when the eyes are being tracked.
        ///
        /// Possible values range from 1-100.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_ROI_SIZE_PERCENT_X = "DeviceRoiSizePercentX";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_ROI_SIZE_PERCENT_Y
        ///
        /// @brief  The height of the region of interest in percentage of the
        /// vertical sensor size when the eyes
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_ROI_SIZE_PERCENT_Y = "DeviceRoiSizePercentY";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_GAZE_POINT_FILTER_MODE
        ///
        /// @brief  The filter mode for the outputted gaze point.
        ///
        /// @see QLDeviceGazePointFilterMode.
        /// @see QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_GAZE_POINT_FILTER_MODE = "DeviceGazePointFilterMode";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE
        ///
        /// @brief  The value used for the filtering mode.
        ///
        /// @see QLDeviceGazePointFilterMode.
        /// @see QL_SETTING_DEVICE_GAZE_POINT_FILTER_MODE.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE = "DeviceGazePointFilterValue";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_IMAGE_PROCESSING_EYES_TO_FIND
        ///
        /// @brief  The search setting for the image processing.
        ///
        /// @see QLDeviceEyesToUse.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_IMAGE_PROCESSING_EYES_TO_FIND = "DeviceImageProcessingEyesToUse";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_INTERPOLATE_ENABLED
        ///
        /// @brief  The flag for disabling/enabling final gaze point interpolation.
        ///
        /// Possible values are true and false. If false then the final gaze point
        /// will not be interpolated and the outputted gaze point will not be valid.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_INTERPOLATE_ENABLED = "DeviceInterpolateEnabled";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_IMAGE_PROCESSING_ENABLED
        ///
        /// @brief  The flag for disabling/enabling image processing.
        ///
        /// Possible values are true and false. If false then the eyes will not be
        /// searched for and the outputted eye information will not be valid.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_IMAGE_PROCESSING_ENABLED = "DeviceImageProcessingEnabled";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_CALIBRATE_ENABLED
        ///
        /// @brief  The flag for disabling/enabling calibration.
        ///
        /// Possible values are true and false. If false then calibration data
        /// collection will be disabled and new calibrations can not be performed.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_CALIBRATE_ENABLED = "DeviceCalibrateEnabled";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT
        ///
        /// @brief  The radius of the cornea of the left eye in centimeters. This
        /// radius can be calculated by calling the function
        /// QLDevice_CalibrateEyeRadius(). This radius will affect the calculated
        /// distance value that is outputted for each frame.
        ///
        /// @see QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_RIGHT.
        /// @see QLFrameData.
        /// @see QLDevice_CalibrateEyeRadius().
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT = "DeviceImageProcessingEyeRadiusLeft";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @def    QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_RIGHT
        ///
        /// @brief  The radius of the cornea of the right eye in centimeters. This
        /// radius can be calculated by calling the function
        /// QLDevice_CalibrateEyeRadius(). This radius will affect the calculated
        /// distance value that is outputted for each frame.
        ///
        /// @see QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT.
        /// @see QLFrameData.
        /// @see QLDevice_CalibrateEyeRadius().
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_RIGHT = "DeviceImageProcessingEyeRadiusRight";
    }

    #endregion QuickLink2 Setting Strings

    #region QuickLink2 API Constants

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @enum   QLError
    ///
    /// @brief  Error codes returned from Quick Link 2 functions.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum QLError
    {
        /// The function successfully completed.
        QL_ERROR_OK = 0,

        /// The inputted device ID is invalid.
        QL_ERROR_INVALID_DEVICE_ID = 1,

        /// The inputted settings ID is invalid.
        QL_ERROR_INVALID_SETTINGS_ID = 2,

        /// The inputted calibration ID is invalid.
        QL_ERROR_INVALID_CALIBRATION_ID = 3,

        /// The inputted target ID is invalid.
        QL_ERROR_INVALID_TARGET_ID = 4,

        /// The password for the device is not valid.
        QL_ERROR_INVALID_PASSWORD = 5,

        /// The inputted path is invalid.
        QL_ERROR_INVALID_PATH = 6,

        /// The inputted duration is invalid.
        QL_ERROR_INVALID_DURATION = 7,

        /// The inputted pointer is NULL.
        QL_ERROR_INVALID_POINTER = 8,

        /// The timeout time was reached.
        QL_ERROR_TIMEOUT_ELAPSED = 9,

        /// An internal error occurred. Contact EyeTech for further help.
        QL_ERROR_INTERNAL_ERROR = 10,

        /// The inputted buffer is is not large enough.
        QL_ERROR_BUFFER_TOO_SMALL = 11,

        /// The calibration container has not been initialized. See QLCalibration_Initialize()
        QL_ERROR_CALIBRAION_NOT_INITIALIZED = 12,

        /// The device has not been started. See QLDevice_Start()
        QL_ERROR_DEVICE_NOT_STARTED = 13,

        /// The setting is not supported by the given device. See QLDevice_IsSettingSupported()
        QL_ERROR_NOT_SUPPORTED = 14,

        /// The setting is not in the settings container.
        QL_ERROR_NOT_FOUND = 15,

        /// The API has been loaded by an unauthorized application. This is only used for specialty builds of the API
        QL_ERROR_APPLICATION_NOT_AUTHORIZED = 16
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @enum   QLDeviceStatus
    ///
    /// @brief  Status values for a device.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum QLDeviceStatus
    {
        /// The device has probably been detached from the computer.
        QL_DEVICE_STATUS_UNAVAILABLE = 0,

        /// The device is stopped.
        QL_DEVICE_STATUS_STOPPED = 1,

        /// The device is in the process of being started.
        QL_DEVICE_STATUS_INITIALIZED = 2,

        /// The device is running.
        QL_DEVICE_STATUS_STARTED = 3
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @enum   QLIndicatorType
    ///
    /// @brief  Values that identify an indicator light.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum QLIndicatorType
    {
        /// The left indicator light.
        QL_INDICATOR_TYPE_LEFT = 0,

        /// The right indicator light.
        QL_INDICATOR_TYPE_RIGHT = 1
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @enum   QLIndicatorMode
    ///
    /// @brief  Values that identify different indicator modes.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum QLIndicatorMode
    {
        /// The indicator will be off constantly.
        QL_INDICATOR_MODE_OFF = 0,

        /// The indicator will be on constantly.
        QL_INDICATOR_MODE_ON = 1,

        /// The indicator will show the current tracking status of the left eye.
        QL_INDICATOR_MODE_LEFT_EYE_STATUS = 2,

        /// The indicator will show the current tracking status of the right eye .
        QL_INDICATOR_MODE_RIGHT_EYE_STATUS = 3,

        /// The indicator will show the filtered current tracking status of the left eye. This will prevent the indicator from flickering if the eye was lost for only a couple of frames.
        QL_INDICATOR_MODE_LEFT_EYE_STATUS_FILTERED = 4,

        /// The indicator will show the filtered current tracking status of the right eye. This will prevent the indicator from flickering if the eye was lost for only a couple of frames.
        QL_INDICATOR_MODE_RIGHT_EYE_STATUS_FILTERED = 5
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @enum   QLCalibrationType
    ///
    /// @brief  Values that identify different calibration modes.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum QLCalibrationType
    {
        /// A five point calibration.
        QL_CALIBRATION_TYPE_5 = 0,

        /// A nine point calibration.
        QL_CALIBRATION_TYPE_9 = 1,

        /// A sixteen point calibration.
        QL_CALIBRATION_TYPE_16 = 2
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @enum   QLCalibrationStatus
    ///
    /// @brief  Status values for a calibration target
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum QLCalibrationStatus
    {
        /// The calibration target has data for both the left and right eyes.
        QL_CALIBRATION_STATUS_OK = 0,

        /// The calibration target is in the process of calibrating.
        QL_CALIBRATION_STATUS_CALIBRATING = 1,

        /// The calibration target has data for the right eye but not the left eye.
        QL_CALIBRATION_STATUS_NO_LEFT_DATA = 2,

        /// The calibration target has data for the left eye but not the right eye.
        QL_CALIBRATION_STATUS_NO_RIGHT_DATA = 3,

        // The calibration target does not have data for the left or right eye.
        QL_CALIBRATION_STATUS_NO_DATA = 4
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @enum   QLEyeType
    ///
    /// @brief  Values that identify an eye.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum QLEyeType
    {
        /// The left eye.
        QL_EYE_TYPE_LEFT = 0,

        /// The right eye.
        QL_EYE_TYPE_RIGHT = 1
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @enum   QLSettingType
    ///
    /// @brief  Values that identify what data type a pointer points to.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum QLSettingType
    {
        /// A c/c++ int type
        QL_SETTING_TYPE_INT = 0,

        /// An 8-bit wide integer type
        QL_SETTING_TYPE_INT8 = 1,

        /// A 16-bit wide integer type
        QL_SETTING_TYPE_INT16 = 2,

        /// A 32-bit wide integer type
        QL_SETTING_TYPE_INT32 = 3,

        /// A 64-bit wide integer type
        QL_SETTING_TYPE_INT64 = 4,

        /// A c/c++ unsigned int type
        QL_SETTING_TYPE_UINT = 5,

        /// An 8-bit wide unsigned integer type
        QL_SETTING_TYPE_UINT8 = 6,

        /// An 16-bit wide unsigned integer type
        QL_SETTING_TYPE_UINT16 = 7,

        /// An 32-bit wide unsigned integer type
        QL_SETTING_TYPE_UINT32 = 8,

        /// An 64-bit wide unsigned integer type
        QL_SETTING_TYPE_UINT64 = 9,

        /// A c/c++ float type
        QL_SETTING_TYPE_FLOAT = 10,

        /// A c/c++ double type
        QL_SETTING_TYPE_DOUBLE = 11,

        /// A c/c++ bool type
        QL_SETTING_TYPE_BOOL = 12,

        /// A c/c++ char* type
        QL_SETTING_TYPE_STRING = 13
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @enum   QLDeviceEyesToUse
    ///
    /// @brief  Values that identify which eyes the device should process.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum QLDeviceEyesToUse
    {
        /// Only search for and process the left eye
        QL_DEVICE_EYES_TO_USE_LEFT = 0,

        /// Only search for and process the right eye
        QL_DEVICE_EYES_TO_USE_RIGHT = 1,

        /// Search for and process both eyes. If only one eye is found then use it for calculating the weighted gaze point.
        QL_DEVICE_EYES_TO_USE_LEFT_OR_RIGHT = 2,

        /// Search for and process both eyes. Both eyes must be found for calculating the weighted gaze point.
        QL_DEVICE_EYES_TO_USE_LEFT_AND_RIGHT = 3
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @enum   QLDeviceGainMode
    ///
    /// @brief  Values that identify which gain mode to use.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum QLDeviceGainMode
    {
        /// The device will automatically adjust the vain value.
        QL_DEVICE_GAIN_MODE_AUTO = 0,

        /// The device will use a fixed gain value.
        QL_DEVICE_GAIN_MODE_MANUAL = 1
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @enum   QLDeviceGazePointFilterMode
    ///
    /// @brief  Values that identify which gain mode to use.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum QLDeviceGazePointFilterMode
    {
        /// No gaze point filtering will be done.
        QL_DEVICE_GAZE_POINT_FILTER_NONE = 0,

        /// The median gaze point value over the last X number of frames, where X
        /// equals the value represented by the setting
        /// @ref QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE.
        /// This will produce a gaze
        /// point latency time of about (X / fps / 2) seconds.
        QL_DEVICE_GAZE_POINT_FILTER_MEDIAN_FRAMES = 1,

        /// The median gaze point value over the last X number of frames, where X
        /// is the number of frames gathered over twice the amount of milliseconds
        /// represented by the setting
        /// latency of about
        /// @ref QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE milliseconds.
        QL_DEVICE_GAZE_POINT_FILTER_MEDIAN_TIME = 2,

        /// The heuristic filter uses different filtering strengths when the eye is
        /// moving and when it is fixating. When the eye is moving, very little
        /// filtering is done which results in very low latency. When the eye is
        /// fixating, large amounts of filtering are being done which greatly reduce
        /// the amount of jitter. Durring fixation, filtering is done over the last
        /// X number of frames where X equals the value represented by the setting
        /// @ref QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE.
        QL_DEVICE_GAZE_POINT_FILTER_HEURISTIC_FRAMES = 3,

        /// The heuristic filter uses different filtering strengths when the eye is
        /// moving and when it is fixating. When the eye is moving, very little
        /// filtering is done which results in very low latency. When the eye is
        /// fixating, large amounts of filtering are being done which greatly reduce
        /// the amount of jitter. Durring fixation, filtering is done over the last
        /// X number of frames where X equals the number of frames gathered over
        /// twice the amount of milliseconds represented by the setting
        /// @ref QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE. This produces aproximatly
        /// the same amount of latency durring fixation for all frame rates
        QL_DEVICE_GAZE_POINT_FILTER_HEURISTIC_TIME = 4,

        /// The weighted previous frame mode filters the gaze point by summing the
        /// weighted current gaze point location and the weighted previous gazepoint
        /// location. The weights are based on the distance the current gaze point is
        /// away from the previous gaze point. The larger the distance, the greater the
        /// weight on the current gaze point. The smaller the distance, the greater the
        /// weight on the previous gaze point. This results in very low latency when
        /// the eye is moving and very low jitter when the eye is fixating. The value
        /// represented by the setting
        /// @ref QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE affects the rate at which
        /// the weighting changes from the previous gaze point to the current gaze point.
        /// Possible values range between 0 and 100.
        QL_DEVICE_GAZE_POINT_FILTER_WEIGHTED_PREVIOUS_FRAME = 5
    }

    #endregion QuickLink2 API Constants

    #region QuickLink2 API Data Structures

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @struct   QLCalibrationTarget
    ///
    /// @brief  Data that identifies a calibration target.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct QLCalibrationTarget
    {
        /// An identifying value used to reference the target. This is unique for each target in a given calibration container.
        public System.Int32 targetId;

        /// The x position of the target. This is in percentage of the horizontal area to be calibrated(0.-100.)
        public System.Single x;

        /// The y position of the target. This is in percentage of the vertical area to be calibrated(0.-100.)
        public System.Single y;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @struct   QLCalibrationScore
    ///
    /// @brief  Values that identify the quality of a calibration at a target.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct QLCalibrationScore
    {
        /// The x offset from the target position for the calibrated gaze point. This is in percentage of the horizontal area to be calibrated(0.-100.)
        public System.Single x;

        /// The y offset from the target position for the calibrated gaze point. This is in percentage of the vertical area to be calibrated(0.-100.)
        public System.Single y;

        /// The magnitude of the distance from the calibrated gaze point to the actual target position. This can be used to identify which target has the worst calibration.
        public System.Single score;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @struct   QLDeviceInfo
    ///
    /// @brief  Device specific information
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct QLDeviceInfo
    {
        /// The actual sensor width in pixels.
        public System.Int32 sensorWidth;

        /// The actual sensor height in pixels.
        public System.Int32 sensorHeight;

        /// The serial unique serial number of the device.
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public System.String serialNumber;

        /// The EyeTech model name of the camera.
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public System.String modelName;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @struct   QLXYPairDouble
    ///
    /// @brief  An x-y pair of type double.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct QLXYPairDouble
    {
        public System.Double x;
        public System.Double y;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @struct   QLXYPairFloat
    ///
    /// @brief  An x-y pair of type float.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct QLXYPairFloat
    {
        public System.Single x;
        public System.Single y;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @struct   QLImageData
    ///
    /// @brief  Information/data about an image
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct QLImageData
    {
        /// The pixel data of the image. The data is single channel grey-scale with 8-bits per pixel.
        public IntPtr PixelData;

        /// The width in pixels of the image. This can be different than the
        /// sensorWidth of the device if binning is turned on.
        public System.Int32 Width;

        /// The height in pixels of the image. This can be different than the
        /// sensorHeight of the device if binning is turned on.
        public System.Int32 Height;

        /// The timestamp of the frame. It is the number of milliseconds from when
        /// the computer was started.
        public System.Double Timestamp;

        /// The gain value of the image.
        public System.Int32 Gain;

        ///  A number to identify a frame. Checking for non consecutive numbers
        /// from frame to frame can determine if a frame was lost.
        public System.Int32 FrameNumber;

        // This was any array of 16 void pointers. -Justin
#if (ISX64)
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
#else
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
#endif
        public System.String Reserved;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @struct   QLEyeData
    ///
    /// @brief  Eye specific data.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct QLEyeData
    {
        /// Indicates whether the eye is found in the image. If this is false
        /// then the rest of the data for the eye is not valid.
        [MarshalAs(UnmanagedType.U1)]
        public System.Boolean Found;

        /// Indicates whether the eye is calibrated. If this is false then the
        /// GazePoint is not valid for the eye.
        [MarshalAs(UnmanagedType.U1)]
        public System.Boolean Calibrated;

        /// The diameter of the pupil in millimeters. The resolution of this
        /// measurement is .02mm. If the eye is found and the pupil diameter is
        /// less than zero then the diameter could not be determined and it's
        /// value should not be used for that frame.
        public System.Single PupilDiameter;

        /// The position in pixels of the image of the center of the pupil.
        public QLXYPairFloat Pupil;

        /// The position in pixels of the image of the center of one of the glints.
        /// Compare the x value to determine if this is the left of right glint.
        public QLXYPairFloat Glint0;

        /// The position in pixels of the image of the center of one of the glints.
        /// Compare the x value to determine if this is the left of right glint.
        public QLXYPairFloat Glint1;

        /// The position of the gaze point in percentage of the calibrated area. This
        /// value is not constrained to the calibrated area.
        public QLXYPairFloat GazePoint;

        // This was any array of 16 void pointers. -Justin
#if (ISX64)
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
#else
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
#endif
        public System.String Reserved;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @struct   QLWeightedGazePoint
    ///
    /// @brief The averaged gaze point. These values intelligently average the
    /// left and right eye based on which eyes were found and previous data
    /// depending on filtering settings.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct QLWeightedGazePoint
    {
        /// Indicates whether the weighted gaze point is available this frame. If
        /// this is false then the rest of the data is invalid.
        [MarshalAs(UnmanagedType.U1)]
        public System.Boolean Valid;

        /// The x position of the gaze point in percentage of the calibrated area.
        public System.Single x;

        /// The y position of the gaze point in percentage of the calibrated area.
        public System.Single y;

        /// The amount the left eye affected the weighted gaze point.
        public System.Single LeftWeight;

        /// The amount the right eye affected the weighted gaze point.
        public System.Single RightWeight;

        // This was any array of 16 void pointers. -Justin
#if (ISX64)
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
#else
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
#endif
        public System.String Reserved;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// @struct   QLFrameData
    ///
    /// @brief The complete data available in each frame.
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct QLFrameData
    {
        /// The image specific data.
        public QLImageData ImageData;

        /// Left eye specific data.
        public QLEyeData LeftEye;

        /// Right eye specific data.
        public QLEyeData RightEye;

        /// An intelligently averaged single gaze point that is the best
        /// representation of the user's gaze.
        public QLWeightedGazePoint WeightedGazePoint;

        /// The focus measurement of the eyes in the image. Higher values are
        /// better. Typical good values range between 15 and 19.
        public System.Single Focus;

        /// The distance from the device to the user in centimeters.
        public System.Single Distance;

        // This was any array of 16 void pointers. -Justin
#if (ISX64)
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
#else
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
#endif
        public System.String Reserved;
    }

    #endregion QuickLink2 API Data Structures

    #region QuickLink2 API Operations

    public static class QuickLink2API
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLAPI_ExportSettings( QLSettingsId settings)
        ///
        /// @ingroup API
        /// @brief  Export the system level API settings that are currently being used
        /// by the API.
        ///
        /// This function exports the current system level API settings values to the
        /// specified settings container. It will only export the values for setting
        /// that have been added to the settings container. To add a setting to the
        /// settings container use the function QLSettings_AddSetting() or the
        /// function QLSettings_SetValue().
        ///
        /// @param  [in] settings The ID of the settings container that will receive
        ///                       the exported values from the API. This ID is obtained
        ///                       by calling either the function QLSettings_Create() or
        ///                       the function QLSettings_Load().
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the settings values were successfully exported.
        ///
        /// @see Settings
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLAPI_ExportSettings", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLAPI_ExportSettings(
               System.Int32 settingsID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLAPI_ImportSettings( QLSettingsId settings)
        ///
        /// @ingroup API
        /// @brief  Import settings values to the system level of the API.
        ///
        /// This function imports settings values to the system level of the API from the
        /// specified settings container. The new settings values will take effect
        /// immediately.
        ///
        /// Not all settings are supported at the system level of the API. Only
        /// settings values that are supported will be imported from the settings
        /// container. All other settings in the container will be ignored.
        ///
        /// @param  [in] settings The ID of the settings container that will supply the
        ///                       settings values that will be imported to the API.
        ///                       This ID is obtained by calling either the function
        ///                       QLSettings_Create() or the function
        ///                       QLSettings_Load().
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the settings values were successfully imported to API.
        ///
        /// @see Settings
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLAPI_ImportSettings", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLAPI_ImportSettings(
               System.Int32 settingsID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QLDevice_Enumerate( int* numDevices,
        /// QLDeviceId* deviceBuffer)
        ///
        /// @ingroup Device
        /// @brief  Enumerate the bus to find connected eye trackers.
        ///
        /// This function creates an ID for each eye tracker connected to the system.
        /// The IDs are used in other functions to reference a specific eye tracker.
        ///
        /// @param [in,out] numDevices      A pointer to an integer containing the
        ///                                 size of deviceBuffer. When the function
        ///                                 returns the integer contains the number of
        ///                                 devices found on the bus.
        /// @param [out] deviceBuffer       A pointer to a QLDeviceId buffer that
        ///                                 will receive the IDs of the devices that
        ///                                 are connected to the system.
        ///
        /// @return The success of the function. If the return value is
        /// QL_ERROR_BUFFER_TOO_SMALL then numDevices contains the number of eye
        /// trackers that were detected on the system. The deviceBuffer should be
        /// resized to be at least as large as numDevices before this function is
        /// called again.
        ///
        /// @example /src/QuickStart/Initialize.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_Enumerate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_Enumerate(
               ref System.Int32 numDevices,
               [In, Out] System.Int32[] deviceBuffer);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_GetInfo( QLDeviceId device,
        /// QLDeviceInfo* info)
        ///
        /// @ingroup Device
        /// @brief  Get device specific information for a device.
        ///
        /// @param  [in] device     The ID of the device from which to get
        ///                         information. This ID is obtained by calling the
        ///                         function QLDevice_Enumerate().
        /// @param [out] info    A pointer to a QLDeviceInfo object. This object
        ///                         will receive the information about the device.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the info parameter contains the information about the device.
        ///
        /// @example /src/QuickStart/Initialize.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_GetInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_GetInfo(
               System.Int32 deviceID,
               out QLDeviceInfo info);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_GetStatus( QLDeviceId device,
        /// QLDeviceStatus* status)
        ///
        /// @ingroup Device
        /// @brief  Get the status of a device.
        ///
        /// @param  [in] device     The ID of the device from which to get the
        ///                         status. This ID is obtained by calling the
        ///                         function QLDevice_Enumerate().
        /// @param [out] status  A pointer to a QLDeviceStatus object. This object
        ///                         will receive the status of the device.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the status parameter contains the status of the device.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_GetStatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_GetStatus(
               System.Int32 deviceID,
               ref QLDeviceStatus status);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_ExportSettings( QLDeviceId device,
        /// QLSettingsId settings)
        ///
        /// @ingroup Device
        /// @brief  Export the active settings values that are being used by a device.
        ///
        /// This function exports the settings values that are currently being used
        /// by the specified device. The values are exported to the specified
        /// settings container. It will only export the values for setting that have
        /// been added to the settings container. To add a setting to the settings
        /// container use the function QLSettings_AddSetting() or the function
        /// QLSettings_SetValue().
        ///
        /// Not all settings are supported by all devices. To determine if a setting
        /// is supported by a particular device then use the function
        /// QLDevice_IsSettingSupported().
        ///
        /// @param  [in] device   The ID of the device from which to export the
        ///                       settings. This ID is obtained by calling the function
        ///                       QLDevice_Enumerate().
        /// @param  [in] settings The ID of the settings container that will receive
        ///                       the exported values from the device. This ID is
        ///                       obtained by calling either the function
        ///                       QLSettings_Create() or the function
        ///                       QLSettings_Load().
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the settings values were successfully exported from the device.
        ///
        /// @see Settings
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_ExportSettings", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLDevice_ExportSettings(
               System.Int32 deviceID,
               System.Int32 settingsID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_ImportSettings( QLDeviceId device,
        /// QLSettingsId settings)
        ///
        /// @ingroup Device
        /// @brief  Import settings values for a device and make them active.
        ///
        /// This function imports settings values for the specified device from the
        /// specified settings container. This operation is synchronous with the
        /// device. The new settings values will take effect after the device has
        /// finished processing its current frame, resulting in a latency of 1 frame.
        ///
        /// Not all settings are supported by all devices. Only settings values that
        /// are supported by the specified device will be imported from the settings
        /// container. All other settings in the settings container will be ignored.
        /// To determine if a setting is supported by a particular device then use
        /// the function QLDevice_IsSettingSupported().
        ///
        /// @param  [in] device   The ID of the device in which to import the settings.
        ///                       This device ID is obtained by calling the function
        ///                       QLDevice_Enumerate().
        /// @param  [in] settings The ID of the settings container that will supply the
        ///                       settings values that will be imported to the device.
        ///                       This ID is obtained by calling either the function
        ///                       QLSettings_Create() or the function
        ///                       QLSettings_Load().
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the settings values were successfully imported to the device.
        ///
        /// @see Settings
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_ImportSettings", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_ImportSettings(
               System.Int32 deviceID,
               System.Int32 settingsID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_IsSettingSupported( QLDeviceId device,
        /// const char* settingName)
        ///
        /// @ingroup Device
        /// @brief  Determine if a setting is supported by a device.
        ///
        /// This function determines if a setting is supported by a given device. Not
        /// all settings are supported by all devices.
        ///
        /// @param  [in] device      The ID of the device to check for setting support.
        ///                          This ID is obtained by calling the function
        ///                          QLDevice_Enumerate().
        /// @param  [in] settingName A pointer to a NULL-terminated string containing the
        ///                          name of the setting to check for in the device.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the setting is supported by the specified device.
        ///
        /// @see Settings
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_IsSettingSupported", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLDevice_IsSettingSupported(
                System.Int32 deviceID,
                [In] System.String settingName);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_SetPassword( QLDeviceId device,
        /// const char* password)
        ///
        /// @ingroup Device
        /// @brief  Set the password for a device.
        ///
        /// This function sets the password for the specified device. The password is
        /// usually found on a label affixed to the device. If the password is not
        /// known for a device then contact EyeTech Digital Systems to obtain it. The
        /// password must be set for most functions to work properly. If the password
        /// is not set then the functions QLDevice_Start(), QLDevice_GetFrame(),
        /// QLDevice_GetStatus() and QLCalibration_Initialize() will return the value
        /// QL_ERROR_INVALID_PASSWORD.
        ///
        /// @param  [in] device   The ID of the device for which to set the password.
        ///                       This ID is obtained by calling the function
        ///                       QLDevice_Enumerate().
        /// @param  [in] password A pointer to a NULL-terminated string containing the
        ///                       password for the device.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the password was valid and stored in the device.
        ///
        /// @example /src/QuickStart/Initialize.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_SetPassword", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLDevice_SetPassword(
                System.Int32 deviceID,
                [In] System.String password);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_Start( QLDeviceId device)
        ///
        /// @ingroup Device
        /// @brief  Start the device.
        ///
        /// This function causes the device to start running. Once running, the
        /// device will grab and process frames as fast as the current settings
        /// allow. Calling this function on a device that has already been started
        /// will not restart the device. QLDevice_Stop() must be called before a
        /// device can be restarted.
        ///
        /// @param  [in] device  The ID of the device to start. This ID is obtained by
        ///                      calling the function QLDevice_Enumerate().
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the device was started successfully.
        ///
        /// @example /src/QuickStart/Main.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_Start", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_Start(
               System.Int32 deviceID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_Stop( QLDeviceId device)
        ///
        /// @ingroup Device
        /// @brief  Stop the device.
        ///
        /// This function causes the device to stop running. Before unloading the
        /// library, this function must be called for the for each device that has
        /// been started using the function QLDevice_Start(). Undefined behavior
        /// could result if the library is unloaded before a device is stopped.
        ///
        /// It is best if this function is called before a process has been signaled
        /// to exit. This will give a device sufficient time to clean up its memory
        /// and close its thread before the process exit procedure unloads the
        /// library automatically.
        ///
        /// @param  [in] device  The ID of the device to stop. This ID is obtained by
        ///                      calling the function QLDevice_Enumerate().
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the device was stopped successfully and all system resources used by
        /// the device have been closed.
        ///
        /// @example /src/QuickStart/Main.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_Stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_Stop(
               System.Int32 deviceID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_SetIndicator( QLDeviceId device,
        /// QLIndicatorType type, QLIndicatorMode mode)
        ///
        /// @ingroup Device
        /// @brief  Set the indicator mode for the device.
        ///
        /// This function sets the mode for the indicator lights on the front of the
        /// device.
        ///
        /// @param  [in] device  The ID of the device for which to set the indicator mode.
        ///                      This ID is obtained by calling the function
        ///                      QLDevice_Enumerate().
        /// @param  [in] type    The type of indicator to set.
        /// @param  [in] mode    The mode to set the indicator.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the indicator was set to the desired mode for the specified device.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_SetIndicator", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_SetIndicator(
               System.Int32 deviceID,
               QLIndicatorType type,
               QLIndicatorMode mode);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_GetIndicator( QLDeviceId device,
        /// QLIndicatorType type, QLIndicatorMode* mode)
        ///
        /// @ingroup Device
        /// @brief  Get the current indicator mode for the device.
        ///
        /// This function gets the current mode for the indicator lights on the front
        /// of the device.
        ///
        /// @param [in] device      The ID of the device from which to get the
        ///                         indicator mode. This ID is obtained by calling
        ///                         the function QLDevice_Enumerate().
        /// @param [in] type        The type of indicator to get.
        /// @param [out] mode       A pointer to a QLIndicatorMode object. This
        ///                         object will receive the mode of the indicator
        ///                         type for the specified device.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the indicator mode was retrieved from the specified device.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_GetIndicator", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_GetIndicator(
               System.Int32 deviceID,
               QLIndicatorType type,
               ref QLIndicatorMode mode);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_GetFrame( QLDeviceId device,
        /// int waitTime, QLFrameData* frame)
        ///
        /// @ingroup Device
        /// @brief  Get a frame from the device
        ///
        /// This function gets the most recent frame from the device. It is a
        /// blocking function which will wait for a frame to become available if
        /// there is not one ready. Waiting does not use CPU time.
        ///
        /// Only the most recent frame can be retrieved from the device. To ensure
        /// that no frames are dropped this function needs to be called at least as
        /// fast as the frame rate of the device. A good way to use this function
        /// that will help ensure the retrieval of every frame is to use it in a
        /// loop. The blocking nature of this function will ensure that the loop will
        /// only run as fast as the device can produce frames. If other processing in
        /// the loop does not exceed the time that it takes for the device to make
        /// another frame available then this function will sync the loop to the
        /// approximate frame rate of the device.
        ///
        /// @param [in] device      The ID of the device from which to get the most
        ///                         recent frame. This ID is obtained by calling the
        ///                         function QLDevice_Enumerate().
        /// @param [in] waitTime    The maximum time to wait for a new frame.
        /// @param [out] frame      A pointer to a QLFrameData object. This object
        ///                         receives the data for the most recent frame.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the most recent frame was successfully retrieved from the device.
        ///
        /// @example /src/QuickStart/Main.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_GetFrame", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_GetFrame(
               System.Int32 deviceID,
               System.Int32 waitTime,
               ref QLFrameData frame);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_ApplyCalibration( QLDeviceId device,
        /// QLCalibrationId calibration)
        ///
        /// @ingroup Device
        /// @brief  Apply a calibration to a device.
        ///
        /// This function applies a calibration to a device. If a calibration is not
        /// applied to a device then the gaze point can not be calculated. By
        /// applying a calibration object that has not been calibrated it is possible
        /// to clear a calibration from a device so it will be un-calibrated.
        ///
        /// For greatest accuracy a device should be calibrated for each user, for
        /// each physical setup of the device and monitor.
        ///
        /// @param [in] device      The ID of the device for which to apply the
        ///                         calibration. This ID is obtained by calling the
        ///                         function QLDevice_Enumerate().
        /// @param [in] calibration The ID of the calibration object to apply. This ID is
        ///                         obtained by calling either the function
        ///                         QLCalibration_Create() or the function
        ///                         QLCalibration_Load().
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the calibration was successfully applied to the device.
        ///
        /// @see Calibration
        ///
        /// @example /src/QuickStart/Main.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_ApplyCalibration", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLDevice_ApplyCalibration(
               System.Int32 deviceID,
               System.Int32 calibrationID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLDevice_CalibrateEyeRadius( QLDeviceId device,
        /// float distance, float* leftRadius, float* rightRadius);
        ///
        /// @ingroup Device
        /// @brief  Calibrate the radius for each eye.
        ///
        /// This function uses the measured distance to the user in order to
        /// calculate the radius of the cornea for each eye. Using the radii returned
        /// by this function as the values for the settings
        /// QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT and
        /// QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_RIGHT will result in
        /// greater accuracy of the distance outputted for each frame.
        ///
        /// @param  [in] device      The ID of the device from which to calibrate
        ///                          the eye radius.
        /// @param  [in] distance    The actual measured distance in centimeters
        ///                          at the time this function is called that the
        ///                          user is away from the device.
        /// @param [out] leftRadius  A pointer to a float that will receive the
        ///                          radius in centimeters of the cornea of the
        ///                          left eye.
        /// @param [out] rightRadius A pointer to a float that will receive the
        ///                          radius in centimeters of the cornea of the
        ///                          right eye.
        ///
        /// @see QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT.
        /// @see QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the radius of the left and right eyes were successful.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLDevice_CalibrateEyeRadius", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError QLDevice_CalibrateEyeRadius(
            System.Int32 device,
            System.Single distance,
            out System.Single leftRadius,
            out System.Single rightRadius);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_Load( const char* path,
        /// QLSettingsId* settings)
        ///
        /// @ingroup Settings
        /// @brief  Load settings from a file into a settings container.
        ///
        /// This function loads settings from a file into a settings container. If
        /// there are settings in the file that are currently in the settings
        /// container then the values from the file will overwrite the current
        /// values. If a setting is in the file multiple times with different values,
        /// the last entry takes precedence.
        ///
        /// @param [in] path            A NULL-terminated string containing the full
        ///                             pathname of the settings file.
        /// @param [in,out] settings    A pointer to a QLSettingsId object. If the
        ///                             QLSettingsId object refers to a valid
        ///                             settings container then that settings
        ///                             container will receive the loaded settings.
        ///                             If the QLSettingsId object does not refer to
        ///                             a valid settings container then a new
        ///                             settings container will be created and this
        ///                             object will receive its ID.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the settings were successfully loaded.
        ///
        /// @example /src/QuickStart/Initialize.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_Load", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_Load(
               [In] System.String path,
               ref System.Int32 settingsID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_Save( const char* path,
        /// QLSettingsId settings)
        ///
        /// @ingroup Settings
        /// @brief  Save settings from a settings container to a file.
        ///
        /// This function saves the settings of a settings container to a file. If
        /// the file already exists then its contents are modified. Only the values
        /// of the settings that are in the settings container are changed. New
        /// settings are added to the end of the file. If the file did not previously
        /// exist then a new file is created.
        ///
        /// @param [in] path        A NULL-terminated string containing the full pathname
        ///                         of the settings file.
        /// @param [in] settings    The ID of the settings container containing the
        ///                         settings to save.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the settings were successfully saved.
        ///
        /// @example /src/QuickStart/Initialize.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_Save", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_Save(
               [In] System.String path,
               System.Int32 settingsID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_Create( QLSettingsId source,
        /// QLSettingsId* settings)
        ///
        /// @ingroup Settings
        /// @brief  Create a new settings container.
        ///
        /// This function creates a new settings container. If the source ID
        /// references a valid settings container then its contents will be copied
        /// into the newly created settings container. If the source ID does not
        /// reference a valid settings container then the new settings container will
        /// be empty.
        ///
        /// @param [in] source          The ID of the settings container from which
        ///                             to copy settings. This ID is obtained by
        ///                             calling either this function or the function
        ///                             QLSettings_Load(). To create an empty
        ///                             settings container, set this value to zero.
        /// @param [out] settings       A pointer to a QLSettingsId object. This
        ///                             object will receive its ID of the new
        ///                             settings container.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the new settings container was successfully created.
        ///
        /// @example /src/QuickStart/Initialize.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_Create", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_Create(
               System.Int32 sourceID,
               out System.Int32 settingsID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_AddSetting( QLSettingsId settings,
        /// const char* settingName)
        ///
        /// @ingroup Settings
        /// @brief  Add a setting to a settings container.
        ///
        /// This function adds a setting to a settings container and gives it an
        /// initial value of zero. If the setting already exists in the settings
        /// container then its value remains unchanged.
        ///
        /// @param [in] settings    The ID of the settings container that will receive
        ///                         the new setting. This ID is obtained by calling
        ///                         either the function QLSettings_Create() or the
        ///                         function QLSettings_Load().
        /// @param [in] settingName A NULL terminated string containing the name of the
        ///                         setting to add to the settings container.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the setting was successfully added to the settings container or the
        /// setting was already in the settings container.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_AddSetting", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_AddSetting(
               System.Int32 settingsID,
               [In] System.String settingName);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_RemoveSetting( QLSettingsId settings,
        /// const char* settingName)
        ///
        /// @ingroup Settings
        /// @brief  Remove a setting from a settings container.
        ///
        /// This function removes a setting from a settings container.
        ///
        /// @param [in] settings    The ID of the settings container from which the
        ///                         setting will be removed. This ID is obtained by
        ///                         calling either the function QLSettings_Create() or
        ///                         the function QLSettings_Load().
        /// @param [in] settingName A NULL terminated string containing the name of the
        ///                         setting that will be removed from the settings
        ///                         container.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the setting was successfully removed from the settings container.
        /// If the return value is QL_ERROR_NOT_FOUND then the setting was not in the
        /// container.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_RemoveSetting", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_RemoveSetting(
               System.Int32 settingsID,
               [In] System.String settingName);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValue( QLSettingsId settings,
        /// const char* settingName, QLSettingType settingType, const void * value)
        ///
        /// @ingroup Settings
        /// @brief  Set the value of a setting in a settings container.
        ///
        /// This function sets value of a setting in a settings container. If the
        /// setting already existed in the settings container then its value is
        /// updated. If the setting did not already exist in the settings container
        /// then it is added with the specified value.
        ///
        /// @param [in] settings    The ID of the settings container that either contains
        ///                         the setting to be altered or will receive the new
        ///                         setting if it did not exist previously. This ID is
        ///                         obtained by calling either the function
        ///                         QLSettings_Create() or the function
        ///                         QLSettings_Load().
        /// @param [in] settingName A NULL terminated string containing the name of the
        ///                         setting that will be altered in, or added to, the
        ///                         settings container.
        /// @param [in] settingType The type of data that was passed in. This tells the
        ///                         API how to interpret the data pointed to by "value".
        /// @param [in] value       A pointer to an object which contains the value to
        ///                         set the setting. The object type must match the type
        ///                         indicated by "settingType". For the type
        ///                         QL_SETTING_TYPE_STRING this must be a NULL terminated
        ///                         string containing the desired value.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the setting value was successfully updated or the setting was
        /// successfully added to the settings container if it did not already exist.
        ///
        /// @example /src/QuickStart/Initialize.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValue", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_SetValue(
               System.Int32 settingsID,
               [In] System.String settingName,
               QLSettingType settingType,
               [In] System.IntPtr value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueInt( QLSettingsId settings,
        /// const char* settingName, int value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_INT. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueInt", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueInt(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Int32 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueInt8( QLSettingsId settings,
        /// const char* settingName, __int8 value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_INT8. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueInt8", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueInt8(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Byte value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueInt16( QLSettingsId settings,
        /// const char* settingName, __int16 value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_INT16. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueInt16", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueInt16(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Int16 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueInt32( QLSettingsId settings,
        /// const char* settingName, __int32 value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_INT32. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueInt32", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueInt32(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Int32 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueInt64( QLSettingsId settings,
        /// const char* settingName, __int64 value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_INT64. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueInt64", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueInt64(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Int64 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueUInt( QLSettingsId settings,
        /// const char* settingName, unsigned int value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_UINT. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueUInt", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueUInt(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.UInt32 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueUInt8( QLSettingsId settings,
        /// const char* settingName, unsigned __int8 value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_UINT8. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueUInt8", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueUInt8(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Byte value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueUInt16( QLSettingsId settings,
        /// const char* settingName, unsigned __int16 value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_UINT16. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueUInt16", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueUInt16(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.UInt16 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueUInt32( QLSettingsId settings,
        /// const char* settingName, unsigned __int32 value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_UINT32. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueUInt32", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueUInt32(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.UInt32 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueUInt64( QLSettingsId settings,
        /// const char* settingName, unsigned __int64 value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_UINT64. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueUInt64", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueUInt64(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.UInt64 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueFloat( QLSettingsId settings,
        /// const char* settingName, float value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_FLOAT. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueFloat", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueFloat(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Single value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueDouble( QLSettingsId settings,
        /// const char* settingName, double value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_DOUBLE. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueDouble", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueDouble(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Double value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueBool( QLSettingsId settings,
        /// const char* settingName, bool value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_BOOL. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueBool", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueBool(
                System.Int32 settingsID,
                [In] System.String settingName,
                [MarshalAs(UnmanagedType.VariantBool)] System.Boolean value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_SetValueString( QLSettingsId settings,
        /// const char* settingName, char* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Set the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_STRING. This is a wrapper for
        /// QLSettings_SetValue().
        ///
        /// @see QLSettings_SetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_SetValueString", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_SetValueString(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.String value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValue( QLSettingsId settings,
        /// const char* settingName, QLSettingType settingType, int size, void* value)
        ///
        /// @ingroup Settings
        /// @brief  Get the value of a setting in a settings container.
        ///
        /// This function gets the value of a setting in a settings container if the
        /// setting exists.
        ///
        /// @param [in] settings    The ID of the settings container from which to
        ///                         get the value. This ID is obtained by calling
        ///                         either the function QLSettings_Create() or the
        ///                         function QLSettings_Load().
        /// @param [in] settingName A NULL terminated string containing the name of
        ///                         the setting whose value will be retrieved.
        /// @param [in] settingType The type of data object that was passed in. This
        ///                         tells the API how to interpret the object pointed
        ///                         to by "value".
        /// @param [in] size        If the type is QL_SETTING_TYPE_STRING then this
        ///                         is the size of the buffer pointed to by "value".
        ///                         For other types this is not used.
        /// @param [out] value      A pointer to an object that will receive the
        ///                         value of the setting. The object type must match
        ///                         the type indicated by "settingType". For the type
        ///                         QL_SETTING_TYPE_STRING this must be a char array
        ///                         at least as large as "size".
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the setting value was successfully retrieved.
        ///
        /// @example /src/QuickStart/Initialize.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValue", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_GetValue(
               System.Int32 settingsID,
               [In] System.String settingName,
               QLSettingType settingType,
               System.Int32 size,
               System.IntPtr value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueInt( QLSettingsId settings,
        /// const char* settingName, int* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_INT. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueInt", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueInt(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.Int32 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueInt8( QLSettingsId settings,
        /// const char* settingName, __int8* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_INT8. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueInt8", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueInt8(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.Byte value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueInt16( QLSettingsId settings,
        /// const char* settingName, __int16* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_INT16. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueInt16", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueInt16(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.UInt16 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueInt32( QLSettingsId settings,
        /// const char* settingName, __int32* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_INT32. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueInt32", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueInt32(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.UInt32 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueInt64( QLSettingsId settings,
        /// const char* settingName, __int64* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_INT64. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueInt64", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueInt64(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.UInt64 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueUInt( QLSettingsId settings,
        /// const char* settingName, unsigned int* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_UINT. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueUInt", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueUInt(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.UInt32 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueUInt8( QLSettingsId settings,
        /// const char* settingName, unsigned __int8* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_UINT8. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueUInt8", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueUInt8(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.Byte value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueUInt16( QLSettingsId settings,
        /// const char* settingName, unsigned __int16* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_UINT16. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueUInt16", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueUInt16(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.UInt16 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueUInt32( QLSettingsId settings,
        /// const char* settingName, unsigned __int32* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_UINT32. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueUInt32", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueUInt32(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.UInt32 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueUInt64( QLSettingsId settings,
        /// const char* settingName, unsigned __int64* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_UINT64. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueUInt64", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueUInt64(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.UInt64 value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueFloat( QLSettingsId settings,
        /// const char* settingName, float* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_FLOAT. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueFloat", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueFloat(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.Single value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueDouble( QLSettingsId settings,
        /// const char* settingName, double* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_DOUBLE. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueDouble", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueDouble(
                System.Int32 settingsID,
                [In] System.String settingName,
                out System.Double value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueBool( QLSettingsId settings,
        /// const char* settingName, bool* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_BOOL. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueBool", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueBool(
                System.Int32 settingsID,
                [In] System.String settingName,
                [MarshalAs(UnmanagedType.VariantBool)] out System.Boolean value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueString( QLSettingsId settings,
        /// const char* settingName, int size, char* value)
        ///
        /// @ingroup Settings
        ///
        /// @brief Get the value of a setting in a settings container using the
        /// setting type QL_SETTING_TYPE_STRING. This is a wrapper for
        /// QLSettings_GetValue().
        ///
        /// @see QLSettings_GetValue()
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueString", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
            QLSettings_GetValueString(
                System.Int32 settingsID,
                [In] System.String settingName,
                System.Int32 size,
                System.Text.StringBuilder value);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @}
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLSettings_GetValueStringSize( QLSettingsId settings,
        /// const char* settingName, int* size)
        ///
        /// @ingroup Settings
        /// @brief  Get the string size of the setting value.
        ///
        /// This function gets the size of the string, including the terminating NULL
        /// character, for the specified setting's value. A buffer would have to be
        /// at least as large as the value returned by "size" to successfully get a
        /// value of type QL_SETTING_TYPE_STRING using the function
        /// QLSettings_GetValue().
        ///
        /// @param [in] settings    The ID of the settings container from which to
        ///                         get the value's string length. This ID is
        ///                         obtained by calling either the function
        ///                         QLSettings_Create() or the function
        ///                         QLSettings_Load().
        /// @param [in] settingName A NULL terminated string containing the name of
        ///                         the setting whose value's string length will be
        ///                         retrieved.
        /// @param [out] size       A pointer to an object that will receive the
        ///                         string length of the value.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the string length for the setting value was successfully retrieved.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLSettings_GetValueStringSize", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLSettings_GetValueStringSize(
               System.Int32 settingsID,
               [In] System.String settingName,
               out System.Int32 size);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLCalibration_Load( const char* path,
        /// QLCalibrationId* calibration)
        ///
        /// @ingroup Calibration
        /// @brief  Load calibration data from a file.
        ///
        /// This function loads calibration data from a file into a calibration
        /// container. The file must have been created by calling the function
        /// QLCalibration_Save().
        ///
        /// @param [in] path            A NULL-terminated string containing the full
        ///                             pathname of the calibration file.
        /// @param [in,out] calibration A pointer to a QLCalibrationId object. If the
        ///                             QLCalibrationId object refers to a valid
        ///                             calibration container then that calibration
        ///                             container will receive the loaded
        ///                             calibration. If the QLCalibrationId object
        ///                             does not refer to a valid calibration
        ///                             container then a new calibration container
        ///                             will be created and this object will receive
        ///                             its ID.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the calibration was successfully loaded.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Load", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Load(
               [In] System.String path,
               ref System.Int32 calibrationID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLCalibration_Save( const char* path,
        /// QLCalibrationId calibration)
        ///
        /// @ingroup Calibration
        /// @brief  Save calibration data to a file.
        ///
        /// This function saves calibration data to a file. The calibration data can
        /// later be loaded by calling the function QLCalibration_Load().
        ///
        /// @param [in] path        A NULL-terminated string containing the full pathname
        ///                         of the calibration file.
        /// @param [in] calibration The ID of the calibration container whose data will
        ///                         be saved. This ID is obtained by calling either the
        ///                         function QLCalibration_Create() or the function
        ///                         QLCalibration_Load().
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the calibration was successfully saved.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Save", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Save(
               [In] System.String path,
               System.Int32 calibrationID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLCalibration_Create( QLCalibrationId source,
        /// QLCalibrationId* calibration)
        ///
        /// @ingroup Calibration
        /// @brief  Create a new calibration container.
        ///
        /// This function creates a new calibration container. If the source ID
        /// references a valid calibration container then its data will be copied
        /// into the newly created calibration container. If the source ID does not
        /// reference a valid settings container then the new settings container will
        /// be empty.
        ///
        /// @param [in] source          The ID of the calibration container from
        ///                             which to copy calibration data. This ID is
        ///                             obtained by calling either this function or
        ///                             the function QLCalibration_Load(). To create
        ///                             an empty settings container, set this value
        ///                             to zero.
        /// @param [out] calibration    A pointer to a QLCalibrationId object. This
        ///                             object will receive the ID of the newly
        ///                             created calibration container.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the new calibration container was successfully created.
        ///
        /// @example /src/QuickStart/Calibrate.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Create", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Create(
               System.Int32 sourceID,
               out System.Int32 calibrationID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLCalibration_Initialize( QLDeviceId device,
        /// QLCalibrationId calibration, QLCalibrationType type)
        ///
        /// @ingroup Calibration
        /// @brief  Initialize a calibration container.
        ///
        /// This function initializes a calibration container which makes it ready to
        /// receive new calibration data from a device. Any calibration data
        /// previously in the container is stored in temporary memory until
        /// QLCalibration_Finalize() is called.
        ///
        /// @param [in] device      The ID of the device from which to receive
        ///                         calibration data. This ID is obtained by calling the
        ///                         function QLDevice_Enumerate().
        /// @param [in] calibration The ID of the calibration container that will receive
        ///                         the new calibration data. This ID is obtained by
        ///                         calling either the function QLCalibration_Create() or
        ///                         the function QLCalibration_Load().
        /// @param [in] type        The type of calibration to perform.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the calibration container was successfully initialized and is now
        /// ready to receive new calibration data.
        ///
        /// @see Device
        ///
        /// @example /src/QuickStart/Calibrate.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Initialize", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Initialize(
               System.Int32 deviceID,
               System.Int32 calibrationID,
               QLCalibrationType type);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLCalibration_GetTargets( QLCalibrationId calibration,
        /// int* numTargets, QLCalibrationTarget* targets)
        ///
        /// @ingroup Calibration
        /// @brief  Get the positions for the calibration targets.
        ///
        /// This function gets the locations and IDs of the targets for the
        /// calibration. It can be called before or after a calibration has been
        /// finalized.
        ///
        /// @param [in] calibration     The ID of a calibration container from which
        ///                             to get the target data. This ID is obtained
        ///                             by calling either the function
        ///                             QLCalibration_Create() or the function
        ///                             QLCalibration_Load().
        /// @param [in,out] numTargets  A pointer to an integer containing the size
        ///                             of the target buffer pointed to by "targets".
        ///                             When the function returns this contains the
        ///                             number of targets for the calibration.
        /// @param [out] targets        A pointer to a QLCalibrationTarget buffer
        ///                             that will receive the data for the
        ///                             calibration points.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the target positions were successfully retrieved.
        ///
        /// @example /src/QuickStart/Calibrate.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_GetTargets", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_GetTargets(
               System.Int32 calibrationID,
               ref System.Int32 numTargets,
                [In, Out] QLCalibrationTarget[] targets);

        //[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 1)]
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLCalibration_Calibrate( QLCalibrationId calibration,
        /// QLTargetId target, int duration, bool block)
        ///
        /// @ingroup Calibration
        /// @brief  Calibrate a target.
        ///
        /// This function causes the calibration container to collect data from a
        /// device for a specific target location. It should be called at least once
        /// for each target that was received by calling the function
        /// QLCalibration_GetTargets(). If this function is called multiple times for
        /// the same target then the data collected by the last call will overwrite
        /// previous data.
        ///
        /// This function must be called after a calibration has been initialized but
        /// before it has been finalized.
        ///
        /// @param [in] calibration The ID of a calibration container which has been
        ///                         initialized and is ready to receive calibration data.
        ///                         This ID is obtained by calling either the function
        ///                         QLCalibration_Create() or the function
        ///                         QLCalibration_Load().
        /// @param [in] target      The ID of a target that the user is looking at. This
        ///                         ID is obtained by calling the function
        ///                         QLCalibration_GetTargets(). Usually there should be a
        ///                         target drawn on the screen at the location referenced
        ///                         by this target before calling this function.
        /// @param [in] duration    The length of time that the API will collect
        ///                         calibration data. For best results the user should be
        ///                         looking at the target position the entire time.
        /// @param [in] block       A flag to determine whether this function should
        ///                         block. If this is true then the function will block
        ///                         and not return until the API is done collecting data
        ///                         for the calibration point. If this is false then this
        ///                         function will return immediately and the status of
        ///                         the data collection can be determined by calling the
        ///                         function QLCalibration_GetStatus().
        ///
        /// @return The success of the function. If the function blocks then a return
        /// value of QL_ERROR_OK means that the duration has finished and that data
        /// was gathered for the target. If the function does not block then a return
        /// value of QL_ERROR_OK means that calibration data collection was
        ///
        /// @example /src/QuickStart/Calibrate.cpp
        /// successfully started for the target.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Calibrate", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Calibrate(
               System.Int32 calibrationID,
               System.Int32 targetID,
               System.Int32 duration,
               [MarshalAs(UnmanagedType.VariantBool)] System.Boolean block);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLCalibration_GetScoring( QLCalibrationId calibration,
        /// QLTargetId target, QLEyeType eye, QLCalibrationScore* score)
        ///
        /// @ingroup Calibration
        /// @brief  Get the scoring of a calibration target.
        ///
        /// This function gets the the score of a calibration target for a particular
        /// eye. The eye must have been detected at least once for each target of the
        /// calibration in order to produce a score.
        ///
        /// This function can be called before or after a calibration has been
        /// finalized.
        ///
        /// @param [in] calibration The ID of a calibration container whose data will
        ///                         be used to calculate a score. This ID is obtained
        ///                         by calling either the function
        ///                         QLCalibration_Create() or the function
        ///                         QLCalibration_Load().
        /// @param [in] target      The ID of a target whose score will be retrieved.
        ///                         This ID is obtained by calling the function
        ///                         QLCalibration_GetTargets().
        /// @param [in] eye         The eye whose score should be retrieved.
        /// @param [out] score      A pointer to a QLCalibrationScore object that
        ///                         will receive the score.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the score was successfully retrieved. If the return value is
        /// QL_ERROR_INTERNAL_ERROR then use the function QLCalibration_GetStatus()
        /// to get extended error information().
        ///
        /// @example /src/QuickStart/Calibrate.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_GetScoring", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_GetScoring(
               System.Int32 calibrationID,
               System.Int32 targetID,
               QLEyeType eye,
               out QLCalibrationScore score);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLCalibration_GetStatus( QLCalibrationId calibration,
        /// QLTargetId target, QLCalibrationStatus* calibrationStatus)
        ///
        /// @ingroup Calibration
        /// @brief  Get the status of a calibration target.
        ///
        /// This function retrieves the status of a calibration target for a given
        /// calibration container. It can be called before or after a calibration has
        /// been finalized.
        ///
        /// @param [in] calibration             The ID of a calibration container
        ///                                     whose data will be used to determine
        ///                                     a status for the target. This ID is
        ///                                     obtained by calling either the
        ///                                     function QLCalibration_Create() or
        ///                                     the function QLCalibration_Load().
        /// @param [in] target                  The ID of a target whose status will
        ///                                     be retrieved. This ID is obtained by
        ///                                     calling the function
        ///                                     QLCalibration_GetTargets().
        /// @param [out] calibrationStatus      A pointer to a QLCalibrationStatus
        ///                                     object that will receive the status.
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the calibration status for the target was successfully retrieved.
        ///
        /// @example /src/QuickStart/Calibrate.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_GetStatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_GetStatus(
               System.Int32 calibrationID,
               System.Int32 targetID,
               out QLCalibrationStatus calibrationStatus);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLCalibration_Finalize( QLCalibrationId calibration)
        ///
        /// @ingroup Calibration
        /// @brief  Finalize a completed calibration.
        ///
        /// This function finalizes a calibration after it is complete. It should be
        /// called when calibration data has been successfully collected at each
        /// target position and the target scores meet the user requirements.
        ///
        /// This function clears any previous calibration data that was stored in the
        /// container.
        ///
        /// @param [in] calibration The ID of a calibration container to finalize. This
        ///                         ID is obtained by calling either the function
        ///                         QLCalibration_Create() or the function
        ///                         QLCalibration_Load().
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the calibration was successfully finalized.
        ///
        /// @example /src/QuickStart/Calibrate.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Finalize", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Finalize(
               System.Int32 calibrationID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLCalibration_Cancel( QLCalibrationId calibration)
        ///
        /// @ingroup Calibration
        /// @brief  Cancel a calibration that is in process.
        ///
        /// This function cancels a calibration. Recently collected calibration data
        /// is cleared and the calibration container is restored to the state it was
        /// in before QLCalibration_Initialize() was called.
        ///
        /// @param [in] calibration The ID of a calibration container to cancel. This
        ///                         ID is obtained by calling either the function
        ///                         QLCalibration_Create() or the function
        ///                         QLCalibration_Load().
        ///
        /// @return The success of the function. If the return value is QL_ERROR_OK
        /// then the calibration was successfully canceled.
        ///
        /// @example /src/QuickStart/Calibrate.cpp
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_Cancel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_Cancel(
               System.Int32 calibrationID);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLCalibration_AddBias( QLCalibrationId calibration,
        /// QLEyeType eye, float xOffset, float yOffset)
        ///
        /// @ingroup Calibration
        /// @brief  Add a bias to the data in the calibration container.
        ///
        /// @param [in] calibration The ID of a calibration container to which the bias
        ///                         will be added. This ID is obtained by calling either
        ///                         the function QLCalibration_Create() or the function
        ///                         QLCalibration_Load().
        /// @param [in] eye         The eye to which the bias should be added.
        /// @param [in] xOffset     The percent of the screen in the x direction that the
        ///                         bias should affect the gaze point. Negative values
        ///                         will cause the resulting gaze point to be left of the
        ///                         current position. Positive values will cause the
        ///                         resulting gaze point to be right of the current
        ///                         position.
        /// @param [in] yOffset     The percent of the screen in the Y direction that the
        ///                         bias should affect the gaze point. Negative values
        ///                         will cause the resulting gaze point to be above the
        ///                         current position. Positive values will cause the
        ///                         resulting gaze point to be below the current
        ///                         position.
        ///
        /// @return .
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("QuickLink2.dll", EntryPoint = "QLCalibration_AddBias", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern
        QLError
           QLCalibration_AddBias(
               System.Int32 calibrationID,
               QLEyeType eye,
               System.Single xOffset,
               System.Single yOffset);

        /* NOTE: This call (QLAPI_GetVersion) was the only one I could not get
         * to work.  I finally decided it wasn't important enough to make
         * myself insane over.  If anyone else figures out how to make this
         * work, please let me know.  -Justin
         */
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// @fn QLError QUICK_LINK_2_CALL_CONVEN QLAPI_GetVersion(int bufferSize, char* buffer)
        ///
        /// @ingroup API
        /// @brief  Get the version of the API.
        ///
        /// This function retrieves the version string of the API.
        ///
        /// @param [in] bufferSize The size of the output buffer.
        /// @param [out] buffer    A pointer to a char buffer that receives the
        ///                        version string.
        ///
        /// @return The success of the function.
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //[DllImport("QuickLink2.dll", EntryPoint = "QLAPI_GetVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        //public static extern
        //QLError
        //   QLAPI_GetVersion(
        //       System.Int32 bufferSize,
        //       System.Text.StringBuilder buffer);
    }

    #endregion QuickLink2 API Operations
}