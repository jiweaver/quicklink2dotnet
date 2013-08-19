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
using QuickLink2DotNet;

namespace QuickLink2DotNetHelper
{
    public partial class QLHelper
    {
        private static string _passwordSettingName = "DevicePassword";

        private static string LoadPassword(string settingsFilename)
        {
            if (!File.Exists(settingsFilename))
            {
                //Console.WriteLine("Cannot load device password from settings file '{0}' because it does not exist.", settingsFilename);
                return null;
            }

            // Read the settings out of a file into a new setting container.
            int settingsId = -1;
            QLError error = QuickLink2API.QLSettings_Load(settingsFilename, ref settingsId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_Load() returned {0}.", error.ToString());
                return null;
            }

            // Check for the device password already in settings and fetch its size.
            int passwordSettingSize;
            error = QuickLink2API.QLSettings_GetValueStringSize(settingsId, QLHelper._passwordSettingName, out passwordSettingSize);
            if (error == QLError.QL_ERROR_NOT_FOUND)
            {
                Console.WriteLine("The device password does not exist in the specified settings file.");
                return null;
            }
            else if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_GetValueStringSize() returned {0}.", error.ToString());
                return null;
            }

            // Load the password from the settings file.
            System.Text.StringBuilder password = new System.Text.StringBuilder(passwordSettingSize);
            error = QuickLink2API.QLSettings_GetValueString(settingsId, QLHelper._passwordSettingName, passwordSettingSize, password);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_GetValueString() returned {0}.", error.ToString());
                return null;
            }

            // Return the password.
            return password.ToString();
        }

        private static bool SavePassword(string settingsFilename, string password)
        {
            // Read the settings out the file (if it exists) into a new setting container.
            int settingsId = 0;
            QLError error = QuickLink2API.QLSettings_Load(settingsFilename, ref settingsId);
            if (error == QLError.QL_ERROR_INVALID_PATH || error == QLError.QL_ERROR_INTERNAL_ERROR)
            {
                // The file does not exist to load from; create a new settings container.
                error = QuickLink2API.QLSettings_Create(-1, out settingsId);
                if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("QLSettings_Create() returned {0}.", error.ToString());
                    return false;
                }
            }
            else if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_Load() returned {0}.", error.ToString());
                return false;
            }

            // Remove the password if it already exists in the container.
            error = QuickLink2API.QLSettings_RemoveSetting(settingsId, QLHelper._passwordSettingName);
            if (error != QLError.QL_ERROR_OK && error != QLError.QL_ERROR_NOT_FOUND)
            {
                Console.WriteLine("QLSettings_RemoveSetting() returned {0}.", error.ToString());
                return false;
            }

            // Add the password to the container.
            error = QuickLink2API.QLSettings_SetValueString(settingsId, QLHelper._passwordSettingName, password);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_SetValueString() returned {0}.", error.ToString());
                return false;
            }

            // Save the settings container to the file.
            error = QuickLink2API.QLSettings_Save(settingsFilename, settingsId);
            if (error != QLError.QL_ERROR_OK)
            {
                Console.WriteLine("QLSettings_Save() returned {0}.", error.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Attempts to load the eye tracker device's password from the settings file at the location
        /// specified in <see cref="SettingsFilename"/>.  If the password cannot be loaded from the
        /// settings file, or if the loaded password is rejected by the device, the user will be prompted
        /// for the device's password.  If the device accepts the new, user-entered password, the
        /// password is then saved to the settings file.
        /// </summary>
        /// <returns>
        /// True when the device has accepted the password; otherwise, false.
        /// </returns>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public bool SetupPassword()
        {
            QLError error;

            string password = LoadPassword(this.SettingsFilename);
            if (password != null)
            {
                error = QuickLink2API.QLDevice_SetPassword(this.DeviceId, password);
                if (error == QLError.QL_ERROR_OK)
                {
                    return true;
                }
                else if (error != QLError.QL_ERROR_INVALID_PASSWORD)
                {
                    Console.WriteLine("QLDevice_SetPassword() returned {0}.", error.ToString());
                    return false;
                }
            }

            ConsoleKey keypress = ConsoleKey.Y;

            while (keypress == ConsoleKey.Y)
            {
                Console.Write("Enter password for device: ");
                password = Console.ReadLine();

                error = QuickLink2API.QLDevice_SetPassword(this.DeviceId, password);
                if (error == QLError.QL_ERROR_INVALID_PASSWORD)
                {
                    // Flush the input buffer.
                    while (Console.KeyAvailable) { Console.ReadKey(true); }

                    Console.WriteLine("Password is invalid. Try again? (y/n): ");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    keypress = keyInfo.Key;
                    continue;
                }
                else if (error != QLError.QL_ERROR_OK)
                {
                    Console.WriteLine("QLDevice_SetPassword() returned {0}.", error.ToString());
                    return false;
                }
                else
                {
                    if (SavePassword(this.SettingsFilename, password))
                    {
                        Console.WriteLine("New password applied to device and saved to settings file.");
                    }
                    else
                    {
                        Console.WriteLine("Password applied to device, but could not save to settings file.");
                    }
                    return true;
                }
            }

            return false;
        }
    }
}