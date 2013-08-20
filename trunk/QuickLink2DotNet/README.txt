QuickLink2DotNet: A wrapper written in C# to expose EyeTech Digital System's unmanaged Quick Link 2 API for use in the managed Microsoft .NET environment.

Author: Justin Weaver
Copyright © 2011-2013 Justin Weaver
Homepage: http://quicklinkapi4net.googlecode.com


!!!ATTENTION!!!: Copy the Quick Link 2 libraries (QuickLink2.dll, PGRFlyCapture.dll, and SMX11MX.dll) from your Quick Link 2 installation into a your program's output directory.  Otherwise, your program will not find the libraries at runtime.  For instance, if you want to run the SetupDevice example, first you should compile the SetupDevice example in Visual Studio 2010, and then copy the aforementioned Quick Link 2 libraries to the example's \bin\Debug directory before you attempt to run the example.


Main Directory Contents:
===================================
(1) Docs: Main help and documentation files for QuickLink2DotNet.

(2) Examples: Usage examples, demos, and helpful utilities for use with QuickLink2DotNet.

(3) QuickLink2DotNet: The QuickLink2DotNet API wrapper.

(4) QuickLink2DotNetHelper: A class library containing some helper methods for use with QuickLink2DotNet.


Examples Directory Contents:
===================================
(1) Gaze: Track the user's gaze, and display a red circle where they are currently looking.  If necessary, prompts for device's password and performs calibration.

(2) QuickStart: A very simple console example to demonstrate initialization, calibration, and data collection from the eye tracker.  If necessary, prompts for device's password and performs calibration.

(3) SetupDevice: Setup an eye tracker device by setting its password, calibration, and other miscellaneous settings.

(4) ShowDeviceSettings: Displays the values of all supported settings for a selected device.  Does not require device's password or calibration.

(5) StopAllDevices: Stops all the EyeTech eye tracker devices on the system.  Prompts for device passwords if necessary.  Does not require device calibration.

(6) VideoViewer: Displays live video from a selected device.  Prompts for device's password if necessary.  Does not require device calibration.


Getting Started:
===================================
See the "HOW TO USE THE WRAPPER IN YOUR VISUAL STUDIO 2010 PROJECT" section on the starting page of the 'Docs\QuickLink2DotNet.chm' help file.