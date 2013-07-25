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

/* $Id: QLTypes.cs 38 2011-05-09 01:07:39Z piranther $
 *
 * Description: This file contains the definitions of all the strings,
 * data structures, and enumerations available through QuickLink2.dll
 *
 * This wrapper requires that you place the QuickLink2.dlls in the same
 * directory as your program executable.  You can download QuickLink2 from
 * http://www.eyetechds.com/support/downloads
 *
 * The extensive inline documentation has been cut & pasted from the
 * QLTypes.h C++ header file for convenient reference. That original file is
 * Copyright (C) 1996 - 2012 EyeTech Digital Systems.
 */

#endregion Header Comments

using System;
using System.Runtime.InteropServices;

namespace QuickLink2DotNet
{
    #region Settings

    /// <summary>
    /// Settings strings for use with QuickLink2 API functions.
    /// </summary>
    public static class QL_SETTINGS
    {
        /// <summary>
        /// <para>
        /// The bandwidth mode the device will use.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDeviceBandwidthMode" />
        public static string QL_SETTING_DEVICE_BANDWIDTH_MODE = "DeviceBandwidthMode";

        /// <summary>
        /// <para>
        /// The percentage of the bus bandwidth used by the device when searching for eyes. This value is
        /// only used when the <see cref="QL_SETTING_DEVICE_BANDWIDTH_MODE" /> setting is set to
        /// <see cref="QLDeviceBandwidthMode.QL_DEVICE_BANDWIDTH_MODE_MANUAL" />.
        /// </para>
        /// <para>
        /// Possible values range from 1-100.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_BANDWIDTH_PERCENT_FULL = "DeviceBandwidthPercentFull";

        /// <summary>
        /// <para>
        /// The percentage of the bus bandwidth used by the device when at least one eye has been
        /// found and is being tracked. This value is only used when the <see cref="QL_SETTING_DEVICE_BANDWIDTH_MODE" />
        /// setting is set to <see cref="QLDeviceBandwidthMode.QL_DEVICE_BANDWIDTH_MODE_MANUAL" />.
        /// </para>
        /// <para>
        /// Possible values range from 1-100.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_BANDWIDTH_PERCENT_TRACKING = "DeviceBandwidthPercentTracking";

        /// <summary>
        /// <para>
        /// The number of pixels to combine in the x and y direction.
        /// <para>The output pixel values are the average of the combined pixels. Possible values are 1,
        /// 2, and 4. A value of 1 outputs one pixel of data for each pixel on the image sensor. A value
        /// of 2 outputs one pixel of data for every 4 (2 X 2) pixels on the image sensor. A value of 4
        /// </para>
        /// outputs one pixel of data for every 16 (4 X 4) pixels on the image sensor.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_BINNING = "DeviceBinning";

        /// <summary>
        /// <para>
        /// The flag for enabling/disabling calibration.
        /// </para>
        /// <para>
        /// Possible values are true and false. If false then calibration data collection will be
        /// disabled and new calibrations can not be performed.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_CALIBRATE_ENABLED = "DeviceCalibrateEnabled";

        /// <summary>
        /// <para>
        /// The approximate distance in centimeters from the user to the device.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_DISTANCE = "DeviceDistance";

        /// <summary>
        /// <para>
        /// The exposure time in milliseconds for each frame. Possible values range from 1-50 ms.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_EXPOSURE = "DeviceExposure";

        /// <summary>
        /// <para>
        /// The horizontal direction of the image.
        /// </para>
        /// <para>
        /// Possible values are true and false. A value of false will result in the right eye being
        /// closest to the origin (0, 0) of the image. A value of true will mirror the image and cause
        /// the left eye to be closest to the origin.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_FLIP_X = "DeviceFlipX";

        /// <summary>
        /// <para>
        /// The vertical direction of the image.
        /// </para>
        /// <para>
        /// Possible values are true and false. A value of false will result in the top of the head being
        /// closest to the origin (0, 0) of the image. A value of true will cause the bottom of the face
        /// to be closest to the origin of the image.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_FLIP_Y = "DeviceFlipY";

        /// <summary>
        /// <para>
        /// The gain mode the device will use.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTING_DEVICE_GAIN_VALUE" />
        /// <seealso cref="QLDeviceGainMode" />
        public static string QL_SETTING_DEVICE_GAIN_MODE = "DeviceGainMode";

        /// <summary>
        /// <para>
        /// The gain value the device will use when the setting <see cref="QL_SETTING_DEVICE_GAIN_MODE" /> is set to
        /// <see cref="QLDeviceGainMode.QL_DEVICE_GAIN_MODE_MANUAL" />.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDeviceGainMode" />
        public static string QL_SETTING_DEVICE_GAIN_VALUE = "DeviceGainValue";

        /// <summary>
        /// <para>
        /// The filter mode for the output gaze point.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE" />
        /// <seealso cref="QLDeviceGazePointFilterMode" />
        public static string QL_SETTING_DEVICE_GAZE_POINT_FILTER_MODE = "DeviceGazePointFilterMode";

