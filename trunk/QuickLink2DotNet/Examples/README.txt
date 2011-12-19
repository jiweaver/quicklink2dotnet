QuickLink2DotNet Example Programs

Copyright (c) 2011 Justin Weaver
Homepage: http://quicklinkapi4net.googlecode.com


Directory		Program Description
-------------------------------------------------------------------------------
Calibrate2		A program that performs device calibration.  It also saves the 
				calibration it performs to the file "c:\qlcalibration.qlc", and
				it saves the device password to the file "c:\qlsettings.txt".
				The password file is necessary for the other two programs 
				(GazeInfo2 and VideoViewer) to function.  The calibration file
				is optionally used by the GazeInfo2 program to display the
				user's gaze point.

GazeInfo2		Displays a stream of info from the first eye tracker device on
				the system.

VideoViewer2	Displays video from the first eye tracker device on the system.
------------------------------------------------------------------------------


IMPORTANT NOTES: 
	1. Don't forget to copy the QuickLink2 DLLs from your QuickLink2 install 
	into the 'Examples\<Example>\bin\Debug' or 'Examples\<Example>\bin\Release'
	directories of whatever example you are working with.  Otherwise, the
	examples will complain about not being able to find the QuickLink2 DLLs
	when you try to run them.
	
	2. Run "Calibrate" before running the other two examples, because it
	generates the password file that is required for the other two programs to
	work.