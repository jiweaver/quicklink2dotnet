QuickLinkAPI4NET : A .NET wrapper (in C#) for EyeTech's QuickLink API.

Author: Justin Weaver
Homepage: http://quicklinkapi4net.googlecode.com

This is a very simple .NET wrapper (in C#) for EyeTech's QuickLink API.  The 
QuickLink API can be used by developers to control EyeTech's line of eye 
trackers.

The QuickLink API is provided by EyeTech's QuickGlance software, which can be 
obtained from their website (http://eyetechds.com).  The API wrapper uses .NET's
 Interop Services to call QuickLink's unmanaged DLLs.

See "QuickLinkAPI4NET\QuickLinkAPI4NET\QuickLink.cs" for the API wrapper 
methods.

See "QuickLinkAPI4NET_Example\QuickLinkAPI4NET_Example\Program.cs" for an example
of how to use the API wrapper in your code.

This version has been tested with "EyeTech Beta Installer" version 10.05.20

Note: 64bit QuickLink support is untested!  You can enable it by uncommenting 
this line (which you will find toward the top of QuickLink.cs):
//#define SYSTEM_64BIT