        /// <summary>
        /// <para>
        /// The value used for the filtering mode.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTING_DEVICE_GAZE_POINT_FILTER_MODE" />
        /// <seealso cref="QLDeviceGazePointFilterMode" />
        public static string QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE = "DeviceGazePointFilterValue";

        /// <summary>
        /// <para>
        /// The flag for enabling/disabling image processing.
        /// </para>
        /// <para>
        /// Possible values are true and false. If false then the eyes will not be searched for and the
        /// output eye information will not be valid.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_IMAGE_PROCESSING_ENABLED = "DeviceImageProcessingEnabled";

        /// <summary>
        /// <para>
        /// The search setting for the image processing.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDeviceEyesToUse" />
        public static string QL_SETTING_DEVICE_IMAGE_PROCESSING_EYES_TO_FIND = "DeviceImageProcessingEyesToUse";

        /// <summary>
        /// <para>
        /// The radius of the cornea of the left eye in centimeters. This radius can be
        /// calculated by calling the function <see cref="QuickLink2API.QLDevice_CalibrateEyeRadius" />. This radius will affect the
        /// calculated distance value that is output for each frame.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_RIGHT" />
        /// <seealso cref="QLFrameData" />
        /// <seealso cref="QuickLink2API.QLDevice_CalibrateEyeRadius" />
        public static string QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT = "DeviceImageProcessingEyeRadiusLeft";

        /// <summary>
        /// <para>
        /// The radius of the cornea of the right eye in centimeters. This radius can be
        /// calculated by calling the function <see cref="QuickLink2API.QLDevice_CalibrateEyeRadius" />. This radius will affect the
        /// calculated distance value that is output for each frame.
        /// </para>
        /// </summary>
        /// <seealso cref="QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_LEFT" />
        /// <seealso cref="QLFrameData" />
        /// <seealso cref="QuickLink2API.QLDevice_CalibrateEyeRadius" />
        public static string QL_SETTING_DEVICE_IMAGE_PROCESSING_EYE_RADIUS_RIGHT = "DeviceImageProcessingEyeRadiusRight";

        /// <summary>
        /// <para>
        /// The flag for enabling/disabling final gaze point interpolation.
        /// </para>
        /// <para>
        /// Possible values are true and false. If false then the final gaze point will not be
        /// interpolated and the output gaze point will not be valid.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_INTERPOLATE_ENABLED = "DeviceInterpolateEnabled";

        /// <summary>
        /// <para>
        /// The mode of operation for the IR lights of the device.
        /// </para>
        /// </summary>
        /// <seealso cref="QLDeviceIRLightMode"/>
        public static string QL_SETTING_DEVICE_IR_LIGHT_MODE = "DeviceIRLightMode";

        /// <summary>
        /// <para>
        /// The focal length in centimeters of the lens.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_LENS_FOCAL_LENGTH = "DeviceLensFocalLength";

        /// <summary>
        /// <para>
        /// The horizontal distance in percentage of the image width that either eye can be from
        /// the left or right edge of the region of interest before the region of interest will move and
        /// try to re-center the eyes.
        /// </para>
        /// <para>Possible values range from 1-50.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_X = "DeviceRoiMoveThresholdPercentX";

        /// <summary>
        /// <para>
        /// The vertical distance in percentage of the image height that either eye can be from
        /// the top or bottom edge of the region of interest before the region of interest will move and
        /// try to re-center the eyes.
        /// </para>
        /// <para>Possible values range from 1-50.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_ROI_MOVE_THRESHOLD_PERCENT_Y = "DeviceRoiMoveThresholdPercentY";

        /// <summary>
        /// <para>
        /// The width of the region of interest in percentage of the horizontal sensor size when
        /// the eyes are being tracked.
        /// </para>
        /// <para>
        /// Possible values range from 1-100.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_ROI_SIZE_PERCENT_X = "DeviceRoiSizePercentX";

        /// <summary>
        /// <para>
        /// The height of the region of interest in percentage of the vertical sensor size when
        /// the eyes are being tracked.
        /// </para>
        /// <para>
        /// Possible values range from 1-100.
        /// </para>
        /// </summary>
        public static string QL_SETTING_DEVICE_ROI_SIZE_PERCENT_Y = "DeviceRoiSizePercentY";
    }

    #endregion Settings

    #region Enumerations

    /// <summary>
    /// <para>
    /// Status values for a calibration target
    /// </para>
    /// </summary>
    /// <seealso cref="QuickLink2API.QLCalibration_GetStatus"/>
    public enum QLCalibrationStatus
    {
        /// <summary>
        /// <para>
        /// The calibration target has data for both the left and right eyes.
        /// </para>
        /// </summary>
        QL_CALIBRATION_STATUS_OK = 0,

        /// <summary>
        /// <para>
        /// The calibration target is in the process of calibrating.
        /// </para>
        /// </summary>
        QL_CALIBRATION_STATUS_CALIBRATING = 1,

        /// <summary>
        /// <para>
        /// The calibration target has data for the right eye but not the left eye.
        /// </para>
        /// </summary>
        QL_CALIBRATION_STATUS_NO_LEFT_DATA = 2,

