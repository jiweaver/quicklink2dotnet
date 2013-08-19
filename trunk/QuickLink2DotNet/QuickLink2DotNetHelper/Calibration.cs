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
    public partial class QLHelper
    {
        private const int DefaultCalibrationTargetDuration = 1500; // milliseconds.
        private const QLCalibrationType DefaultCalibrationType = QLCalibrationType.QL_CALIBRATION_TYPE_9;
        private const int DefaultDeviceDistance = 55; // centimeters.

        private static bool CalibrationTypeToNumberOfPoints(QLCalibrationType calibrationType, out int numberOfPoints)
        {
            switch (calibrationType)
            {
                case QLCalibrationType.QL_CALIBRATION_TYPE_5:
                    numberOfPoints = 5;
                    return true;

                case QLCalibrationType.QL_CALIBRATION_TYPE_9:
                    numberOfPoints = 9;
                    return true;

                case QLCalibrationType.QL_CALIBRATION_TYPE_16:
                    numberOfPoints = 16;
                    return true;

                default:
                    numberOfPoints = 0;
                    return false;
            }
        }

        private static bool NumberOfPointsToCalibrationType(int numberOfPoints, out QLCalibrationType calibrationType)
        {
            switch (numberOfPoints)
            {
                case 5:
                    calibrationType = QLCalibrationType.QL_CALIBRATION_TYPE_5;
                    return true;

                case 9:
                    calibrationType = QLCalibrationType.QL_CALIBRATION_TYPE_9;
                    return true;

                case 16:
                    calibrationType = QLCalibrationType.QL_CALIBRATION_TYPE_16;
                    return true;

                default:
                    calibrationType = DefaultCalibrationType;
                    return false;
            }
        }

        // Returns false on 'q' for cancellation of configuration; otherwise
        // returns true.
        private static bool PromptForCalibrationType(out QLCalibrationType calibrationType)
        {
            calibrationType = DefaultCalibrationType;

            while (true)
            {
                // Flush the input buffer.
                while (Console.KeyAvailable) { Console.ReadKey(true); }

                Console.Write("  Perform 5, 9, or 16-point calibration ({0}): ", (calibrationType == QLCalibrationType.QL_CALIBRATION_TYPE_5) ? "5" : (calibrationType == QLCalibrationType.QL_CALIBRATION_TYPE_9) ? "9" : "16");

                string input = Console.ReadLine();

                if (input.Length == 0)
                {
                    return true;
                }
                else if (input.ToLower().Equals("q"))
                {
                    return false;
                }
                else
                {
                    try
                    {
                        int numberOfPoints = int.Parse(input);
                        if (NumberOfPointsToCalibrationType(numberOfPoints, out calibrationType))
                        {
                            return true;
                        }
                    }
                    catch (ArgumentNullException) { }
                    catch (FormatException) { }
                    catch (OverflowException) { }
                }
            }
        }

        // Returns false on 'q' for cancellation of configuration; otherwise
        // returns true.
        private static bool PromptForTargetDuration(out int targetDuration)
        {
            targetDuration = DefaultCalibrationTargetDuration;

            while (true)
            {
                // Flush the input buffer.
                while (Console.KeyAvailable) { Console.ReadKey(true); }

                Console.Write("  Target duration in milliseconds ({0}ms): ", targetDuration);

                string input = Console.ReadLine();

                if (input.Length == 0)
                {
                    return true;
                }
                else if (input.ToLower().Equals("q"))
                {
                    return false;
                }
                else
                {
                    try
                    {
                        int readNumber = int.Parse(input);
                        targetDuration = readNumber;
                        return true;
                    }
                    catch (ArgumentNullException) { }
                    catch (FormatException) { }
                    catch (OverflowException) { }
                }
            }
        }

        // Returns false on 'q' for cancellation of configuration; otherwise
        // returns true.
        private static bool PromptForDeviceDistance(out int deviceDistance)
        {
            deviceDistance = DefaultDeviceDistance;

            while (true)
            {
                // Flush the input buffer.
                while (Console.KeyAvailable) { Console.ReadKey(true); }

                Console.Write("  Distance from device to user ({0}cm): ", deviceDistance);

                string input = Console.ReadLine();

                if (input.Length == 0)
                {
                    return true;
                }
                else if (input.ToLower().Equals("q"))
                {
                    return false;
                }
                else
                {
                    try
                    {
                        int readNumber = int.Parse(input);
                        deviceDistance = readNumber;
                        return true;
                    }
                    catch (ArgumentNullException) { }
                    catch (FormatException) { }
                    catch (OverflowException) { }
                }
            }
        }

        private static bool PromptToRecalibrate()
        {
            while (true)
            {
                // Flush the input buffer.
                while (Console.KeyAvailable) { Console.ReadKey(true); }

                Console.Write("  Recalibrate? (y/n): ");

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();

                if (keyInfo.Key == ConsoleKey.N)
                {
                    return false;
                }
                else if (keyInfo.Key == ConsoleKey.Y)
                {
                    return true;
                }
            }
        }

        private static bool PromptToApplyCalibration()
        {
            while (true)
            {
                // Flush the input buffer.
                while (Console.KeyAvailable) { Console.ReadKey(true); }

                Console.Write("  Apply? (y/n): ");

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();

                if (keyInfo.Key == ConsoleKey.N)
                {
                    return false;
                }
                else if (keyInfo.Key == ConsoleKey.Y)
                {
                    return true;
                }
            }
        }

        // Returns false on 'q' for cancellation of configuration; otherwise
        // returns true.
        private static bool PromptForShowVideoStream(out bool showVideoStream)
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to see the video stream from the camera?" + Environment.NewLine +
            "Viewing the video image will allow you:" + Environment.NewLine +
            "  1) Positioned the eye tracker so the user's eyes are centered and in the" + Environment.NewLine +
            "     upper half of the image." + Environment.NewLine +
            "  2) Focus the camera on the user's face.  Adjust the focus by turning the dial" + Environment.NewLine +
            "     around the camera lens.  Use the visible glint points in the user's pupils" + Environment.NewLine +
            "     to verify the image is in crisp focus." + Environment.NewLine +
            "  3) Check for ambient infrared light interference.  Place your palms over the" + Environment.NewLine +
            "     infrared lights on either side of the camera.  If you see anything but" + Environment.NewLine +
            "     blackness, then there is another source of infrared light in your" + Environment.NewLine +
            "     environment that could potentially interfere with accurate eye tracking" + Environment.NewLine +
            "     (e.g., The Sun, wide spectrum indoor lighting)." + Environment.NewLine +
            "  When you are finished viewing the video image, just hit the close button on" + Environment.NewLine +
            "  the window to continue device configuration.");

            showVideoStream = false;
            while (true)
            {
                // Flush the input buffer.
                while (Console.KeyAvailable) { Console.ReadKey(true); }

                Console.Write("View the video image? (y/n/q): ");
                ConsoleKeyInfo videoKeyInfo = Console.ReadKey();
                Console.WriteLine();

                if (videoKeyInfo.Key == ConsoleKey.Q)
                {
                    return false;
                }
                else if (videoKeyInfo.Key == ConsoleKey.N)
                {
                    showVideoStream = false;
                    return true;
                }
                else if (videoKeyInfo.Key == ConsoleKey.Y)
                {
                    showVideoStream = true;
                    return true;
                }
            }
        }

        private static bool LoadDeviceDistance(string settingsFilename, out int value)
        {
            value = 0;

            int settingsId = 0;
            QLError error = QuickLink2API.QLSettings_Load(settingsFilename, ref settingsId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_Load() returned {0}.", error.ToString());
                return false;
            }

            error = QuickLink2API.QLSettings_GetValueInt(settingsId, QL_SETTINGS.QL_SETTING_DEVICE_DISTANCE, out value);
            if (error != QLError.QL_ERROR_OK && error != QLError.QL_ERROR_INVALID_PATH && error != QLError.QL_ERROR_NOT_FOUND)
            {
                Console.WriteLine("QLSettings_GetValueInt() returned {0}.", error.ToString());
                return false;
            }

            return true;
        }

        private static bool SaveDeviceDistance(string settingsFilename, int value)
        {
            int settingsId = 0;
            QLError error = QuickLink2API.QLSettings_Load(settingsFilename, ref settingsId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_Load() returned {0}.", error.ToString());
                return false;
            }

            error = QuickLink2API.QLSettings_SetValueInt(settingsId, QL_SETTINGS.QL_SETTING_DEVICE_DISTANCE, value);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_SetValueInt() returned {0}.", error.ToString());
                return false;
            }

            error = QuickLink2API.QLSettings_Save(settingsFilename, settingsId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_Save() returned {0}.", error.ToString());
                return false;
            }

            return true;
        }

        private static bool ApplyDeviceDistance(int deviceId, int value)
        {
            int settingsId;
            QLError error = QuickLink2API.QLSettings_Create(0, out settingsId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_Create() returned {0}.", error.ToString());
                return false;
            }

            error = QuickLink2API.QLSettings_SetValueInt(settingsId, QL_SETTINGS.QL_SETTING_DEVICE_DISTANCE, value);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_SetValueInt() returned {0}.", error.ToString());
                return false;
            }

            error = QuickLink2API.QLDevice_ImportSettings(deviceId, settingsId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_ImportSettings() returned {0}.", error.ToString());
                return false;
            }

            return true;
        }

        private bool ShowVideoStream()
        {
            // Start the device.
            QLError error = QuickLink2API.QLDevice_Start(this.DeviceId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_Start() returned {0}.", error.ToString());
                return false;
            }

            using (QLHelper.VideoForm videoForm = new QLHelper.VideoForm(this))
            {
                videoForm.ShowDialog();
            }

            error = QuickLink2API.QLDevice_Stop(this.DeviceId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_Stop() returned {0}.", error.ToString());
                return false;
            }

            return true;
        }

        private static bool Calibrate(int deviceId, string calibrationFilename, int deviceDistance, QLCalibrationType calibrationType, int targetDuration)
        {
            // Start the device.
            QLError error = QuickLink2API.QLDevice_Start(deviceId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_Start() returned {0}.", error.ToString());
                return false;
            }

            bool calibrationSuccessful = false;
            using (CalibrationForm calibrationForm = new CalibrationForm(deviceId, calibrationType, targetDuration))
            {
                if (calibrationForm.Calibrate())
                {
                    // Calculate the total average score.
                    float avg = 0;
                    for (int i = 0; i < calibrationForm.LeftScores.Length; i++)
                    {
                        avg += calibrationForm.LeftScores[i].score;
                        avg += calibrationForm.RightScores[i].score;
                    }
                    avg /= (float)calibrationForm.LeftScores.Length * 2f;

                    Console.WriteLine("Calibration Score: {0}.", avg);

                    while (true)
                    {
                        // Flush the input buffer.
                        while (Console.KeyAvailable) { Console.ReadKey(true); }

                        if (!PromptToApplyCalibration())
                        {
                            Console.WriteLine("Not applying calibration.");
                            calibrationSuccessful = false;
                            break;
                        }
                        else
                        {
                            error = QuickLink2API.QLCalibration_Finalize(calibrationForm.CalibrationId);
                            if (error != QLError.QL_ERROR_OK)
                            {
                                Console.WriteLine("QLCalibration_Finalize() retuned {0}.", error.ToString());
                                calibrationSuccessful = false;
                                break;
                            }

                            error = QuickLink2API.QLDevice_ApplyCalibration(deviceId, calibrationForm.CalibrationId);
                            if (error != QLError.QL_ERROR_OK)
                            {
                                Console.WriteLine("QLCalibration_ApplyCalibration() returned {0}", error.ToString());
                                calibrationSuccessful = false;
                                break;
                            }

                            error = QuickLink2API.QLCalibration_Save(calibrationFilename, calibrationForm.CalibrationId);
                            if (error != QLError.QL_ERROR_OK)
                            {
                                Console.WriteLine("QLCalibration_Save() returned {0}", error.ToString());
                                calibrationSuccessful = false;
                                break;
                            }

                            calibrationSuccessful = true;
                            break;
                        }
                    }
                }
            }

            error = QuickLink2API.QLDevice_Stop(deviceId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_Stop() returned {0}.", error.ToString());
                return false;
            }

            return calibrationSuccessful;
        }

        /// <summary>
        /// <para>
        /// Loads eye tracker device calibration from the location specified by
        /// <see cref="CalibrationFilename"/>, or performs interactive calibration--if calibration cannot
        /// be loaded from the file--and saves it to the calibration file.  Optionally, (when the
        /// <paramref name="promptToRecalibrate"/> parameter is true) this method will prompt the user to
        /// re-perform interactive calibration even when the calibration has successfully been loaded
        /// from the calibration file.
        /// </para>
        /// <para>
        /// If interactive calibration is chosen (via the <paramref name="promptToRecalibrate"/>
        /// parameter being set to true) or required (when no stored calibration file is present), the
        /// user is prompted to choose 5, 9, or 16 point calibration, choose the duration for
        /// display of each target (in milliseconds), and specify the device's distance from the user.
        /// </para>
        /// <para>
        /// During interactive calibration, the user will be given the option to view the live video
        /// stream from the device in order to adjust camera focus and orientation for best results.
        /// </para>
        /// <para>
        /// Interactive calibration may attempt to improve its results by redisplaying some targets
        /// several times immediately after the initial, ordered target sequence has been displayed.
        /// During this improvement process, warning messages may appear on the console; this is normal
        /// and informative only.
        /// </para>
        /// <para>
        /// After a successful interactive calibration procedure, the user will be shown a score and
        /// prompted to apply or discard the results.  The score is the average of the left and right
        /// scores for each target.  Each left and right score for a target is the magnitude of the
        /// distance of the projected gaze location from the center of the target.  In other words, a
        /// lower score is better.
        /// </para>
        /// </summary>
        /// <seealso cref="SetupCalibration()"/>
        /// <param name="promptToRecalibrate">
        /// When true, the user will be asked if they would like to re-perform the eye tracker device's
        /// calibration even when the calibration was successfully loaded from the calibration file.
        /// </param>
        /// <returns>
        /// <para>
        /// If calibration was loaded from file, this method returns true when the calibration has been
        /// successfully applied to the device.
        /// </para>
        /// <para>
        /// If calibration was not loaded from the device, then this method returns true when the
        /// calibration procedure has been completed, the calibration has been applied to the device, the
        /// calibration has been saved to the calibration file, and the eye tracker has been successfully
        /// stopped.
        /// </para>
        /// <para>
        /// If calibration was successfully loaded and applied from the calibration file, but the user
        /// has chosen to re-perform the calibration procedure, then true is returned if the user cancels
        /// the re-calibration before the actual calibration sequence begins, or when the newly performed
        /// calibration has been completed, the newly performed calibration has been applied to the
        /// device, the newly performed calibration has been saved to the calibration file, and the eye
        /// tracker device has been stopped.
        /// </para>
        /// <para>
        /// Otherwise, this method returns false.
        /// </para>
        /// </returns>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public bool SetupCalibration(bool promptToRecalibrate)
        {
            int deviceDistance;
            if (!LoadDeviceDistance(this.SettingsFilename, out deviceDistance))
            {
                return false;
            }

            if (deviceDistance == 0)
            {
                deviceDistance = DefaultDeviceDistance;
                if (!SaveDeviceDistance(this.SettingsFilename, deviceDistance))
                {
                    return false;
                }
            }

            if (!ApplyDeviceDistance(this.DeviceId, deviceDistance))
            {
                return false;
            }

            bool calibrationLoadedFromFile = false;

            // Load the calibration out of a file into a new calibration container.
            int calibrationID = -1;
            QLError error = QuickLink2API.QLCalibration_Load(this.CalibrationFilename, ref calibrationID);
            if (error == QLError.QL_ERROR_OK)
            {
                // Apply the loaded calibration to the device.
                error = QuickLink2API.QLDevice_ApplyCalibration(this.DeviceId, calibrationID);
                if (error == QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("Calibration loaded from file.");
                    if (!promptToRecalibrate || !PromptToRecalibrate())
                    {
                        // Loaded from file and no prompt for recalibration
                        // requested, or recalibration cancelled.
                        return true;
                    }
                    calibrationLoadedFromFile = true;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Beginning calibration setup.");

            bool showVideoStream;
            if (!PromptForShowVideoStream(out showVideoStream))
            {
                // Cancelled.
                return calibrationLoadedFromFile;
            }
            else if (showVideoStream)
            {
                if (!ShowVideoStream())
                {
                    // Error.
                    return false;
                }
            }

            Console.WriteLine();
            Console.WriteLine("[Calibration Parameters]");
            Console.WriteLine("  Press ENTER to use (default), or enter 'q' to quit.");

            int newDeviceDistance;
            if (!PromptForDeviceDistance(out newDeviceDistance))
            {
                // Cancelled.
                return calibrationLoadedFromFile;
            }
            else if (newDeviceDistance != deviceDistance)
            {
                deviceDistance = newDeviceDistance;
                if (!SaveDeviceDistance(this.SettingsFilename, deviceDistance))
                {
                    // Error.
                    return false;
                }
                else if (!ApplyDeviceDistance(this.DeviceId, newDeviceDistance))
                {
                    // Error.
                    return false;
                }
            }

            QLCalibrationType calibrationType;
            if (!PromptForCalibrationType(out calibrationType))
            {
                // Cancelled.
                return calibrationLoadedFromFile;
            }

            int targetDuration;
            if (!PromptForTargetDuration(out targetDuration))
            {
                // Cancelled.
                return calibrationLoadedFromFile;
            }

            Console.WriteLine();
            Console.WriteLine("Beginning calibration.");

            if (!Calibrate(this.DeviceId, this.CalibrationFilename, deviceDistance, calibrationType, targetDuration))
            {
                Console.WriteLine("Calibration failed.");
                return false;
            }
            else
            {
                Console.WriteLine("Calibration completed, applied, and saved.\n");
                return true;
            }
        }

        /// <summary>
        /// This is the same as calling <see cref="SetupCalibration(bool)"/> with the promptToRecalibrate parameter set to false.
        /// </summary>
        /// <returns>
        /// Same as <see cref="SetupCalibration(bool)"/> with the promptToRecalibrate parameter set to false.
        /// </returns>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public bool SetupCalibration()
        {
            return SetupCalibration(false);
        }
    }
}