QuickLinkDotNet: A .NET wrapper (in C#) for EyeTech's QuickLink API.

Homepage: <http://quicklinkapi4net.googlecode.com>

Author: Justin Weaver


--------------
Description:

This is a .NET wrapper (in C#) for EyeTech's QuickLink API.  The QuickLink API 
can be used by developers to control EyeTech's line of eye trackers.  The 
QuickLink API is bundled with EyeTech's Quick Glance software, which can be 
obtained from their website <http://eyetechds.com>.


--------------
How It Works:

The API wrapper loads QuickLink's unmanaged libraries into local memory space 
using .NET's InteropServices.  Microsoft's MSDN website provides a variety of 
helpful resources on the subject of working with unmanaged DLL functions from 
within managed code.


--------------
Usage:

First, download and install the Quick Glance software from EyeTech’s website.
  
Then start Quick Glance to do the initial setup.  A developer who wants to use 
this wrapper in their code has three usage choices:

1.	Instantiate the QuickGlance() class.  It will find and start Quick Glance, 
	if it is not already running.  Then it will find and load the necessary 
	QuickLink DLLs into local program space.  Finally, QuickLink API operations 
	can be accessed via the QuickLink property of the QuickGlance class 
	instance.  Be sure to Dispose() of the QuickGlance class instance later.
	
2. 	Instantiate the QuickLink() class. It will find and load the necessary 
	QuickLink DLLs into local program space.  QuickLink API operations are 
	public methods within the instantiated QuickLink class.  Be sure to 
	Dispose() of the QuickLink class instance later.
	
3. 	Copy the required DLLs into the local program directory.  The public methods
	in the static QuickLinkAPI class can be called without first instantiating 
	anything.  The first call loads the DLLs.

To use QuickLinkDotNet in your project:

1.	Open your project in Visual Studio 2010 Professional

2.	In the "Solution Explorer" right-click on your solution and select "Add -> 
	Existing Project" from the context menu.

3.	Browse to, and select the "QuickLinkDotNet.csproj" file.  You should see 
	the project appear in the "Solution Explorer" along with yours.

4.	Then, in the "Solution Explorer," right click on the "References" item in 
	_your_ project, and select "Add Reference" from the context menu.  An "Add 
	Reference" window should appear.

5.	In the "Add Reference" window, select the "Projects" tab and highlight 
	"QuickLinkDotNet," then press the "Ok" button.


--------------	
32/64-Bit Support:

This is for the 32-bit version of Quick Glance & QuickLink API only.

There are some provisions made within the code for 64-bit support, but I have 
been unable to test anything, because the 64-bit version of Quick Glance always 
crashes on my 64-bit Windows 7 system.

If anyone else has info on this, please let me know.