        /// <summary>
        /// <para>
        /// The calibration target has data for the left eye but not the right eye.
        /// </para>
        /// </summary>
        QL_CALIBRATION_STATUS_NO_RIGHT_DATA = 3,

        /// <summary>
        /// <para>
        /// The calibration target does not have data for the left or right eye.
        /// </para>
        /// </summary>
        QL_CALIBRATION_STATUS_NO_DATA = 4
    }

    /// <summary>
    /// <para>
    /// Values that identify different calibration modes.
    /// </para>
    /// </summary>
    /// <seealso cref="QuickLink2API.QLCalibration_Initialize"/>
    public enum QLCalibrationType
    {
        /// <summary>
        /// <para>
        /// A five point calibration.
        /// </para>
        /// </summary>
        QL_CALIBRATION_TYPE_5 = 0,

        /// <summary>
        /// <para>
        /// A nine point calibration.
        /// </para>
        /// </summary>
        QL_CALIBRATION_TYPE_9 = 1,

        /// <summary>
        /// <para>
        /// A sixteen point calibration.
        /// </para>
        /// </summary>
        QL_CALIBRATION_TYPE_16 = 2
    }

    /// <summary>
    /// <para>
    /// Values that identify which bandwidth mode to use.
    /// </para>
    /// </summary>
    /// <seealso cref="QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_MODE" />
    /// <seealso cref="QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_PERCENT_FULL" />
    /// <seealso cref="QL_SETTINGS.QL_SETTING_DEVICE_BANDWIDTH_PERCENT_TRACKING" />
    public enum QLDeviceBandwidthMode
    {
        /// <summary>
        /// <para>
        /// The device will automatically adjust the bandwidth value until the best value is found.
        /// </para>
        /// </summary>
        QL_DEVICE_BANDWIDTH_MODE_AUTO = 0,

        /// <summary>
        /// <para>
        /// The device will use a fixed bandwidth value.
        /// </para>
        /// </summary>
        QL_DEVICE_BANDWIDTH_MODE_MANUAL = 1
    }

    /// <summary>
    /// <para>
    /// Values that identify which eyes the device should process.
    /// </para>
    /// </summary>
    /// <seealso cref="QL_SETTINGS.QL_SETTING_DEVICE_IMAGE_PROCESSING_EYES_TO_FIND" />
    public enum QLDeviceEyesToUse
    {
        /// <summary>
        /// <para>
        /// Only search for and process the left eye
        /// </para>
        /// </summary>
        QL_DEVICE_EYES_TO_USE_LEFT = 0,

        /// <summary>
        /// <para>
        /// Only search for and process the right eye
        /// </para>
        /// </summary>
        QL_DEVICE_EYES_TO_USE_RIGHT = 1,

        /// <summary>
        /// <para>
        /// Search for and process both eyes. If only one eye is found then use it for calculating the
        /// weighted gaze point.
        /// </para>
        /// </summary>
        QL_DEVICE_EYES_TO_USE_LEFT_OR_RIGHT = 2,

        /// <summary>
        /// <para>
        /// Search for and process both eyes. Both eyes must be found for calculating the weighted gaze
        /// point.
        /// </para>
        /// </summary>
        QL_DEVICE_EYES_TO_USE_LEFT_AND_RIGHT = 3
    }

    /// <summary>
    /// <para>
    /// Values that identify which gain mode to use.
    /// </para>
    /// </summary>
    /// <seealso cref="QL_SETTINGS.QL_SETTING_DEVICE_GAIN_MODE" />
    /// <seealso cref="QL_SETTINGS.QL_SETTING_DEVICE_GAIN_VALUE" />
    public enum QLDeviceGainMode
    {
        /// <summary>
        /// <para>
        /// The device will automatically adjust the vain value.
        /// </para>
        /// </summary>
        QL_DEVICE_GAIN_MODE_AUTO = 0,

        /// <summary>
        /// <para>
        /// The device will use a fixed gain value.
        /// </para>
        /// </summary>
        QL_DEVICE_GAIN_MODE_MANUAL = 1
    }

    /// <summary>
    /// <para>
    /// Values that identify which gain mode to use.
    /// </para>
    /// </summary>
    /// <seealso cref="QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_MODE" />
    /// <seealso cref="QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE" />
    public enum QLDeviceGazePointFilterMode
    {
        /// <summary>
        /// <para>
        /// No gaze point filtering will be done.
        /// </para>
        /// </summary>
        QL_DEVICE_GAZE_POINT_FILTER_NONE = 0,

        /// <summary>
        /// <para>
        /// The median gaze point value over the last X number of frames, where X equals the value
        /// represented by the setting
        /// <see cref="QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE" />. This will produce a gaze point latency time
        /// of about (X / fps / 2) seconds.
        /// </para>
        /// </summary>
        QL_DEVICE_GAZE_POINT_FILTER_MEDIAN_FRAMES = 1,

        /// <summary>
        /// <para>
        /// The median gaze point value over the last X number of frames, where X is the number of
        /// frames gathered over twice the amount of milliseconds represented by the setting
        /// <see cref="QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE" />. This will produce a
        /// latency of about
        /// <see cref="QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE" /> milliseconds.
        /// </para>
        /// </summary>
        QL_DEVICE_GAZE_POINT_FILTER_MEDIAN_TIME = 2,

