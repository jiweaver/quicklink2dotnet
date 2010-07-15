/* File: ReadBuf.cs
 * Author: Justin Weaver
 * Date: Jul 2010
 * Revision: $Rev$
 * Description: TODO
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using QuickLinkAPI4NET;

namespace EyeTrackerAuthenticator
{
    public class ReadBuf : IDisposable
    {
        // Periodic eye data reader.
        private QuickLinkPeriodicReader reader;

        // The length of the circular ImageData queue.
        private int _qLen;

        // The ImageData queue.
        private Queue<ImageData> _Q;

        // The method to callback.
        private NewImageDataCallbackType _newImageDataCallbackMethod;

        public ReadBuf(int qLen, NewImageDataCallbackType callBackMethod)
        {
            this._qLen = qLen;
            
            this._newImageDataCallbackMethod = (callBackMethod != null) ? new NewImageDataCallbackType(callBackMethod) : null;

            this._Q = new Queue<ImageData>();

            this.reader = new QuickLinkPeriodicReader(500, 0, ReadEvent);
        }

        // A version of the constructor without a callback.
        public ReadBuf(int qLen) : this(qLen, null) { }

        private void ReadEvent(ImageData iDat)
        {
            lock (this)
            {
                // We own the lock on the Q, put the new data in it.

                if (this._Q.Count() >= this._qLen)
                    // The queue is full, make room first.                         
                    this._Q.Dequeue();

                // Put the new data on the queue.
                this._Q.Enqueue(iDat);
            }

            // Call the registered callback method on this new ImageData.
            if (this._newImageDataCallbackMethod != null)
                this._newImageDataCallbackMethod(iDat);
        }

        // Return the current queue and start fresh with an empty one.
        public Queue<ImageData> GetQ()
        {
            Queue<ImageData> retQ;
            lock (this)
            {
                retQ = this._Q;
                this._Q.Clear();
            }
            return retQ;
        }

        public void FlushQ()
        {
            lock (this)
            {
                // We have the lock on the queue, empty it.
                this._Q.Clear();
            }
        }
    }
}
