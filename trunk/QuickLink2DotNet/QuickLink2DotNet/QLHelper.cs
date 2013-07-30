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
        /// Loading the calibration from the specified file fails
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
            int numDevices = 8;
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
    }
}