        /// <summary>
        /// <para>
        /// The heuristic filter uses different filtering strengths when the eye is moving and when it
        /// is fixating. When the eye is moving, very little filtering is done which results in very low
        /// latency. When the eye is fixating, large amounts of filtering are being done which greatly
        /// reduce the amount of jitter. During fixation, filtering is done over the last X number of
        /// frames where X equals the value represented by the setting
        /// <see cref="QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE" />.
        /// </para>
        /// </summary>
        QL_DEVICE_GAZE_POINT_FILTER_HEURISTIC_FRAMES = 3,

        /// <summary>
        /// <para>
        /// The heuristic filter uses different filtering strengths when the eye is moving and when it
        /// is fixating. When the eye is moving, very little filtering is done which results in very low
        /// latency. When the eye is fixating, large amounts of filtering are being done which greatly
        /// reduce the amount of jitter. During fixation, filtering is done over the last X number of
        /// frames where X equals the number of frames gathered over twice the amount of milliseconds
        /// represented by the setting
        /// <see cref="QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE" />. This produces approximately
        /// the same amount of latency during fixation for all frame rates.
        /// </para>
        /// </summary>
        QL_DEVICE_GAZE_POINT_FILTER_HEURISTIC_TIME = 4,

        /// <summary>
        /// <para>
        /// The weighted previous frame mode filters the gaze point by summing the current weighted gaze
        /// point location and the previous weighted gaze point location. The weights are based on the
        /// distance the current gaze point is away from the previous gaze point. The larger the
        /// distance, the greater the weight on the current gaze point. The smaller the distance, the
        /// greater the weight on the previous gaze point. This results in very low latency when the eye
        /// is moving and very low jitter when the eye is fixating. The value represented by the setting
        /// <see cref="QL_SETTINGS.QL_SETTING_DEVICE_GAZE_POINT_FILTER_VALUE" /> affects the rate at which
        /// the weighting changes from the previous gaze point to the current gaze point. Possible
        /// values range between 0 and 100.
        /// </para>
        /// </summary>
        QL_DEVICE_GAZE_POINT_FILTER_WEIGHTED_PREVIOUS_FRAME = 5
    }

    /// <summary>
    /// <para>
    /// Values that identify which light mode to use.
    /// </para>
    /// </summary>
    /// <seealso cref="QL_SETTINGS.QL_SETTING_DEVICE_IR_LIGHT_MODE"/>
    public enum QLDeviceIRLightMode
    {
        /// <summary>
        /// <para>
        /// The IR lights on the device will be off. Use this when other IR light sources are being
        /// used.
        /// </para>
        /// </summary>
        QL_DEVICE_IR_LIGHT_MODE_OFF = 0,

        /// <summary>
        /// <para>
        /// The IR lights on the device will be on. This should be used for normal operation.
        /// </para>
        /// </summary>
        QL_DEVICE_IR_LIGHT_MODE_AUTO = 1
    }

    /// <summary>
    /// <para>
    /// Status values for a device.
    /// </para>
    /// </summary>
    /// <seealso cref="QuickLink2API.QLDevice_GetStatus" />
    public enum QLDeviceStatus
    {
        /// <summary>
        /// <para>
        /// The device cannot be detected. It has probably been disconnected from the computer.
        /// </para>
        /// </summary>
        QL_DEVICE_STATUS_UNAVAILABLE = 0,

        /// <summary>
        /// <para>
        /// The device is stopped.
        /// </para>
        /// </summary>
        QL_DEVICE_STATUS_STOPPED = 1,

        /// <summary>
        /// <para>
        /// The device is in the process of being started.
        /// </para>
        /// </summary>
        QL_DEVICE_STATUS_INITIALIZED = 2,

        /// <summary>
        /// <para>
        /// The device is running.
        /// </para>
        /// </summary>
        QL_DEVICE_STATUS_STARTED = 3
    }

    /// <summary>
    /// <para>
    /// Error codes returned from Quick Link 2 functions.
    /// </para>
    /// </summary>
    public enum QLError
    {
        /// <summary>
        /// <para>
        /// The function successfully completed.
        /// </para>
        /// </summary>
        QL_ERROR_OK = 0,

        /// <summary>
        /// <para>
        /// The input device ID is invalid.
        /// </para>
        /// </summary>
        QL_ERROR_INVALID_DEVICE_ID = 1,

        /// <summary>
        /// <para>
        /// The input settings ID is invalid.
        /// </para>
        /// </summary>
        QL_ERROR_INVALID_SETTINGS_ID = 2,

        /// <summary>
        /// <para>
        /// The input calibration ID is invalid.
        /// </para>
        /// </summary>
        QL_ERROR_INVALID_CALIBRATION_ID = 3,

        /// <summary>
        /// <para>
        /// The input target ID is invalid.
        /// </para>
        /// </summary>
        QL_ERROR_INVALID_TARGET_ID = 4,

