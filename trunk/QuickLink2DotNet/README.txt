QuickLink2DotNet: A wrapper written in C# to expose EyeTech Digital System's unmanaged QuickLink2 API for use in the managed Microsoft .NET environment.

Author: Justin Weaver
Copyright © 2011-2013 Justin Weaver
Homepage: http://quicklinkapi4net.googlecode.com


!!!ATTENTION!!!: Copy the QuickLink2 libraries (QuickLink2.dll, PGRFlyCapture.dll, and SMX11MX.dll) from your QuickLink2 installation into a your program's output directory.  Otherwise, your program will not find the libraries at runtime.  For instance, if you want to run the SetupDevicePassword example, first you should compile the example in Visual Studio 2010, and then copy the aforementioned QuickLink2 libraries to the example's \bin\Debug directory before you attempt to run the example.


Main Directory Contents:
===================================
(1) Docs: Main help and documentation files for QuickLink2DotNet.

(2) Examples: Usage examples and helpful utilities for use with QuickLink2DotNet.

(3) QLExampleHelper: A class library containing some convenience methods used in a number of the example programs to reduce code duplication.

(4) QuickLink2DotNet: The QuickLink2DotNet API wrapper.


Examples Directory Contents:
===================================
(1) DeviceInfo: Displays a list of available devices and prompts the user to select one.  Then queries the selected device for all its settings and displays their values.

(2) GazeInfo2: Displays a list of available devices and prompts the user to select one.  Then displays a stream of info from the device on a Windows Form.  Requires password and calibration files (see #3, 4, and 5 in this list).

(3) QuickStart: A very simple console example to demonstrate initialization, calibration, and data collection from the eye tracker.  The password is saved in the file  "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlsettings.txt" for later use.  The calibration is saved in the file "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlcalibration.qlc" for later use.

(4) SetupDeviceCalibration: Displays a list of available devices and prompts the user to select one.  Then performs device calibration.  The calibration is saved in the file "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlcalibration.qlc" for later use.

(5) SetupDevicePassword: Displays a list of available devices and prompts the user to select one.  Then prompts for the device's password.  The password is saved in the file  "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlsettings.txt" for later use.

(6) StopAllDevices: Stops all the EyeTech eye tracker devices on the system without prompting.  Requires password file (see #4 or 5 in this list).

(7) VideoViewer2: Displays a list of available devices and prompts the user to select one.  Then displays video from the selected device.  Requires password and calibration files (see #3, 4, and 5 in this list).