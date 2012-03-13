#region License

/* EyeLogger : Dumps a CSV log from an EyeTech eye tracker to the console.
 *
 * Copyright (c) 2010-2012 Justin Weaver
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
 * Description: Dumps a CSV log from an EyeTech eye tracker to the console.
 */

#endregion Header Comments

using System;
using System.Threading;
using QuickLinkDotNet;

namespace EyeLogger
{
    internal class Program
    {
        #region Configuration

        /// <summary>
        /// Minimum delay between successful frame reads (ms).
        /// </summary>
        private const int MinDelayBetweenReads = 250;

        /// <summary>
        /// Maximum time for the reader thread to block and wait for a new
        /// frame from the eye tracker before reporting error to the log and
        /// trying again.
        /// </summary>
        private const int MaxFrameWaitTime = 240;

        #endregion Configuration

        private static void Main(string[] args)
        {
            // Load Quick Glance; and load the QuickLink DLLs into our
            // address space.
            QuickLink QL;
            try
            {
                QL = new QuickLink();
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Failed to load QuickLink.  MSG: {0}.", e.Message));
                // Can't continue without QuickLink.
                return;
            }

            // Create an empty frame to hold newly read data.
            ImageData iDat = new ImageData();

            Console.WriteLine("Time, Width, Height" +
                ", Left Eye Found, Left Eye Calibrated, Left Eye Pupil X, Left Eye Pupil Y, Left Eye Pupil Diameter, Left Eye Glint1 X, Left Eye Glint1 Y, Left Eye Glint2 X, Left Eye Glint2 Y, Left Eye GazePoint X, Left Eye GazePoint Y" +
                ", Right Eye Found, Right Eye Calibrated, Right Eye Pupil X, Right Eye Pupil Y, Right Eye Pupil Diameter, Right Eye Glint1 X, Right Eye Glint1 Y, Right Eye Glint2 X, Right Eye Glint2 Y, Right Eye GazePoint X, Right Eye GazePoint Y");

            while (true)
            {
                // Read a new data sample.
                if (QL.GetImageData(MaxFrameWaitTime, ref iDat))
                {
                    string headerString = string.Format("{0},{1},{2}", iDat.Time, iDat.Width, iDat.Height);
                    string leftEyeString = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", iDat.LeftEye.Found, iDat.LeftEye.Calibrated, iDat.LeftEye.Pupil.x, iDat.LeftEye.Pupil.y, iDat.LeftEye.PupilDiameter, iDat.LeftEye.Glint1.x, iDat.LeftEye.Glint1.y, iDat.LeftEye.Glint2.x, iDat.LeftEye.Glint2.y, iDat.LeftEye.GazePoint.x, iDat.LeftEye.GazePoint.y);
                    string rightEyeString = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", iDat.RightEye.Found, iDat.RightEye.Calibrated, iDat.RightEye.Pupil.x, iDat.RightEye.Pupil.y, iDat.RightEye.PupilDiameter, iDat.RightEye.Glint1.x, iDat.RightEye.Glint1.y, iDat.RightEye.Glint2.x, iDat.RightEye.Glint2.y, iDat.RightEye.GazePoint.x, iDat.RightEye.GazePoint.y);

                    Console.WriteLine(string.Format("{0},{1},{2}", headerString, leftEyeString, rightEyeString));
                }

                if (MinDelayBetweenReads > 0)
                    Thread.Sleep(MinDelayBetweenReads);
            }
        }
    }
}