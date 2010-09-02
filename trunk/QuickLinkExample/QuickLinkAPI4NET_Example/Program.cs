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
            long outCount = 0;

            while (true)
            {
                QuickLink QL = new QuickLink();

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

                outCount++;

                Thread.Sleep(1000);
            }
        }
    }
}