        /// <summary>
        /// <para>
        /// The password for the device is not valid.
        /// </para>
        /// </summary>
        QL_ERROR_INVALID_PASSWORD = 5,

        /// <summary>
        /// <para>
        /// The input path is invalid.
        /// </para>
        /// </summary>
        QL_ERROR_INVALID_PATH = 6,

        /// <summary>
        /// <para>
        /// The input duration is invalid.
        /// </para>
        /// </summary>
        QL_ERROR_INVALID_DURATION = 7,

        /// <summary>
        /// <para>
        /// The input pointer is NULL.
        /// </para>
        /// </summary>
        QL_ERROR_INVALID_POINTER = 8,

        /// <summary>
        /// <para>
        /// The timeout time was reached.
        /// </para>
        /// </summary>
        QL_ERROR_TIMEOUT_ELAPSED = 9,

        /// <summary>
        /// <para>
        /// An internal error occurred. Contact EyeTech for further help.
        /// </para>
        /// </summary>
        QL_ERROR_INTERNAL_ERROR = 10,

        /// <summary>
        /// <para>
        /// The input buffer is is not large enough.
        /// </para>
        /// </summary>
        QL_ERROR_BUFFER_TOO_SMALL = 11,

        /// <summary>
        /// <para>
        /// The calibration container has not been initialized.
        /// </para>
        /// </summary>
        /// <seealso cref="QuickLink2API.QLCalibration_Initialize" />
        QL_ERROR_CALIBRAION_NOT_INITIALIZED = 12,

        /// <summary>
        /// <para>
        /// The device has not been started.
        /// </para>
        /// </summary>
        /// <seealso cref="QuickLink2API.QLDevice_Start"/>
        QL_ERROR_DEVICE_NOT_STARTED = 13,

        /// <summary>
        /// <para>
        /// The setting is not supported by the given device.
        /// </para>
        /// </summary>
        /// <seealso cref="QuickLink2API.QLDevice_IsSettingSupported" />
        QL_ERROR_NOT_SUPPORTED = 14,

        /// <summary>
        /// <para>
        /// The setting is not in the settings container.
        /// </para>
        /// </summary>
        QL_ERROR_NOT_FOUND = 15,

        /// <summary>
        /// <para>
        /// The API has detected that an unauthorized application is running on the computer. This occurs
        /// most often when a device is being used in a way that is prohibited by the original sales
        /// agreement of the device. For further information please contact EyeTech.
        /// </para>
        /// </summary>
        QL_ERROR_UNAUTHORIZED_APPLICATION_RUNNING = 16,

        /// <summary>
        /// <para>
        /// The input device group ID is invalid.
        /// </para>
        /// </summary>
        QL_ERROR_INVALID_DEVICE_GROUP_ID = 17
    }

    /// <summary>
    /// <para>
    /// Values that identify an eye.
    /// </para>
    /// </summary>
    /// <seealso cref="QuickLink2API.QLCalibration_GetScoring" />
    /// <seealso cref="QuickLink2API.QLCalibration_AddBias" />
    public enum QLEyeType
    {
        /// <summary>
        /// <para>
        /// The left eye.
        /// </para>
        /// </summary>
        QL_EYE_TYPE_LEFT = 0,

        /// <summary>
        /// <para>
        /// The right eye.
        /// </para>
        /// </summary>
        QL_EYE_TYPE_RIGHT = 1
    }

    /// <summary>
    /// <para>
    /// Values that identify different indicator modes.
    /// </para>
    /// </summary>
    /// <seealso cref="QuickLink2API.QLDevice_SetIndicator" />
    /// <seealso cref="QuickLink2API.QLDevice_GetIndicator" />
    public enum QLIndicatorMode
    {
        /// <summary>
        /// <para>
        /// The indicator will be off constantly.
        /// </para>
        /// </summary>
        QL_INDICATOR_MODE_OFF = 0,

        /// <summary>
        /// <para>
        /// The indicator will be on constantly.
        /// </para>
        /// </summary>
        QL_INDICATOR_MODE_ON = 1,

        /// <summary>
        /// <para>
        /// The indicator will show the current tracking status of the left eye.
        /// </para>
        /// </summary>
        QL_INDICATOR_MODE_LEFT_EYE_STATUS = 2,

        /// <summary>
        /// <para>
        /// The indicator will show the current tracking status of the right eye.
        /// </para>
        /// </summary>
        QL_INDICATOR_MODE_RIGHT_EYE_STATUS = 3,

        /// <summary>
        /// <para>
        /// The indicator will show the filtered current tracking status of the left eye. This will
        /// prevent the indicator from flickering if the eye was lost for only a couple of frames.
        /// </para>
        /// </summary>
        QL_INDICATOR_MODE_LEFT_EYE_STATUS_FILTERED = 4,

        /// <summary>
        /// <para>
        /// The indicator will show the filtered current tracking status of the right eye. This will
        /// prevent the indicator from flickering if the eye was lost for only a couple of frames.
        /// </para>
        /// </summary>
        QL_INDICATOR_MODE_RIGHT_EYE_STATUS_FILTERED = 5
    }

