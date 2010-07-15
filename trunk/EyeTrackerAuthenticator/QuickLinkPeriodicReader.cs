/* File: QuickLinkPeriodicReader.cs
 * Author: Justin Weaver
 * Date: Jul 2010
 * Revision: $Rev$
 * Description: A class that spawns a thread to read raw data from the eye 
 * tracker at regular time intervals and perform a callback with the data as a
 * paramter.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using QuickLinkAPI4NET;

namespace EyeTrackerAuthenticator
{
    // This method will get a callback when a new ImageData is read.
    public delegate void NewImageDataCallbackType(ImageData iDat);

    public class QuickLinkPeriodicReader : IDisposable
    {
        /* The thread that will perform the periodic eye tracker reads and the
         * callBack.
         */
        private Thread _readerThread;

        // 'true' tells the thread to exit.
        private volatile bool _shouldStop;

        /* The amount of time (ms) to wait for a new ImageData.  This value is
         * used as the 'timeout' parameter in the read call to the QuickLink 
         * API.
         */
        private int _pollTimeout;

        /* Thread Parameter: The amount of time (ms) between wakes.  The thread
         * attempts to maintain a regular wake cycle by adjusting its sleep 
         * time each night according the the amount of time it spent working.
         */
        private int _pollPeriod;

        // The method to callback.
        private NewImageDataCallbackType _newImageDataCallbackMethod;

        public QuickLinkPeriodicReader(int pollPeriod, int pollTimeout, NewImageDataCallbackType callBackMethod)
        {
            //TODO- Make separate process with high priority to get very regular intervals?
            //Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            // Initialize with the specified paramters.
            this._pollPeriod = pollPeriod;
            this._pollTimeout = pollTimeout;
            this._newImageDataCallbackMethod = (callBackMethod != null) ? new NewImageDataCallbackType(callBackMethod) : null;

            this._shouldStop = false;

            // Create the periodic reader thread.
            this._readerThread = new Thread(new ThreadStart(this.ThreadTask));
            this._readerThread.IsBackground = true;

            // Start the thread and wait for it to get going.
            this._readerThread.Start();
            while (!this._readerThread.IsAlive) ;
        }

        private void ThreadTask()
        {
            int nextTime;
            ImageData iDat;

            while (!this._shouldStop)
            {
                // Calculate the next wake time (in ms).
                nextTime = System.Environment.TickCount;
                nextTime += this._pollPeriod;

                iDat = new ImageData();
                if (QuickLink.GetImageData((uint)this._pollTimeout, ref iDat))
                    /* We sucessfully read some data from the Eye tracker.  
                     * Perform the callback.
                     */
                    this._newImageDataCallbackMethod(iDat);

                // Sleep until next wake time.
                nextTime -= System.Environment.TickCount;
                Thread.Sleep(nextTime);
            }
        }

        // Tell the thread to stop working and exit.
        public void RequestStop()
        {
            this._shouldStop = true;
        }

        public void Dispose()
        {
            // Tell the thread to stop, then wait for it to finish.
            this.RequestStop();
            this._readerThread.Join();
        }
    }
}
