#region License

/* QuickLinkDotNet : A .NET wrapper (in C#) for EyeTech's QuickLink API.
 *
 * Copyright (c) 2010-2011 Justin Weaver
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

/* $Id$
 *
 * Description: Some constants for internal use.
 */

#endregion Header Comments

namespace QuickLinkDotNet
{
    public static class QuickConstants
    {
#if ISX64
        public static string[] QuickLinkRegistryKeyLocations =
        {
            @"SOFTWARE\EyeTech Digital Systems\Assistive\11.11.04.12\QuickLinkAPI64",
            @"SOFTWARE\EyeTech Digital Systems\QuickLinkAPI64",
        };

        public static string[] QuickGlanceRegistryKeyLocations =
        {
            @"SOFTWARE\EyeTech Digital Systems\Assistive\11.11.04.12\Quick Glance64",
            @"SOFTWARE\EyeTech Digital Systems\Quick Glance64",
        };
#else
        public static string[] QuickLinkRegistryKeyLocations =
        {
            @"SOFTWARE\EyeTech Digital Systems\Assistive\11.11.04.12\QuickLinkAPI",
            @"SOFTWARE\EyeTech Digital Systems\QuickLinkAPI",
        };

        public static string[] QuickGlanceRegistryKeyLocations =
        {
            @"SOFTWARE\EyeTech Digital Systems\Assistive\11.11.04.12\Quick Glance",
            @"SOFTWARE\EyeTech Digital Systems\Quick Glance",
        };
#endif

        public const string PGRFlyCaptureDLLName = "PGRFlyCapture.dll";
        public const string QuickLinkDllName = "QuickLinkAPI.dll";

        // Quick Glance process name.  Used to check for running Quick Glance.
        public static string ProcessName = "Quick Glance";
    }
}