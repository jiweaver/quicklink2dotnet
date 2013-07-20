QuickLink2DotNet Example Programs

Homepage: http://quicklinkapi4net.googlecode.com


Directory		Program Description
-------------------------------------------------------------------------------
Calibrate2		A program that performs device calibration.  It also saves the 
				calibration it performs to the file
				"%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlcalibration.qlc"
				, and it saves the device password to the file
				"%USERPROFILE%\AppData\Roaming\QuickLink2DotNet\qlsettings.txt"
				.  The password file is necessary for the other two programs 
				(GazeInfo2 and VideoViewer2) to function.  The calibration file
				is optionally used by the GazeInfo2 program to display the
				user's gaze point.

GazeInfo2		Displays a stream of info from the first eye tracker device on
				the system.

QuickStart		A simple example to demonstrate initialization, calibration,
				and data collection from the eye tracker.

Stopper			Shuts down all the EyeTech eye tracker devices on the system.
				This is sometimes handy to have during debugging.
				
VideoViewer2	Displays video from the first eye tracker device on the system.
------------------------------------------------------------------------------


IMPORTANT NOTES: 
	1. Don't forget to copy the QuickLink2 DLLs from your QuickLink2 install 
	into the 'Examples\<Example>\bin\Debug' or 'Examples\<Example>\bin\Release'
	directories of whatever example you are working with.  Otherwise, the
	examples will complain about not being able to find the QuickLink2 DLLs
	when you try to run them.
	
	2. Run Calibrate2 before running GazeInfo2, VideoViewer2 or Stopper, because
	it will generate the password file that they require.