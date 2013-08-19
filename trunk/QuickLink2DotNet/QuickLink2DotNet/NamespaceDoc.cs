#region License

/* QuickLink2DotNet: A wrapper written in C# to expose EyeTech Digital System's
 * unmanaged QuickLink2 API for use in the managed Microsoft .NET environment.
 *
 * Copyright © 2011-2013 Justin Weaver
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 * IN THE SOFTWARE.
 */

#endregion License

#region Header Comments

/* $Id: QuickLink2.cs 38 2011-05-09 01:07:39Z piranther $
 *
 * Description: This file contains the main namespace XML documentation for
 * QuickLink2DotNet which is used to build the help document.
 *
 * ATTENTION!!: This wrapper requires that you place the QuickLink2.dlls in the
 * same directory as your program executable.  You can download QuickLink2 from
 * http://www.eyetechds.com/support/downloads.
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickLink2DotNet
{
    /// <summary>
    /// <para>
    /// <b>The <see cref="QuickLink2DotNet"/> namespace contains a wrapper written by
    /// <a href="http://www.justinweaver.com">Justin Weaver</a> for use with the
    /// QuickLink2 API made by <a href="http://eyetechds.com">EyeTech Digital
    /// Systems, Inc.</a></b>
    /// </para>
    /// <para>
    /// QuickLink2DotNet Homepage: <a href="http://quicklinkapi4net.googlecode.com">http://quicklinkapi4net.googlecode.com</a>.
    /// </para>
    /// <para>
    /// This Version of QuickLink2DotNet is for QuickLink2 API v2.5.1.0 (released on Aug 8, 2012).
    /// </para>
    /// <para>
    /// <hr/>
    /// </para>
    /// <para>
    /// LICENSE:
    /// </para>
    /// <para>
    /// <em>QuickLink2DotNet is Copyright © 2011-2013 Justin Weaver.</em>
    /// </para>
    /// <para>
    /// <em>Permission is hereby granted, free of charge, to any person obtaining a copy
    /// of this software and associated documentation files (the "Software"), to
    /// deal in the Software without restriction, including without limitation the
    /// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
    /// sell copies of the Software, and to permit persons to whom the Software is
    /// furnished to do so, subject to the following conditions:</em>
    /// </para>
    /// <para>
    /// <em>The above copyright notice and this permission notice shall be included in
    /// all copies or substantial portions of the Software.</em>
    /// </para>
    /// <para>
    /// <em>THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    /// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    /// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    /// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    /// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
    /// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
    /// IN THE SOFTWARE.</em>
    /// </para>
    /// <para>
    /// <hr/>
    /// </para>
    /// <para>
    /// SPECIAL THANKS:
    /// </para>
    /// <para>
    /// To the <a href="http://www.uaa.alaska.edu">University of Alaska Anchorage</a>
    /// <a href="http://www.uaa.alaska.edu/mathematicalsciences/">Department of
    /// Mathematical Sciences</a> for allowing me to use an EyeTech TM3 eye tracker
    /// for this project.
    /// </para>
    /// <para>
    /// To <a href="http://www.eyetechds.com">EyeTech Digital Systems, Inc.</a> for generously
    /// lending me an EyeTech TM3 eye tracker, which allows me to keep this wrapper
    /// updated.
    /// </para>
    /// <para>
    /// To Caleb Hinton, Brianna Peters, Maulik Mistry, and everyone else at
    /// <a href="http://www.eyetechds.com">EyeTech Digital Systems, Inc.</a> for their
    /// continuing help and technical support, and for their contributions of code
    /// examples and unit tests (see individual files).
    /// </para>
    /// <para>
    /// <hr/>
    /// </para>
    /// <para>
    /// <para>
    /// HOW THE WRAPPER WORKS:
    /// </para>
    /// The API wrapper loads QuickLink2's unmanaged libraries into local memory
    /// space using .NET's InteropServices.  Microsoft's MSDN website provides a
    /// variety of helpful resources on the subject of working with unmanaged DLL
    /// functions from within managed code.
    /// </para>
    /// <para>
    /// <hr/>
    /// </para>
    /// <para>
    /// HOW TO USE THE WRAPPER IN YOUR VISUAL STUDIO 2010 PROJECT:
    /// </para>
    /// <para>
    /// <list type="number">
    /// <item>
    /// <description>
    /// Download and install the QuickLink2 software from EyeTech’s website.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Open your project in Visual Studio 2010
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// In the "Solution Explorer" right-click on your solution and select
    /// "Add -> Existing Project" from the context menu.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Browse to, and select the "QuickLinkDotNet.csproj" file.  You should see
    /// the project appear in the "Solution Explorer" along with yours.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Then, in the "Solution Explorer", right click on the "References" item
    /// in _your_ project, and select "Add Reference" from the context menu.  An
    /// "Add Reference" window should appear.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// In the "Add Reference" window, select the "Projects" tab and highlight
    /// "QuickLinkDotNet", then press the "Ok" button.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Towards the top of each source file from which you want to call the
    /// QuickLink2 API (with the already present "using" statements) add the line
    /// "using QuickLink2DotNet;"
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Use any of the methods, classes, structs, or enums declared in
    /// QLTypes.cs and QuickLink2.cs that you want to.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <para>
    /// Choose the project's architecture.
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// <em>For a 32-bit program:</em>
    /// In the "Solution Explorer", right-click on your project's name (not the
    /// entire solution) and select "Properties."  Under the "Build" tab,
    /// choose "x86" for the "Platform Target".  This is commonly the default.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <em>For a 64-bit program:</em>
    /// In the "Solution Explorer", right-click on your project's name (not the
    /// entire solution) and select "Properties."  Under the "Build" tab,
    /// choose "x64" for the "Platform Target".  Additionally, right-click on
    /// the QuickLink2DotNet project, and select "Properties".  Under the
    /// "Build" tab, type "ISX64" in the "Conditional compilation symbols" box.
    /// </description>
    /// </item>
    /// </list>
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// From Visual Studio's drop down menus, select "Build -> Build Solution".
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <para>
    /// Copy the QuickLink2 DLL files to your program's directory.
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// <em>For a 32-bit program (see also Step 9):</em>
    /// Copy the required QuickLink2 DLLs into you program's directory.  These
    /// DLLs are likely in "Program Files\EyeTechDS\QuickLink2_x.x.x.x\bin"
    /// (where the "x"s are the version number of QuickLink2), unless you chose
    /// to customize QuickLink2's installation directory.  Copy all of the
    /// files in the aforementioned directory into your program's directory
    /// (which is usually either "bin\Debug" or "bin\Release", but can be
    /// different if you created a custom configuration).  If you don't perform
    /// this step properly, you will get a message like this when you attempt
    /// to run your program: "Unable to load DLL 'QuickLink2.dll': The
    /// specified module could not be found."
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <em>For a 64-bit program (see also Step 9):</em>
    /// If you chose "x64" as the "Platform" in Step 9, then follow the 32-bit
    /// directions above, but the DLLs you need to copy are likely in
    /// "Program Files\EyeTechDS\QuickLink2_x.x.x.x\bin64".
    /// </description>
    /// </item>
    /// </list>
    /// </description>
    /// </item>
    /// <item>
    /// <description>Run your program.
    /// </description>
    /// </item>
    /// </list>
    /// </para>
    /// <para>
    /// <hr/>
    /// </para>
    /// <para>
    /// <para>
    /// NOTES:
    /// </para>
    /// <para>
    /// There is a known problem with QuickLink2 software.  Sometimes when the
    /// QuickLink2 API is initially started, the eye tracker continuously loses
    /// and then relocates the user's eyes every few seconds.  When this happens,
    /// just restart your program until it works properly.  Note that the problem
    /// only occurs when QuickLink2 is started, so it won't suddenly start
    /// happening while you are using a program.  If it happens, you will know
    /// right away.
    /// </para>
    /// </para>
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc { }
}