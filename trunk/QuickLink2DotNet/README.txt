QuickLink2DotNet

Copyright (c) 2011-2013 Justin Weaver
Homepage: http://quicklinkapi4net.googlecode.com


Main Directory Contents:

(1) Docs: Main help and documentation files for QuickLink2DotNet. <---LOOK HERE FOR ALL OTHER INFORMATION NOT PRESENTED HERE!

(2) Examples: Examples, helpful utilities, and class libraries for use with QuickLink2DotNet.

(3) QuickLink2DotNet: The QuickLink2DotNet API wrapper.


'Examples' Directory Contents:

(1) Calibrate2: A program that performs and saves device calibration, and saves the device password to a file for later use.  Be aware that the password file that this example generates is necessary for GazeInfo2 and VideoViewer2 to function properly.  Additionally, GazeInfo2 will not be able to report the user's gaze information unless the calibration file is present.  So, be sure to run this example before attempting to run GazeInfo2 or VideoViewer2.
	This example will store its calibration file in: "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlcalibration.qlc"
	This example will store its settings/password file in: "%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlsettings.txt"

(2) GazeInfo2: Displays a stream of info from the first eye tracker device on the system.

(3) QuickStart: A simple example to demonstrate initialization, calibration, and data collection from the eye tracker.

(4) Stopper: Shuts down all the EyeTech eye tracker devices on the system.  This is sometimes handy to have during debugging.

(5) VideoViewer2: Displays video from the first eye tracker device on the system.


IMPORTANT NOTE ABOUT EXAMPLES:

Don't forget to copy the QuickLink2 DLLs from your QuickLink2 install into the 'Examples\<Example Name>\bin\Debug' or 'Examples\<Example Name>\bin\Release' directories of whatever example you are working with.  Otherwise, the examples will complain about not being able to find the QuickLink2 DLLs when you try to run them.