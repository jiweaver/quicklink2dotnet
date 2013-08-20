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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using QuickLink2DotNet;

namespace QuickLink2DotNet_UnitTests
{
    /// <summary>
    /// This class tests all of the QLAPI_ methods not tested in QuickStart
    /// </summary>

    [TestFixture]
    class Test_0000_APIMethods
    {
        [Test]
        public void Test_0010_QLAPI_ImportSettings_ExportSettings()
        {
            // There are currently no documented settings.

            // In the future, we will test this very similar to the TestSettings class:
            // Create settings containers
            // Store initial settings
            // Add settings to a container
            // Set settings
            // Import settings
            // Export settings
            // Get settings
            // Check that values were set correctly
            // Restore initial settings

            //Assert.Ignore("No documented settings.  Create tests when settings are added");
        }

        [Test]
        public void Test_0010_QLAPI_GetVersion()
        {
            string expectedVersionString = "2.5.1.0";

            int buffSize = expectedVersionString.Length + 1; // +1 for the null that terminates the string.
            System.Text.StringBuilder version = new System.Text.StringBuilder(buffSize);

            QLError error = QuickLink2API.QLAPI_GetVersion(buffSize, version);
            Assert.AreEqual(QLError.QL_ERROR_OK, error);
            Assert.AreEqual(version.ToString(), expectedVersionString);
        }
    }
}