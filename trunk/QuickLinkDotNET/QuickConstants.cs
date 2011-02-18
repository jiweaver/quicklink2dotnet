#region License

/* QuickLinkDotNet : A .NET wrapper (in C#) for EyeTech's QuickLink API.
 *
 * Copyright (c) 2010 Justin Weaver
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

#endregion License

#region Header Comments

/* $Id$
 *
 * Description: TODO.
 */

#endregion Header Comments

namespace QuickLinkDotNet
{
    public static class QuickConstants
    {
        public static string[] PossibleRegistryKeyLocations =
        {
            @"SOFTWARE\EyeTech Digital Systems",  // 32-bit.
            //@"SOFTWARE\Wow6432Node\EyeTech Digital Systems" // 64-bit
        };

        public static string[] QuickLinkAPIKeys = new string[]
        {
            "QuickLinkAPI",  // 32-bit.
            //"QuickLinkAPI64", // 64-bit
        };

        public const string PGRFlyCaptureDLLName = "PGRFlyCapture.dll";
        public const string QuickLinkDllName = "QuickLinkAPI.dll";

        // Quick Glance process name.  Used to check for running Quick Glance.
        public static string ProcessName = "Quick Glance";

        public static string QuickGlanceSubKeyName = "Quick Glance";
    }
}