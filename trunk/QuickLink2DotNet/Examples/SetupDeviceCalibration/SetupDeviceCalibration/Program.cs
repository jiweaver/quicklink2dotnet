#region License

/* QuickLink2DotNet SetupDeviceCalibration Example: Displays a list of
 * available devices and prompts the user to select one.  Then performs device
 * calibration.  The calibration is saved in the file
 * "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlcalibration.qlc" for later
 * use.
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
using System.Threading;
using System.Windows.Forms;
using QLExampleHelper;
using QuickLink2DotNet;

namespace SetupDeviceCalibration
{
    static class Program
    {
        private static string dirname = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QuickLink2DotNet");

        // The file used to store the password.
        private static string filename_Password = System.IO.Path.Combine(dirname, "qlsettings.txt");

        // The file used to store the calibration information.
        private static string filename_Calibration = System.IO.Path.Combine(dirname, "qlcalibration.qlc");

        private static QLCalibrationType defaultCalibrationType = QLCalibrationType.QL_CALIBRATION_TYPE_5;

        private static int defaultTargetDuration = 1500; // milliseconds.

        private static void WaitForKeyPress()
        {
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static bool StopDevice(int deviceID)
        {
            QLError error = QuickLink2API.QLDevice_Stop(deviceID);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_Stop() returned {0}", error.ToString());
                return false;
            }

            Console.WriteLine("Stopped Device.\n");
            return true;
        }

        private static bool ChooseCalibrationType(out QLCalibrationType calibrationType)
        {
            calibrationType = defaultCalibrationType;

            int numberOfPoints = -1;
            while (numberOfPoints == -1)
            {
                Console.Write("Perform 5, 9, or 16-point calibration? (5, 9, 16, or 'q' to quit): ");

                string input = Console.ReadLine();
                if (input.Equals("q") || input.Equals("Q"))
                {
                    break;
                }

                try
                {
                    numberOfPoints = int.Parse(input);
                }
                catch (ArgumentNullException) { }
                catch (FormatException) { }
                catch (OverflowException) { }

                switch (numberOfPoints)
                {
                    case 5:
                        calibrationType = QLCalibrationType.QL_CALIBRATION_TYPE_5;
                        break;

                    case 9:
                        calibrationType = QLCalibrationType.QL_CALIBRATION_TYPE_9;
                        break;

                    case 16:
                        calibrationType = QLCalibrationType.QL_CALIBRATION_TYPE_16;
                        break;

                    default:
                        numberOfPoints = -1;
                        break;
                }
            }

            if (numberOfPoints == -1)
            {
                Console.WriteLine("Calibration canceled.");
                return false;
            }

            return true;
        }

        private static bool ChooseTargetDuration(out int targetDuration)
        {
            targetDuration = defaultTargetDuration;

            int readNumber = -1;
            while (readNumber == -1)
            {
                Console.Write("Target duration in milliseconds ('q' to quit): ");

                string input = Console.ReadLine();
                if (input.Equals("q") || input.Equals("Q"))
                {
                    break;
                }

                try
                {
                    readNumber = int.Parse(input);
                }
                catch (ArgumentNullException) { }
                catch (FormatException) { }
                catch (OverflowException) { }

                if (readNumber <= 0)
                {
                    readNumber = -1;
                }
                else
                {
                    targetDuration = readNumber;
                    break;
                }
            }

            if (readNumber == -1)
            {
                Console.WriteLine("Calibration canceled.");
                return false;
            }

            return true;
        }

        private static void DisplayScores(CalibrationForm calibrationForm)
        {
            Console.WriteLine("Calibration Scores Per Target (Lower is Better):");
            Console.Write("  Left: ");
            float leftAvg = 0;
            for (int i = 0; i < calibrationForm.LeftScores.Length; i++)
            {
                Console.Write("{0:f}, ", calibrationForm.LeftScores[i].score);
                leftAvg += calibrationForm.LeftScores[i].score;
            }
            leftAvg /= (float)calibrationForm.LeftScores.Length;
            Console.WriteLine("Avg={0:f}", leftAvg);

            Console.Write("  Right: ");
            float rightAvg = 0;
            for (int i = 0; i < calibrationForm.RightScores.Length; i++)
            {
                Console.Write("{0:f}, ", calibrationForm.RightScores[i].score);
                rightAvg += calibrationForm.RightScores[i].score;
            }
            rightAvg /= (float)calibrationForm.RightScores.Length;
            Console.WriteLine("Avg={0:f}", rightAvg);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            int[] deviceIDs = QLHelper.GetDeviceIDs();
            int deviceID = QLHelper.ChooseDevice(deviceIDs);
            string password = QLHelper.ReadPasswordFromFile(deviceID, filename_Password);
            if (password == null)
            {
                WaitForKeyPress();
                return;
            }

            QLError error = QuickLink2API.QLDevice_SetPassword(deviceID, password);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLDevice_SetPassword() returned {0}.", error.ToString());
                WaitForKeyPress();
                return;
            }

            Console.WriteLine("Default calibration parameters: Calibration Type={0}, Target Duration={1}", defaultCalibrationType.ToString(), defaultTargetDuration);

            QLCalibrationType calibrationType = defaultCalibrationType;
            int targetDuration = defaultTargetDuration;

            Console.Write("Customize calibration parameters? (y/n): ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Console.WriteLine();
            if (keyInfo.Key == ConsoleKey.Y)
            {
                bool result = ChooseCalibrationType(out calibrationType);
                if (!result)
                {
                    WaitForKeyPress();
                    return;
                }

                result = ChooseTargetDuration(out targetDuration);
                if (!result)
                {
                    WaitForKeyPress();
                    return;
                }
            }

            // Start the device.
            error = QuickLink2API.QLDevice_Start(deviceID);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine(string.Format("QLDevice_Start() returned {0}", error.ToString()));
                WaitForKeyPress();
                return;
            }

            Console.WriteLine("Beginning calibration procedure.\n");

            using (CalibrationForm calibrationForm = new CalibrationForm(calibrationType, targetDuration))
            {
                bool result = calibrationForm.PerformCalibration(deviceID);

                if (!result)
                {
                    Console.WriteLine("Calibration failed.");
                    WaitForKeyPress();
                    return;
                }

                do
                {
                    DisplayScores(calibrationForm);
                    Console.Write("Try to improve? (y/n): ");
                    keyInfo = Console.ReadKey();
                    Console.WriteLine();
                    if (keyInfo.Key == ConsoleKey.Y)
                    {
                        result = calibrationForm.ImproveCalibration();
                    }
                }
                while (keyInfo.Key == ConsoleKey.Y);

                // Prompt to apply calibration.
                Console.Write("Apply? (y/n): ");
                keyInfo = Console.ReadKey();
                Console.WriteLine();
                if (keyInfo.Key != ConsoleKey.Y)
                {
                    Console.WriteLine("Not applying calibration.");
                    StopDevice(deviceID);
                    WaitForKeyPress();
                    return;
                }

                error = QuickLink2API.QLCalibration_Finalize(calibrationForm.CalibrationID);
                if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("QLCalibration_Finalize() retuned {0}.", error.ToString());
                    StopDevice(deviceID);
                    WaitForKeyPress();
                    return;
                }

                error = QuickLink2API.QLCalibration_Save(filename_Calibration, calibrationForm.CalibrationID);
                if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("QLCalibration_Save() returned {0}", error.ToString());
                    StopDevice(deviceID);
                    WaitForKeyPress();
                    return;
                }
            }

            Console.WriteLine("Calibration completed and successfully saved.\n");
            StopDevice(deviceID);
            WaitForKeyPress();
        }
    }
}