    /// <summary>
    /// <para>
    /// Values that identify an indicator light.
    /// </para>
    /// </summary>
    /// <seealso cref="QuickLink2API.QLDevice_SetIndicator" />
    /// <seealso cref="QuickLink2API.QLDevice_GetIndicator" />
    public enum QLIndicatorType
    {
        /// <summary>
        /// <para>
        /// The left indicator light.
        /// </para>
        /// </summary>
        QL_INDICATOR_TYPE_LEFT = 0,

        /// <summary>
        /// <para>
        /// The right indicator light.
        /// </para>
        /// </summary>
        QL_INDICATOR_TYPE_RIGHT = 1
    }

    /// <summary>
    /// <para>
    /// Values that identify what data type a pointer points to.
    /// </para>
    /// </summary>
    /// <seealso cref="QuickLink2API.QLSettings_SetValue" />
    /// <seealso cref="QuickLink2API.QLSettings_GetValue" />
    public enum QLSettingType
    {
        /// <summary>
        /// <para>
        /// A c/c++ int type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_INT = 0,

        /// <summary>
        /// <para>
        /// An 8-bit wide integer type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_INT8 = 1,

        /// <summary>
        /// <para>
        /// A 16-bit wide integer type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_INT16 = 2,

        /// <summary>
        /// <para>
        /// A 32-bit wide integer type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_INT32 = 3,

        /// <summary>
        /// <para>
        /// A 64-bit wide integer type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_INT64 = 4,

        /// <summary>
        /// <para>
        /// A c/c++ unsigned int type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_UINT = 5,

        /// <summary>
        /// <para>
        /// An 8-bit wide unsigned integer type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_UINT8 = 6,

        /// <summary>
        /// <para>
        /// An 16-bit wide unsigned integer type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_UINT16 = 7,

        /// <summary>
        /// <para>
        /// An 32-bit wide unsigned integer type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_UINT32 = 8,

        /// <summary>
        /// <para>
        /// An 64-bit wide unsigned integer type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_UINT64 = 9,

        /// <summary>
        /// <para>
        /// A c/c++ float type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_FLOAT = 10,

        /// <summary>
        /// <para>
        /// A c/c++ double type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_DOUBLE = 11,

        /// <summary>
        /// <para>
        /// A c/c++ bool type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_BOOL = 12,

        /// <summary>
        /// <para>
        /// A c/c++ char* type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_STRING = 13,

        /// <summary>
        /// <para>
        /// A c/c++ void* type
        /// </para>
        /// </summary>
        QL_SETTING_TYPE_VOID_POINTER = 14
    }

    #endregion Enumerations

    #region Structures

    /// <summary>
    /// <para>
    /// Data that identifies a calibration target.
    /// </para>
    /// </summary>
    /// <seealso cref="QuickLink2API.QLCalibration_GetTargets" />
    [StructLayout(LayoutKind.Sequential)]
    public struct QLCalibrationTarget
    {
        /// <summary>
        /// <para>
        /// An identifying value used to reference the target. This is unique for each target in a given
        /// calibration container.
        /// </para>
        /// </summary>
        public System.Int32 targetId;

        /// <summary>
        /// <para>
        /// The x position of the target. This is in percentage of the horizontal area to be
        /// calibrated (0.-100.)
        /// </para>
        /// </summary>
        public System.Single x;

        /// <summary>
        /// <para>
        /// The y position of the target. This is in percentage of the vertical area to be calibrated
        /// (0.- 100.)
        /// </para>
        /// </summary>
        public System.Single y;
    }

    /// <summary>
    /// <para>
    /// Values that identify the quality of a calibration at a target.
    /// </para>
    /// </summary>
    /// <seealso cref="QuickLink2API.QLCalibration_GetScoring" />
    [StructLayout(LayoutKind.Sequential)]
    public struct QLCalibrationScore
    {
        /// <summary>
        /// <para>
        /// The x offset from the target position for the calibrated gaze point. This is in percentage
        /// of the horizontal area to be calibrated (0.-100.). Negative are to the left. Positive values
        /// are to the right.
        /// </para>
        /// </summary>
        public System.Single x;

        /// <summary>
        /// <para>
        /// The y offset from the target position for the calibrated gaze point. This is in percentage
        /// of the vertical area to be calibrated( (0.-100.). Negative are above. Positive values are
        /// below.
        /// </para>
        /// </summary>
        public System.Single y;

        /// <summary>
        /// <para>
        /// The magnitude of the distance from the calibrated gaze point to the actual target position.
        /// This can be used to identify which target has the worst calibration.
        /// </para>
        /// </summary>
        public System.Single score;
    }

    /// <summary>
    /// <para>
    /// Device specific information
    /// </para>
    /// </summary>
    /// <seealso cref="QuickLink2API.QLDevice_GetInfo" />
    [StructLayout(LayoutKind.Sequential)]
    public struct QLDeviceInfo
    {
        /// <summary>
        /// <para>
        /// The actual sensor width in pixels.
        /// </para>
        /// </summary>
        public System.Int32 sensorWidth;

