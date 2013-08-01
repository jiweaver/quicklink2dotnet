#region License

/* QLHelper: Helper methods for QuickLink2DotNet.
 *
 * Copyright (c) 2011-2013 Justin Weaver
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

/* $Id: MainForm.cs 38 2011-05-09 01:07:39Z piranther $
 *
 * Description: Helper methods for eye tracker.
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using QuickLink2DotNet;

namespace QuickLink2DotNet
{
    /// <summary>
    /// Indicates that a QuickLink2 API call made from within the Helper returned an unhandled error
    /// value.
    /// </summary>
    [Serializable()]
    public class QLErrorException : System.Exception
    {
        /// <summary>
        /// Set to the name of the QuickLink2 API function that returned the error.
        /// </summary>
        public string QLFunctionName;

        /// <summary>
        /// The error that was returned.
        /// </summary>
        public QLError QLError;

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public QLErrorException()
            : base()
        {
        }

        /// <summary>
        /// Constructor that sets QLFunctionName and QLError fields of the exception.
        /// </summary>
        /// <param name="functionName">
        /// Value to assign to the QLFunctionName field of the exception.
        /// </param>
        /// <param name="error">
        /// Value to assign to the QLError field of the exception.
        /// </param>
        public QLErrorException(string functionName, QLError error)
            : base()
        {
            this.QLFunctionName = functionName;
            this.QLError = error;
        }

        /// <summary>
        /// Constructor that sets QLFunctionName, QLError, and generic Message fields of the exception.
        /// </summary>
        /// <param name="functionName">
        /// Value to assign to the QLFunctionName field of the exception.
        /// </param>
        /// <param name="error">
        /// Value to assign to the QLError field of the exception.
        /// </param>
        /// <param name="message">
        /// Value to assign to the generic Message field of the exception.
        /// </param>
        public QLErrorException(string functionName, QLError error, string message)
            : base(message)
        {
            this.QLFunctionName = functionName;
            this.QLError = error;
        }

        /// <summary>
        /// Constructor that sets QLFunctionName, QLError, generic Message, and generic InnerException
        /// fields of the exception.
        /// </summary>
        /// <param name="functionName">
        /// Value to assign to the QLFunctionName field of the exception.
        /// </param>
        /// <param name="error">
        /// Value to assign to the QLError field of the exception.
        /// </param>
        /// <param name="message">
        /// Value to assign to the generic Message field of the exception.
        /// </param>
        /// <param name="inner">
        /// Value to assign to the generic InnerException field of the exception.
        /// </param>
        public QLErrorException(string functionName, QLError error, string message, System.Exception inner)
            : base(message, inner)
        {
            this.QLFunctionName = functionName;
            this.QLError = error;
        }

        /// <summary>
        /// A constructor for serialization of an exception that propagates from a remoting server
        /// to the client.
        /// </summary>
        /// <param name="info">
        /// The info needed to serialize the object.
        /// </param>
        /// <param name="context">
        /// Source, destination, and additional caller-defined context info.
        /// </param>
        protected QLErrorException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }

    /// <summary>
    /// Some convenient 'helper' methods for the QuickLink2 API.
    /// </summary>
    public static class QLHelper
    {
        /// <summary>
        /// Get the deviceIDs of every eye tracker device detected on the system.
        /// </summary>
        /// <returns>
        /// An array containing the deviceIDs of every eye tracker device detected on the system.
        /// </returns>
        /// <exception cref="QLErrorException">
        /// Thrown when:
        /// <list type="numbered">
        /// <item>
        /// <description>
        /// Enumerating the bus fails.
        /// </description>
        /// </item>
        /// </list>
        /// <para>
        /// In all cases, the name of the offending API function is written to the QLFunctionName field,
        /// the returned QLError value is written to the QLError field, and a message with further
        /// specific information is written to the Message field of the exception object.
        /// </para>
        /// </exception>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public static int[] GetDeviceIDs()
        {
            int numDevices = 1;
            int[] deviceIDs = new int[numDevices];

            QLError error = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIDs);

            if (error == QLError.QL_ERROR_BUFFER_TOO_SMALL)
            {
                // Array too small.  Try again with a properly sized array.
                deviceIDs = new int[numDevices];
                error = QuickLink2API.QLDevice_Enumerate(ref numDevices, deviceIDs);
            }

            if (error != QLError.QL_ERROR_OK)
            {
                throw new QLErrorException("QLDevice_Enumerate", error, "Error while enumerating the bus");
            }

            if (numDevices == 0)
            {
                // No devices detected.
                return new int[0];
            }

            // Return the ID of the first device.
            return deviceIDs;
        }

        /// <summary>
        /// Loads the password for the specified device from the specified file.
        /// </summary>
        /// <returns>
        /// The password string.
        /// </returns>
        /// <param name="deviceID">
        /// The ID of the device we wish to load the password for.
        /// </param>
        /// <param name="filename">
        /// The path to the settings file from which we will load the password.
        /// </param>
        /// <exception cref="QLErrorException">
        /// <para>
        /// Thrown when:
        /// <list type="numbered">
        /// <item>
        /// <description>
        /// Loading the settings from the specified file fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Fetching info for the specified device fails
        /// </description>
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// In all cases, the name of the offending API function is written to the QLFunctionName field,
        /// the returned QLError value is written to the QLError field, and a message with further
        /// specific information is written to the Message field of the exception object.
        /// </para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when:
        /// <list type="numbered">
        /// <item>
        /// <description>
        /// The specified file does not exist
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The specified deviceID is invalid.
        /// </description>
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public static string LoadDevicePasswordFromFile(int deviceID, string filename)
        {
            // Read the settings out of a file into a new setting container.
            int settingsID = -1;
            QLError error = QuickLink2API.QLSettings_Load(filename, ref settingsID);
            if (error == QLError.QL_ERROR_INVALID_PATH)
            {
                throw new ArgumentException("The specified file does not exist", "filename");
            }
            if (error != QLError.QL_ERROR_OK)
            {
                throw new QLErrorException("QLSettings_Load", error);
            }

            // Get the device's serial number.
            QLDeviceInfo devInfo;
            error = QuickLink2API.QLDevice_GetInfo(deviceID, out devInfo);
            if (error == QLError.QL_ERROR_INVALID_DEVICE_ID)
            {
                throw new ArgumentException("Invalid device ID", "deviceID");
            }
            if (error != QLError.QL_ERROR_OK)
            {
                throw new QLErrorException("QLDevice_GetInfo", error);
            }

            string passwordSettingName = "SN_" + devInfo.serialNumber;

            // Check for the device password already in settings and fetch its size.
            int passwordSettingSize;
            error = QuickLink2API.QLSettings_GetValueStringSize(settingsID, passwordSettingName, out passwordSettingSize);
            if (error == QLError.QL_ERROR_NOT_FOUND)
            {
                throw new ArgumentException("The device password does not exist in the specified file", "filename");
            }
            else if (error != QLError.QL_ERROR_OK)
            {
                throw new QLErrorException("QLSettings_GetValueStringSize", error);
            }

            // Load the password from the settings file.
            System.Text.StringBuilder password = new System.Text.StringBuilder(passwordSettingSize);
            error = QuickLink2API.QLSettings_GetValueString(settingsID, passwordSettingName, passwordSettingSize, password);
            if (error != QLError.QL_ERROR_OK)
            {
                throw new QLErrorException("QLSettings_GetValueString", error);
            }

            // Return the password.
            return password.ToString();
        }

        /// <summary>
        /// Saves the eye tracker's password to a file.
        /// </summary>
        /// <param name="deviceID">
        /// The ID of the device for which we wish to save the password.
        /// </param>
        /// <param name="password">
        /// The password for the specified device.
        /// </param>
        /// <param name="filename">
        /// The path to the settings file where we wish to save the password.
        /// </param>
        /// <exception cref="QLErrorException">
        /// <para>
        /// Thrown when:
        /// <list type="numbered">
        /// <item>
        /// <description>
        /// Loading the settings from the specified file fails (if the file already exists)
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Creating a new settings container fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Fetching info for the specified device fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Removal of existing password from settings container fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Writing the value of the new password to the settings container fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Saving the settings container to the specified file fails
        /// </description>
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// In all cases, the name of the offending API function is written to the QLFunctionName field,
        /// the returned QLError value is written to the QLError field, and a message with further
        /// specific information is written to the Message field of the exception object.
        /// </para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when:
        /// <list type="numbered">
        /// <item>
        /// <description>
        /// The specified deviceID is invalid.
        /// </description>
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public static void SaveDevicePasswordToFile(int deviceID, string password, string filename)
        {
            // Read the settings out the file (if it exists) into a new setting container.
            int settingsID = -1;
            QLError error = QuickLink2API.QLSettings_Load(filename, ref settingsID);
            if (error == QLError.QL_ERROR_INVALID_PATH)
            {
                // The file does not exist to load from; create a new settings container.
                error = QuickLink2API.QLSettings_Create(-1, out settingsID);
                if (error != QLError.QL_ERROR_OK)
                {
                    throw new QLErrorException("QLSettings_Create", error);
                }
            }
            else if (error != QLError.QL_ERROR_OK)
            {
                throw new QLErrorException("QLSettings_Load", error);
            }

            // Get the device's serial number.
            QLDeviceInfo devInfo;
            error = QuickLink2API.QLDevice_GetInfo(deviceID, out devInfo);
            if (error == QLError.QL_ERROR_INVALID_DEVICE_ID)
            {
                throw new ArgumentException("Invalid device ID", "deviceID");
            }
            else if (error != QLError.QL_ERROR_OK)
            {
                throw new QLErrorException("QLDevice_GetInfo", error);
            }

            string passwordSettingName = "SN_" + devInfo.serialNumber;

            // Remove the password if it already exists in the container.
            error = QuickLink2API.QLSettings_RemoveSetting(settingsID, passwordSettingName);
            if (error != QLError.QL_ERROR_OK && error != QLError.QL_ERROR_NOT_FOUND)
            {
                throw new QLErrorException("QLSettings_RemoveSetting", error);
            }

            // Add the password to the container.
            error = QuickLink2API.QLSettings_SetValueString(settingsID, passwordSettingName, password);
            if (error != QLError.QL_ERROR_OK)
            {
                throw new QLErrorException("QLSettings_SetValueString", error);
            }

            // Save the settings container to the file.
            error = QuickLink2API.QLSettings_Save(filename, settingsID);
            if (error != QLError.QL_ERROR_OK)
            {
                throw new QLErrorException("QLSettings_Save", error);
            }
        }

        /// <summary>
        /// Prompts the user for a password for the specified device and writes it to the specified
        /// settings file.  If the specified settings file already exists, and an entry for the device's
        /// password exists within the file, then the user will be prompted if they "are sure?" they want
        /// to overwrite the existing password (unless the <paramref name="force"/> parameter is true).
        /// </summary>
        /// <param name="deviceID">
        /// The ID of the device for which we wish to set the password.
        /// </param>
        /// <param name="filename">
        /// The path to the settings file where we wish to save the password.
        /// </param>
        /// <param name="force">
        /// When set to true, ignores and overwrites any existing password for the specified device
        /// without prompting; otherwise, prompts "are you sure?" when the password already exists in the
        /// specified file.
        /// </param>
        /// <exception cref="QLErrorException">
        /// <para>
        /// Thrown when:
        /// <list type="numbered">
        /// <item>
        /// <description>
        /// The specified file already exists, but loading the settings from the specified file fails.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Creating a new settings container fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Fetching info for the specified device fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Removal of existing password from settings container fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Writing the value of the new password to the settings container fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Saving the settings container to the specified file fails
        /// </description>
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// In all cases, the name of the offending API function is written to the QLFunctionName field,
        /// the returned QLError value is written to the QLError field, and a message with further
        /// specific information is written to the Message field of the exception object.
        /// </para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when:
        /// <list type="numbered">
        /// <item>
        /// <description>
        /// The specified deviceID is invalid.
        /// </description>
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public static void ConsoleInteractive_SetDevicePassword(int deviceID, string filename, bool force)
        {
            ConsoleKey keypress = ConsoleKey.Y;

            if (!force)
            {
                try
                {
                    QLHelper.LoadDevicePasswordFromFile(deviceID, filename);
                    Console.Write("Device password is already set. Would you like to enter a new password? (y/n): ");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    Console.WriteLine();
                    keypress = keyInfo.Key;
                }
                catch (QLErrorException e)
                {
                    if (e.QLError != QLError.QL_ERROR_INVALID_PATH && e.QLError != QLError.QL_ERROR_NOT_FOUND)
                    {
                        throw;
                    }
                }
            }

            while (keypress == ConsoleKey.Y)
            {
                QLDeviceInfo info;
                QuickLink2API.QLDevice_GetInfo(deviceID, out info);
                Console.Write("Enter password for detected model {0} with serial number {1}: ", info.modelName, info.serialNumber);
                string password = Console.ReadLine();

                QLError error = QuickLink2API.QLDevice_SetPassword(deviceID, password);
                if (error == QLError.QL_ERROR_INVALID_PASSWORD)
                {
                    Console.WriteLine("New password is invalid. Try again? (y/n): ");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    keypress = keyInfo.Key;
                    continue;
                }
                else if (error != QLError.QL_ERROR_OK)
                {
                    throw new QLErrorException("QLDevice_SetPassword", error, "Setting device password failed");
                }
                else
                {
                    QLHelper.SaveDevicePasswordToFile(deviceID, password, filename);
                    Console.WriteLine("New password saved.");
                    return;
                }
            }
        }

        /// <summary>
        /// Prompts the user for a password for the specified device and writes it to the specified
        /// settings file.  If the specified settings file already exists, and an entry for the device's
        /// password exists within the file, then the user will be prompted if they "are sure?" they want
        /// to overwrite the existing password.
        /// </summary>
        /// <param name="deviceID">
        /// The ID of the device for which we wish to set the password.
        /// </param>
        /// <param name="filename">
        /// The path to the settings file where we wish to save the password.
        /// </param>
        /// <exception cref="QLErrorException">
        /// <para>
        /// Thrown when:
        /// <list type="numbered">
        /// <item>
        /// <description>
        /// The specified file already exists, but loading the settings from the specified file fails.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Creating a new settings container fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Fetching info for the specified device fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Removal of existing password from settings container fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Writing the value of the new password to the settings container fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Saving the settings container to the specified file fails
        /// </description>
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// In all cases, the name of the offending API function is written to the QLFunctionName field,
        /// the returned QLError value is written to the QLError field, and a message with further
        /// specific information is written to the Message field of the exception object.
        /// </para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when:
        /// <list type="numbered">
        /// <item>
        /// <description>
        /// The specified deviceID is invalid.
        /// </description>
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public static void ConsoleInteractive_SetDevicePassword(int deviceID, string filename)
        {
            ConsoleInteractive_SetDevicePassword(deviceID, filename, false);
        }

        /// <summary>
        /// Detects all eye tracker devices on the system and lists them on the console, then prompts the
        /// user to choose a device.  If only one device is found, that device ID is returned without
        /// prompting the user for input.
        /// </summary>
        /// <returns>
        /// The ID of the chosen device.  Returns -1 if no devices are found or the user opts to quit.
        /// </returns>
        /// <exception cref="QLErrorException">
        /// Thrown when:
        /// <list type="numbered">
        /// <item>
        /// <description>
        /// Enumerating the bus fails.
        /// </description>
        /// </item>
        /// </list>
        /// <para>
        /// In all cases, the name of the offending API function is written to the QLFunctionName field,
        /// the returned QLError value is written to the QLError field, and a message with further
        /// specific information is written to the Message field of the exception object.
        /// </para>
        /// </exception>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public static int ConsoleInteractive_GetDeviceID()
        {
            int deviceID = -1;

            int[] deviceIDs = QLHelper.GetDeviceIDs();
            if (deviceIDs.Length == 0)
            {
                Console.WriteLine("No eye trackers found.");
                return -1;
            }
            else if (deviceIDs.Length == 1)
            {
                return deviceIDs[0];
            }
            else
            {
                Console.WriteLine("Found devices:");
                for (int i = 0; i < deviceIDs.Length; i++)
                {
                    QLDeviceInfo info;
                    QLError error = QuickLink2API.QLDevice_GetInfo(deviceIDs[i], out info);
                    Console.WriteLine("  ID: {0}; Model: {1}; Serial: {2}", deviceIDs[i], info.modelName, info.serialNumber);
                }

                do
                {
                    Console.Write("Enter the ID of the device to use (or enter q to quit): ");
                    string answer = Console.ReadLine();

                    if (answer.Equals("q") || answer.Equals("Q"))
                    {
                        break;
                    }
                    else
                    {
                        try
                        {
                            deviceID = Int32.Parse(answer);
                        }
                        catch (ArgumentException) { continue; }
                        catch (FormatException) { continue; }
                        catch (OverflowException) { continue; }
                    }

                    for (int i = 0; i < deviceIDs.Length; i++)
                    {
                        if (deviceID == deviceIDs[i])
                        {
                            return deviceID;
                        }
                    }
                } while (deviceID < 0);

                return -1;
            }
        }

        /// <summary>
        /// <list type="numeric">
        /// <item>
        /// <decription>
        /// Uses the console to allow the user to select a device from all detected devices on the
        /// system.
        /// </decription>
        /// </item>
        /// <item>
        /// <decription>
        /// Checks the specified settings file for a valid password for the selected device and prompts
        /// the user to enter one if it is not found.  If a new password is entered, then the new
        /// password is also saved to the specified settings file.
        /// </decription>
        /// </item>
        /// <item>
        /// <decription>
        /// Readies the selected device by writing the password to it.
        /// </decription>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="filename">
        /// The path to the settings file that we wish to load the device password from and/or where we
        /// wish to save the password to.
        /// </param>
        /// <returns>
        /// The ID of the chosen device on success.  -1 if no devices are detected or the user cancels.
        /// </returns>
        /// <exception cref="QLErrorException">
        /// <para>
        /// Thrown when:
        /// <list type="numbered">
        /// <item>
        /// <description>
        /// Enumerating the bus fails.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Loading the settings from the specified file fails for reasons other than
        /// <see cref="QLError.QL_ERROR_INVALID_PATH"/>, <see cref="QLError.QL_ERROR_NOT_FOUND"/>, or
        /// <see cref="QLError.QL_ERROR_INVALID_PASSWORD"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Fetching info for the specified device fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The specified file already exists, but loading the settings from the specified file fails.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Creating a new settings container fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Fetching info for the specified device fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Removal of existing password from settings container fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Writing the value of the new password to the settings container fails
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Saving the settings container to the specified file fails
        /// </description>
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// In all cases, the name of the offending API function is written to the QLFunctionName field,
        /// the returned QLError value is written to the QLError field, and a message with further
        /// specific information is written to the Message field of the exception object.
        /// </para>
        /// </exception>
        /// <exception cref="DllNotFoundException">
        /// The QuickLink2 DLLs ("QuickLink2.dll," "PGRFlyCapture.dll," and "SMX11MX.dll") must be placed
        /// in the same directory as your program's binary executable; otherwise, this exception will be
        /// thrown.
        /// </exception>
        public static int ConsoleInteractive_Initialize(string filename)
        {
            int deviceID = QLHelper.ConsoleInteractive_GetDeviceID();

            if (deviceID == -1)
            {
                return -1;
            }

            try
            {
                QLHelper.LoadDevicePasswordFromFile(deviceID, filename);
            }
            catch (QLErrorException e)
            {
                if (e.QLError == QLError.QL_ERROR_INVALID_PATH || e.QLError == QLError.QL_ERROR_NOT_FOUND)
                {
                    Console.WriteLine("Device password not found.");
                    QLHelper.ConsoleInteractive_SetDevicePassword(deviceID, filename);
                }
                else if (e.QLError == QLError.QL_ERROR_INVALID_PASSWORD)
                {
                    Console.WriteLine("Device password is invalid.");
                    QLHelper.ConsoleInteractive_SetDevicePassword(deviceID, filename, true);
                }
                else
                {
                    throw;
                }
            }

            return deviceID;
        }
    }
}