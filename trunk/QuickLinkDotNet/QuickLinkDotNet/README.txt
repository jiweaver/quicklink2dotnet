QuickLinkDotNet: A .NET wrapper (in C#) for EyeTech's QuickLink API.

Copyright (c) 2010-2011 Justin Weaver
Homepage: http://quicklinkapi4net.googlecode.com


DESCRIPTION:
	This is a .NET wrapper (in C#) for EyeTech's QuickLink API.  The QuickLink
	API can be used by developers to control EyeTech's line of eye trackers.
	The QuickLink API is bundled with EyeTech's Quick Glance software, which 
	can be obtained from their website <http://eyetechds.com>.

NOTICE:
	This wrapper is for QuickLink.  The wrapper for the new QuickLink2 is
	also available from <http://quicklinkapi4net.googlecode.com>.

SPECIAL THANKS:
	To the University of Alaska Department of Mathematical Sciences for 
	allowing me to use an EyeTech TM3 eye tracker for this project.

HOW THE WRAPPER WORKS:
	The API wrapper loads QuickLink's unmanaged libraries into local memory
	space using .NET's InteropServices.  Microsoft's MSDN website provides a
	variety of helpful resources on the subject of working with unmanaged DLL
	functions from within managed code.

USAGE OPTIONS:
	Be sure to start Quick Glance once and do the initial eye tracker setup
	(enter the password, etc).  A developer who wants to use this wrapper in
	their code has two usage choices:

	1. Instantiate the QuickLink class.  This will automatically find and load
	the necessary QuickLink DLLs into local program space. Then QuickLink API
	operations can be accessed through your QuickLink class instance.
	
	2. Instantiate the QuickGlance class.  It will find and start the Quick
	Glance program, if it is not already running.  After that, it acts just 
	like usage option #1.  A QuickLink class instance is automatically created
	by the QuickGlance class, and it can be accessed via the QuickLink
	property.
	
	3. Simply include the required QuickLink DLLs in the local program 
	directory.  The QuickLink class need not be instantiated at all.  Instead, 
	the static methods in the QuickLinkAPI class can be called directly.
	instantiating anything.  The first call you make loads the DLLs.

HOW TO USE THE WRAPPER IN YOUR VISUAL STUDIO 2010 PROJECT:
	1. Download and install the Quick Glance software from EyeTech’s website.

	2. Open your project in Visual Studio 2010

	3. In the "Solution Explorer" right-click on your solution and select
	"Add -> Existing Project" from the context menu.

	4. Browse to, and select the "QuickLinkDotNet.csproj" file.  You should
	see the project appear in the "Solution Explorer" along with yours.

	5. Then, in the "Solution Explorer," right click on the "References" item
	in _your_ project (not the entire solution), and select "Add Reference"
	from the context menu.  An "Add Reference" window should appear.

	6. In the "Add Reference" window, select the "Projects" tab and highlight 
	"QuickLinkDotNet," then press the "Ok" button.
	
	7. Towards the top of each source file from which you want to call the 
	QuickLink API (with the already present "using" statements) add the line
	"using QuickLinkDotNet;"

	8. Instantiate the QuickLink class, and us the methods available in that 
	class.  Or, use any of the 3 methods listed in the Program Usage section of
	this document to load the QuickLink DLL files.
	
	9. 
	For a 32-bit program:
		In the "Solution Explorer", right-click on your project's name (not the
		entire solution) and select "Properties."  Under the "Build" tab, 
		choose "x86" for the "Platform Target".  This is commonly the default.
	For a 64-bit program:
		In the "Solution Explorer", right-click on your project's name (not the
		entire solution) and select "Properties."  Under the "Build" tab, 
		choose "x64" for the "Platform Target".  Additionally, right-click on 
		the QuickLinkDotNet project, and select "Properties".  Under the 
		"Build" tab, type "ISX64" in the "Conditional compilation symbols" box.

	10. From Visual Studio's drop down menus, select "Build -> Build Solution".
	
	11. Run your program.

NOTES:
	1. There is a known problem with Quick Glance software.  Sometimes when the
	Quick Glance program is initially started, the eye tracker continuously
	looses and then relocates the use eyes every few seconds.  When this
	happens, just restart your program until it works properly.  Note that the
	problem only occurs when Quick Glance is started, so it won't suddenly
	start happening while you are using a program.  If it happens, you will
	know right away.