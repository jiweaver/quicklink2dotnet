QuickLinkDotNet: A .NET wrapper (in C#) for EyeTech's QuickLink API.

Copyright (c) 2010-2011 Justin Weaver
Homepage: http://quicklinkapi4net.googlecode.com


DESCRIPTION:
	This is a .NET wrapper (in C#) for EyeTech's QuickLink API.  The QuickLink
	API can be used by developers to control EyeTech's line of eye trackers.
	The QuickLink API is bundled with EyeTech's Quick Glance software, which can
	be obtained from their website <http://eyetechds.com>.

NOTICE:
	This wrapper is for QuickLink.  The wrapper for the new QuickLink2 is
	also available from <http://quicklinkapi4net.googlecode.com>.

SPECIAL THANKS:
	To the University of Alaska Department of Mathematical Sciences for allowing
	me to use an EyeTech TM3 eye tracker for this project.

HOW THE WRAPPER WORKS:
	The API wrapper loads QuickLink's unmanaged libraries into local memory
	space using .NET's InteropServices.  Microsoft's MSDN website provides a
	variety of helpful resources on the subject of working with unmanaged DLL
	functions from within managed code.

USAGE OPTIONS:
	Then start Quick Glance to do the initial setup.  A developer who wants to
	use this wrapper in their code has two usage choices:

	1. Instantiate the QuickGlance() class.  It will find and start Quick
	Glance, if it is not already running.  Then it will find and load the
	necessary QuickLink DLLs into local program space.  Finally, QuickLink API
	operations can be accessed via the QuickLink property of the QuickGlance
	class instance.  Be sure to Dispose() of the QuickGlance class instance
	later.
	
	2. Copy the required DLLs into the local program directory.  The public
	methods in the static QuickLinkAPI class can be called without first
	instantiating anything.  The first call loads the DLLs.

HOW TO USE THE WRAPPER IN YOUR VISUAL STUDIO 2010 PROJECT:
	1. Download and install the Quick Glance software from EyeTech’s website.

	2. Open your project in Visual Studio 2010

	3. In the "Solution Explorer" right-click on your solution and select
	"Add -> Existing Project" from the context menu.

	4. Browse to, and select the "QuickLinkDotNet.csproj" file.  You should see 
	the project appear in the "Solution Explorer" along with yours.

	5. Then, in the "Solution Explorer," right click on the "References" item in 
	_your_ project, and select "Add Reference" from the context menu.  An "Add 
	Reference" window should appear.

	6. In the "Add Reference" window, select the "Projects" tab and highlight 
	"QuickLinkDotNet," then press the "Ok" button.
	
	7. Towards the top of each source file from which you want to call the 
	QuickLink API (with the already present "using" statements) add the line
	"using QuickLinkDotNet;"

	8. Instantiate the QuickLink class, and us the methods available in that 
	class.  Also, after instantiating the QuickLink class, feel free to use any
	of the methods, classes, structs, or enums declared in QuickLinkAPI.cs
	
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
