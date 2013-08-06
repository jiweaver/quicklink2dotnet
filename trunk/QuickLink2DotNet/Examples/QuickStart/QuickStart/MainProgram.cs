#region License

/* QuickLink2DotNet QuickStart Example: A very simple console example to
 * demonstrate initialization, calibration, and data collection from the eye
 * tracker.  The password is saved in the file
 * "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlsettings.txt" for later
 * use.  The calibration is saved in the file
 * "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlcalibration.qlc" for later
 * use.
 *
 * Copyright © 2009-2012 EyeTech Digital Systems
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

/* $Id$
 *
 * Authors: Brianna Peters <bpeters@eyetechds.com>
 * Date: May 2, 2012; 10:50 AM
 * Last modified May 24, 2012; 3:54 PM
 * Copyright © 2009-2012 EyeTech Digital Systems
 * support@eyetechds.com
 * Description: A simple example to demonstrate initialization, calibration,
 * and data collection from the eye tracker.
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickLink2DotNet;

namespace QuickStart
{
    static class MainProgram
    {
        private static string dirname = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QuickLink2DotNet");

        // The calibration file.
        private static string calibrationFilename = System.IO.Path.Combine(dirname, "qlcalibration.qlc");

        // The file used to store the password.
        private static string filename_Password = System.IO.Path.Combine(dirname, "qlsettings.txt");

        static void Main(string[] args)
        {
            int deviceId = Initialize.QL2Initialize(filename_Password);
            QLError error = QuickLink2API.QLDevice_Start(deviceId);

            if (error != QLError.QL_ERROR_OK)
            {
                System.Console.WriteLine("Device not started successfully!");
                System.Console.ReadLine();
                return;
            }

            System.Console.WriteLine("Press any key to begin calibration.");
            Console.ReadKey(true);

            //Calibrate the device
            int calibrationId = 0;
            if (Calibrate.AutoCalibrate(deviceId, QLCalibrationType.QL_CALIBRATION_TYPE_16, ref calibrationId))
            {
                System.Console.WriteLine("\n\nPress \'q\' to quit. \n");

                // If the calibration was successful then apply the calibration to the device.
                QuickLink2API.QLDevice_ApplyCalibration(deviceId, calibrationId);

                // Display the gaze information until the user quits.
                QLFrameData frameData = new QLFrameData();
                while ((!Console.KeyAvailable) || (Console.ReadKey(true).Key != ConsoleKey.Q))
                {
                    QuickLink2API.QLDevice_GetFrame(deviceId, 10000, ref frameData);
                    if (frameData.WeightedGazePoint.Valid)
                    {
                        System.Console.Write("\rX:{0:F}    Y:{1:F}", frameData.WeightedGazePoint.x,
                                                    frameData.WeightedGazePoint.y);
                    }
                }
            }
            else
            {
                System.Console.WriteLine("The calibration did not finish successfully!");
                System.Console.Read();
                QuickLink2API.QLDevice_Stop(deviceId);
                return;
            }

            // Stop the device.
            QuickLink2API.QLDevice_Stop(deviceId);

            return;
        }
    }
}