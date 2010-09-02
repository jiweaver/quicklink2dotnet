/* QuickLinkAPI4NET_Example : An example of how to call QuickLink's API.
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

/* File: QuickLinkAPI4NET_Example.cs
 * Author: Justin Weaver (Sep 2010)
 * Revision: $Rev$
 * Description: A example of calling QuickLink's API.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickLinkAPI4NET;
using System.Threading;

namespace QuickLinkAPI4NET_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // Load the QuickLink DLLs into our address space.
                QuickLink QL = new QuickLink();

                // Now we can call QuickLink's methods.
                if (!QuickLink.GetQGOnFlag())
                {
                    Console.WriteLine("Quick Glance is not running.");
                }
                else
                {
                    // Read a new data sample.
                    ImageData d = new ImageData();
                    if (QuickLink.GetImageData(0, ref d))  //0 means: no blocking
                    {
                        EyeData leftData = d.LeftEye;

                        string leftOutText = "[Left Eye] Found:" + leftData.Found;

                        // The value of Calibrated is only valid when Found is true.
                        if (leftData.Found)
                        {
                            leftOutText += ", Calibrated:" + leftData.Calibrated;

                            // The value of GazePoint only valid if Calibrated is true.
                            if (leftData.Calibrated)
                                leftOutText += ", Gaze-Point:" + leftData.GazePoint.x + "," + leftData.GazePoint.y;
                        }

                        EyeData rightData = d.LeftEye;

                        string rightOutText = "[Right Eye] Found:" + rightData.Found;

                        // The value of Calibrated is only valid when Found is true.
                        if (rightData.Found)
                        {
                            rightOutText += ", Calibrated:" + rightData.Calibrated;

                            // The value of GazePoint only valid if Calibrated is true.
                            if (rightData.Calibrated)
                                rightOutText += ", Gaze-Point:" + rightData.GazePoint.x + "," + rightData.GazePoint.y;
                        }

                        Console.WriteLine(leftOutText);
                        Console.WriteLine(rightOutText);
                        Console.WriteLine();
                    }
                }

                // Sleep a while.
                Thread.Sleep(1000);
            }
        }
    }
}
