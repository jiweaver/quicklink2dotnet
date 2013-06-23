QuickLink2DotNet: A .NET wrapper (in C#) for EyeTech's QuickLink2 API.

Copyright (c) 2011-2013 Justin Weaver
Homepage: http://quicklinkapi4net.googlecode.com


DESCRIPTION:
	This is a .NET wrapper (in C#) for EyeTech's QuickLink2 API.  The QuickLink
	API can be used by developers to control EyeTech's line of eye trackers.
	The QuickLink2 API can be obtained from EyeTech's website
	<http://www.eyetechds.com>.

VERSION:
	This wrapper has been updated to work with QuickLink2 Version 2.5.1.0
	(Released at http://www.eyetechds.com on Aug 8, 2012)
	
NOTICE:
	This wrapper is for QuickLink2.  The wrapper for the original QuickLink is
	also available from <http://quicklinkapi4net.googlecode.com>.

SPECIAL THANKS:
	To the University of Alaska <http://www.uaa.alaska.edu> Department of 
	Mathematical Sciences for allowing me to use an EyeTech TM3 eye tracker for
	this project.

	To EyeTech Digital Systems Inc. <http://www.eyetechds.com> for generously
	lending me a TM3 eye tracker, which allows me to keep this wrapper updated.

HOW THE WRAPPER WORKS:
	The API wrapper loads QuickLink2's unmanaged libraries into local memory
	space using .NET's InteropServices.  Microsoft's MSDN website provides a
	variety of helpful resources on the subject of working with unmanaged DLL
	functions from within managed code.

HOW TO USE THE WRAPPER IN YOUR VISUAL STUDIO 2010 PROJECT:
	1. Download and install the QuickLink2 software from EyeTech’s website.
	
	2. Open your project in Visual Studio 2010

	3. In the "Solution Explorer" right-click on your solution and select
	"Add -> Existing Project" from the context menu.

	4. Browse to, and select the "QuickLinkDotNet.csproj" file.  You should see 
	the project appear in the "Solution Explorer" along with yours.

	5. Then, in the "Solution Explorer", right click on the "References" item
	in _your_ project, and select "Add Reference" from the context menu.  An 
	"Add Reference" window should appear.

	6. In the "Add Reference" window, select the "Projects" tab and highlight 
	"QuickLinkDotNet", then press the "Ok" button.

	7. Towards the top of each source file from which you want to call the 
	QuickLink2 API (with the already present "using" statements) add the line
	"using QuickLink2DotNet;"

	8. Use any of the methods, classes, structs, or enums declared in 
	QLTypes.cs and QuickLink2.cs that you want to.

	9. Choose the project's architecture.
	For a 32-bit program:
		In the "Solution Explorer", right-click on your project's name (not the
		entire solution) and select "Properties."  Under the "Build" tab, 
		choose "x86" for the "Platform Target".  This is commonly the default.
	For a 64-bit program:
		In the "Solution Explorer", right-click on your project's name (not the
		entire solution) and select "Properties."  Under the "Build" tab, 
		choose "x64" for the "Platform Target".  Additionally, right-click on 
		the QuickLink2DotNet project, and select "Properties".  Under the 
		"Build" tab, type "ISX64" in the "Conditional compilation symbols" box.

	10.	From Visual Studio's drop down menus, select "Build -> Build Solution".
	
	11. Copy the QuickLink2 DLL files to your program's directory.
	For a 32-bit program (see also Step 9):
		Copy the required QuickLink2 DLLs into you program's directory.  These
		DLLs are likely in "Program Files\EyeTechDS\QuickLink2_x.x.x.x\bin"
		(where the "x"s are the version number of QuickLink2), unless you chose
		to customize QuickLink2's installation directory.  Copy all of the
		files in the aforementioned directory into your program's directory
		(which is usually either "bin\Debug" or "bin\Release", but can be
		different if you created a custom configuration).  If you don't perform
		this step properly, you will get a message like this when you attempt 
		to run your program: "Unable to load DLL 'QuickLink2.dll': The 
		specified module could not be found."
	For a 64-bit program (see also Step 9):
		If you chose "xx64" as the "Platform" in Step 9, then follow the 32-bit
		directions above, but the DLLs you need to copy are likely in
		"Program Files\EyeTechDS\QuickLink2_x.x.x.x\bin64".  

	12. Run your program.

NOTES:
	1. I could not make the API call "QLAPI_GetVersion()" work.  I finally
	decided it was not important enough to make myself insane over.  If anyone
	else happens to know how to get it working, please let me know.
	
	2. There is a known problem with QuickLink2 software.  Sometimes when the
	QuickLink2 API is initially started, the eye tracker continuously loses
	and then relocates the user's eyes every few seconds.  When this happens,
	just restart your program until it works properly.  Note that the problem
	only occurs when QuickLink2 is started, so it won't suddenly start
	happening while you are using a program.  If it happens, you will know
	right away.