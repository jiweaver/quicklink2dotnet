#region License

/* Calibrate: Class to provide an easy method for auto-calibration of an
 * eye tracker device.
 *
 * Copyright (c) 2009-2012 EyeTech Digital Systems
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
 * Description: Class to provide an easy method for auto-calibration of an
 * eye tracker device.
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickLink2DotNet;

namespace QuickStart
{
    public static class Calibrate
    {
        public static bool AutoCalibrate(int deviceId, QLCalibrationType calibrationType, ref int calibrationId)
        {
            QLError qlerror = QLError.QL_ERROR_OK;

            //Initialize the calibration using the inputted data.
            qlerror = QuickLink2API.QLCalibration_Initialize(deviceId, calibrationId, calibrationType);

            // If the calibrationId was not valid then create a new calibration container and use it.
            if (qlerror == QLError.QL_ERROR_INVALID_CALIBRATION_ID)
            {
                QuickLink2API.QLCalibration_Create(0, out calibrationId);
                qlerror = QuickLink2API.QLCalibration_Initialize(deviceId, calibrationId, calibrationType);
            }

            // If the initialization failed then print an error and return false.
            if (qlerror == QLError.QL_ERROR_INVALID_DEVICE_ID)
            {
                System.Console.WriteLine("QLCalibration_Initialize() failed with error code {0}.", qlerror);
                return false;
            }

            //Create a buffer for the targets. This just needs to be large enough to hold the targets.
            const int bufferSize = 20;
            int numTargets = bufferSize;
            QLCalibrationTarget[] targets = new QLCalibrationTarget[bufferSize];

            //Get the targets.  After the call, numTargets will contain the number of actual targets.
            qlerror = QuickLink2API.QLCalibration_GetTargets(calibrationId, ref numTargets, targets);

            // If the buffer was not large enough then print an error and return false.
            if (qlerror == QLError.QL_ERROR_BUFFER_TOO_SMALL)
            {
                System.Console.WriteLine("The target buffer is too small.");
                return false;
            }

            // Create a window for doing the calibration.
            CalibrationForm calibrationForm = new CalibrationForm();
            calibrationForm.PerformCalibration(calibrationId, numTargets, targets);

            System.Console.WriteLine("Do you want to improve the calibration? y/n");
            while (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                calibrationForm.ImproveCalibration(calibrationId, numTargets, targets);
                System.Console.WriteLine("Do you want to improve the calibration? y/n");
            }

            QuickLink2API.QLCalibration_Finalize(calibrationId);

            return true;
        }
    }
}