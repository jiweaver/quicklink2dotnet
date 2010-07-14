QuickLinkAPI4NET : A .NET wrapper (in C#) for EyeTech's QuickLink API.

Homepage: http://quicklinkapi4net.googlecode.com
Author: Justin Weaver


-------------------
Directory Structure

QuickLinkAPI4NET\  	: The API Wrapper.

QuickLinkExample\	: Example program.

README.txt		: This file.


-----------
Description

This is a very simple .NET wrapper (in C#) for EyeTech's QuickLink API.  The 
QuickLink API can be used by developers to control EyeTech's line of eye 
trackers.  The QuickLink API is provided by EyeTech's QuickGlance software, 
which can be obtained from their website at http://eyetechds.com

The wrapper uses .NET's Interop Services to call QuickLink's unmanaged DLLs.


---------------
Getting Started

1. Install EyeTech's QuickGlance software.

2. See QuickLinkExample\Form1.cs for an example of how to use the API wrapper 
in your code.  The actual wrapper methods are in QuickLinkAPI4NET\QuickLink.cs