        /// <summary>
        /// <para>
        /// The actual sensor height in pixels.
        /// </para>
        /// </summary>
        public System.Int32 sensorHeight;

        /// <summary>
        /// <para>
        /// The serial unique serial number of the device.
        /// </para>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public System.String serialNumber;

        /// <summary>
        /// <para>
        /// The EyeTech model name of the camera.
        /// </para>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public System.String modelName;
    }

    /// <summary>
    /// <para>
    /// An x-y pair of type double.
    /// </para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct QLXYPairDouble
    {
        /// <summary>
        /// <para>
        /// An x-coordinate of type double.
        /// </para>
        /// </summary>
        public System.Double x;

        /// <summary>
        /// <para>
        /// A y-coordinate of type double.
        /// </para>
        /// </summary>
        public System.Double y;
    }

    /// <summary>
    /// <para>
    /// An x-y pair of type float.
    /// </para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct QLXYPairFloat
    {
        /// <summary>
        /// <para>
        /// An x-coordinate of type float.
        /// </para>
        /// </summary>
        public System.Single x;

        /// <summary>
        /// <para>
        /// A y-coordinate of type float.
        /// </para>
        /// </summary>
        public System.Single y;
    }

    /// <summary>
    /// <para>
    /// Information describing a rectangle.
    /// </para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct QLRectInt
    {
        /// <summary>
        /// <para>
        /// The x coordinate of the upper left corner of the rectangle.
        /// </para>
        /// </summary>
        public System.Int32 x;

        /// <summary>
        /// <para>
        /// The y coordinate of the upper left corner of the rectangle.
        /// </para>
        /// </summary>
        public System.Int32 y;

        /// <summary>
        /// <para>
        /// The width of the rectangle.
        /// </para>
        /// </summary>
        public System.Int32 width;

        /// <summary>
        /// <para>
        /// The height of the rectangle.
        /// </para>
        /// </summary>
        public System.Int32 height;
    }

    /// <summary>
    /// <para>
    /// Information/data about an image
    /// </para>
    /// </summary>
    /// <seealso cref="QLRectInt"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct QLImageData
    {
        /// <summary>
        /// <para>
        /// The pixel data of the image. The data is single channel grey-scale with 8-bits per pixel.
        /// </para>
        /// </summary>
        public IntPtr PixelData;

        /// <summary>
        /// <para>
        /// The width in pixels of the image. This can be different than the sensorWidth of the device
        /// if binning is turned on.
        /// </para>
        /// </summary>
        public System.Int32 Width;

        /// <summary>
        /// <para>
        /// The height in pixels of the image. This can be different than the sensorHeight of the device
        /// if binning is turned on.
        /// </para>
        /// </summary>
        public System.Int32 Height;

        /// <summary>
        /// <para>
        /// The timestamp of the frame. It is the number of milliseconds from when the computer was
        /// started.
        /// </para>
        /// </summary>
        public System.Double Timestamp;

        /// <summary>
        /// <para>
        /// The gain value of the image.
        /// </para>
        /// </summary>
        public System.Int32 Gain;

        /// <summary>
        /// <para>
        /// A number to identify a frame. Checking for non consecutive numbers from frame to frame can
        /// determine if a frame was lost.
        /// </para>
        /// </summary>
        public System.Int32 FrameNumber;

        /// <summary>
        /// <para>
        /// Information describing the location and size of the region of interest. If the device is not
        /// in region of interest mode then the x and y values will be zero and the width and height
        /// values will equal the width and height of the entire image.
        /// </para>
        /// </summary>
        /// <seealso cref="QLRectInt"/>
        public QLRectInt ROI;

#if (ISX64)

        /// <summary>
        /// <para>
        /// 14 void pointers of reserved space.
        /// </para>
        /// <para>
        /// Note: Void pointers are 4-bytes on x86, and 8-bytes on x64.
        /// </para>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 112)]
#else

        /// <summary>
        /// <para>
        /// 12 void pointers of reserved space.
        /// </para>
        /// <para>
        /// Note: Void pointers are 4-bytes on x86, and 8-bytes on x64.
        /// </para>
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
#endif
        public System.String Reserved;
    }

