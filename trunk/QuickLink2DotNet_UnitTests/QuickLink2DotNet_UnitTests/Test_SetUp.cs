#region License

/* Description: QuickLink2DotNet Unit Tests
 * Unit Test Authors: Brianna Peters, Maulik Mistry, Justin Weaver
 *
 * Copyright © 2009-2012 EyeTech Digital Systems <support@eyetechds.com>
 * Copyright © 2013 Justin Weaver
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

/* Note: These unit tests are part of QuickLink2DotNet, which is a .NET wrapper
 * for the QuickLink2 API used to control eye trackers produced by EyeTech
 * Digital Systems, Inc. <http://www.eyetechds.com>.  QuickLink2DotNet is not a
 * product of EyeTech Digital Systems, Inc.
 *
 * QuickLink2DotNet Homepage: http://quicklinkapi4net.googlecode.com
 * QuickLink2DotNet Copyright © 2011-2013 Justin Weaver
 */

#endregion License

#region Notes

// NOTE: The test setup also serves as a basic test for: QLDevice_Enumerate(), QLDevice_GetInfo(),
// QLSettings_Load(), QLSettings_GetStringSize(), and QLSettings_GetValueString().  So, those tests do
// not explicitly appear in the rest of the unit test suite; though, some of them are used several
// additional times.

#endregion Notes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using QuickLink2DotNet;
using QuickLink2DotNetHelper;

namespace QuickLink2DotNet_UnitTests
{
    [SetUpFixture]
    class Test_SetUp
    {
        public static QLHelper Helper;

        private static string note =
            "▌NOTE▐► The test setup also serves as a basic test for: QLDevice_SetPassword(), QLDevice_Enumerate()," + Environment.NewLine +
            "QLDevice_GetInfo(), QLSettings_Load(), QLSettings_GetStringSize(), and QLSettings_GetValueString()." + Environment.NewLine +
            "So, those tests do not explicitly appear in the rest of the unit test suite; though, some of them are" + Environment.NewLine +
            "used several additional times within other tests." + Environment.NewLine;

        [SetUp]
        public void RunBeforeAnyTests()
        {
            Console.WriteLine(note);

            Helper = QLHelper.ChooseDevice();
            if (Helper == null)
            {
                Assert.Ignore("Cannot run tests without a device.");
                return;
            }

            if (!Helper.SetupPassword())
            {
                Assert.Ignore("Failed to setup password for device.");
                return;
            }
        }
    }
}