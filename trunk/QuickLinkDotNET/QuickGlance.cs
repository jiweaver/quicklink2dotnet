#region License

/* QuickLinkDotNet : A .NET wrapper (in C#) for EyeTech's QuickLink API.
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

#endregion License

#region Header Comments

/* $Id$
 *
 * Description: If Quick Glance is not currently running, this class will start
 * it, and wait for it to become ready. Additionally, it will create an
 * instance of the QuickLink class (which finds and loads the required
 * QuickLink DLLs).
 */

#endregion Header Comments

using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace QuickLinkDotNet
{
    public class QuickGlance : IDisposable
    {
        #region Constants

        private const int WaitForQuickGlanceToStart_Timeout = 30000; // ms.
        private const int WaitForQuickGlanceToExit_Timeout = 30000; // ms.

        #endregion Constants

        #region Fields

        private Process process = null;

        private QuickLink quickLink = null;

        #endregion Fields

        #region Properties

        public bool QuickGlanceWasAlreadyRunning
        {
            get
            {
                return (this.process == null);
            }
        }

        public Process Process
        {
            get
            {
                return this.process;
            }
        }

        public QuickLink QuickLink
        {
            get
            {
                return this.quickLink;
            }
        }

        #endregion Properties

        #region Constructor

        public QuickGlance()
        {
            if (!ProcessIsRunning(QuickConstants.ProcessName))
                /* Quick Glance is not running, so start it up. */
                this.process = StartQuickGlance();
            else
                this.process = null;

            /* Make an instance of the QuickLink class. */
            this.quickLink = new QuickLink();
        }

        ~QuickGlance()
        {
            Dispose(false);
        }

        private static bool ProcessIsRunning(string processName)
        {
            Process[] processes;

            try
            {
                processes = Process.GetProcessesByName(processName);
            }

            catch (Exception ex)
            {
                throw new Exception("Failed to get process info.  (MSG): " + ex.Message);
            }

            return (processes.Length > 0);
        }

        private static Process StartQuickGlance()
        {
            string executablePath = FindQuickGlancePath();

            if (executablePath == null)
                throw new Exception("The Quick Glance executable path could not be retrieved from the registry.");

            Process p;
            try
            {
                p = Process.Start(executablePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to start executable '" + executablePath + "'.  (MSG): " + ex.Message);
            }

            if (!p.WaitForInputIdle(WaitForQuickGlanceToStart_Timeout))
            {
                throw new Exception("Timeout waiting for '" + executablePath + "' process to become idle.");
            }

            return p;
        }

        private static string FindQuickGlancePath()
        {
            RegistryKey reg = Registry.LocalMachine;

            string result = null;

            foreach (string possibleLocation in QuickConstants.PossibleRegistryKeyLocations)
            {
                // Try each possible key locatregOpenSubKeyion.
                RegistryKey key = reg.OpenSubKey(Path.Combine(possibleLocation, QuickConstants.QuickGlanceSubKeyName));
                if (key == null)
                    continue;

                object oresult = key.GetValue("Path");
                // Path to the Quick Glance executable.
                if (oresult == null)
                    // Value not found!
                    continue;

                result = oresult.ToString();

                break;
            }

            return result;
        }

        #endregion Constructor

        #region IDisposable

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed == false)
            {
                if (this.process != null)
                {
                    /* We started Quick Glance, so we should shut it down.
                     */
                    this.quickLink.ExitQuickGlance();

                    try
                    {
                        this.process.CloseMainWindow();

                        this.process.WaitForExit(WaitForQuickGlanceToExit_Timeout);
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error closing main Quick Glance window.  (MSG): " + ex.Message);
                    }
                }

                /* The unmanaged resources have not been freed yet.
                 */
                this.quickLink.Dispose();
            }

            /* Mark the unmanaged resources as freed. */
            this.disposed = true;
        }

        #endregion IDisposable
    }
}