    /// <summary>
    /// <para>
    /// Eye specific data.
    /// </para>
    /// </summary>
    /// <seealso cref="QLXYPairFloat"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct QLEyeData
    {
        /// <summary>
        /// <para>
        /// Indicates whether the eye is found in the image. If this is false then the rest of the data
        /// for the eye is not valid.
        /// </para>
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public System.Boolean Found;

        /// <summary>
        /// <para>
        /// Indicates whether the eye is calibrated. If this is false then the GazePoint is not valid
        /// for the eye.
        /// </para>
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public System.Boolean Calibrated;

        /// <summary>
        /// <para>
        /// The diameter of the pupil in millimeters. The resolution of this measurement is .02mm. If
        /// the eye is found and the pupil diameter is less than zero then the diameter could not be
        /// determined and it's value should not be used for that frame.
        /// </para>
        /// </summary>
        public System.Single PupilDiameter;

        /// <summary>
        /// <para>
        /// The position in pixels of the image of the center of the pupil.
        /// </para>
        /// </summary>
        /// <seealso cref="QLXYPairFloat"/>
        public QLXYPairFloat Pupil;

        /// <summary>
        /// <para>
        /// The position in pixels of the image of the center of one of the glints. Compare the x value
        /// to determine if this is the left of right glint.
        /// </para>
        /// </summary>
        /// <seealso cref="QLXYPairFloat"/>
        public QLXYPairFloat Glint0;

        /// <summary>
        /// <para>
        /// The position in pixels of the image of the center of one of the glints. Compare the x value
        /// to determine if this is the left of right glint.
        /// </para>
        /// </summary>
        /// <seealso cref="QLXYPairFloat"/>
        public QLXYPairFloat Glint1;

        /// <summary>
        /// <para>
        /// The position of the gaze point in percentage of the calibrated area. This value is not
        /// constrained to the calibrated area.
        /// </para>
        /// </summary>
        /// <seealso cref="QLXYPairFloat"/>
        public QLXYPairFloat GazePoint;

        /// <summary>
        /// <para>
        /// Void pointers are 4-bytes on x86, and 8-bytes on x64.  This field is 64 bytes of reserved
        /// space for x86, and 128 bytes of reserved space for x64.
        /// </para>
        /// </summary>
#if (ISX64)

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
#else

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
#endif
        public System.String Reserved;
    }

    /// <summary>
    /// <para>
    /// The averaged gaze point. These values intelligently average the left and right eye
    /// based on which eyes were found and previous data depending on filtering settings.
    /// </para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct QLWeightedGazePoint
    {
        /// <summary>
        /// <para>
        /// Indicates whether the weighted gaze point is available this frame. If this is false then the
        /// rest of the data is invalid.
        /// </para>
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public System.Boolean Valid;

        /// <summary>
        /// <para>
        /// The x position of the gaze point in percentage of the calibrated area.
        /// </para>
        /// </summary>
        public System.Single x;

        /// <summary>
        /// <para>
        /// The y position of the gaze point in percentage of the calibrated area.
        /// </para>
        /// </summary>
        public System.Single y;

        /// <summary>
        /// <para>
        /// The amount the left eye affected the weighted gaze point.
        /// </para>
        /// </summary>
        public System.Single LeftWeight;

        /// <summary>
        /// <para>
        /// The amount the right eye affected the weighted gaze point.
        /// </para>
        /// </summary>
        public System.Single RightWeight;

        /// <summary>
        /// <para>
        /// Void pointers are 4-bytes on x86, and 8-bytes on x64.  This field is 64 bytes of reserved
        /// space for x86, and 132 bytes of reserved space for x64.
        /// </para>
        /// </summary>
#if (ISX64)

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
#else

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
#endif
        public System.String Reserved;
    }

    /// <summary>
    /// <para>
    /// The complete data available in each frame.
    /// </para>
    /// </summary>
    /// <seealso cref="QLEyeData"/>
    /// <seealso cref="QLImageData"/>
    /// <seealso cref="QLWeightedGazePoint"/>
    /// <seealso cref="QuickLink2API.QLDevice_GetFrame" />
    [StructLayout(LayoutKind.Sequential)]
    public struct QLFrameData
    {
        /// <summary>
        /// <para>
        /// The image specific data.
        /// </para>
        /// </summary>
        /// <seealso cref="QLImageData"/>
        public QLImageData ImageData;

        /// <summary>
        /// <para>
        /// Left eye specific data.
        /// </para>
        /// </summary>
        /// <seealso cref="QLEyeData"/>
        public QLEyeData LeftEye;

        /// <summary>
        /// <para>
        /// Right eye specific data.
        /// </para>
        /// </summary>
        /// <seealso cref="QLEyeData"/>
        public QLEyeData RightEye;

        /// <summary>
        /// <para>
        /// An intelligently averaged single gaze point that is the best representation of the user's
        /// gaze.
        /// </para>
        /// </summary>
        /// <seealso cref="QLWeightedGazePoint"/>
        public QLWeightedGazePoint WeightedGazePoint;

        /// <summary>
        /// <para>
        /// The focus measurement of the eyes in the image. Higher values are better. Typical good
        /// values range between 15 and 19.
        /// </para>
        /// </summary>
        public System.Single Focus;

        /// <summary>
        /// <para>
        /// The distance from the device to the user in centimeters.
        /// </para>
        /// </summary>
        public System.Single Distance;

        /// <summary>
        /// <para>
        /// The current bandwidth of the device.
        /// </para>
        /// </summary>
        public System.Int32 Bandwidth;

        /// <summary>
        /// <para>
        /// The ID of the device that is the source for the current frame.
        /// </para>
        /// </summary>
        public System.Int32 DeviceId;

        /// <summary>
        /// <para>
        /// Void pointers are 4-bytes on x86, and 8-bytes on x64.  This field is 56 bytes of reserved
        /// space for x86, and 120 bytes of reserved space for x64.
        /// </para>
        /// </summary>
#if (ISX64)

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 120)]
#else

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 56)]
#endif
        public System.String Reserved;
    }

    #endregion Structures
}