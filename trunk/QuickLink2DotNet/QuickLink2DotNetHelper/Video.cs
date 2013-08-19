#region License

/* QLHelper: A class library containing some helper methods for use with
 * QuickLink2DotNet.
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

/* $Id: QLTypes.cs 38 2011-05-09 01:07:39Z piranther $
 *
 * Author: Justin Weaver
 * Homepage: http://quicklinkapi4net.googlecode.com
 */

#endregion Header Comments

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuickLink2DotNet;

namespace QuickLink2DotNetHelper
{
    public partial class QLHelper
    {
        /// <summary>
        /// <para>
        /// Given a reference to a filled <see cref="QLFrameData" /> structure, this function returns a
        /// <see cref="Bitmap" /> version of the <see cref="QuickLink2DotNet.QLFrameData.ImageData" />
        /// pointed to by the <see cref="QuickLink2DotNet.QLImageData.PixelData" /> field upon success,
        /// or null on failure.
        /// </para>
        /// <para>
        /// This method does not create a copy of the pixel data.  And, the pixel buffer will be reused
        /// by the QuickLink2 API when the
        /// <see cref="QuickLink2DotNet.QuickLink2API.QLDevice_GetFrame" /> function is called again.  If
        /// a copy of the image is desired, one must explicitly make one, like so:
        /// <c>Bitmap copyOfImage = new Bitmap(imageReturnedFromThisFunction);</c>
        /// </para>
        /// </summary>
        /// <param name="imageData">
        /// A reference to a <see cref="QuickLink2DotNet.QLImageData" /> structure which has come from
        /// within a <see cref="QuickLink2DotNet.QLFrameData" /> structure that was filled in by the
        /// function <see cref="QuickLink2DotNet.QuickLink2API.QLDevice_GetFrame" /> or
        /// <see cref="QuickLink2DotNet.QuickLink2API.QLDeviceGroup_GetFrame" />.
        /// </param>
        /// <returns>
        /// A <see cref="Bitmap"/> version of the <see cref="QuickLink2DotNet.QLFrameData.ImageData" />
        /// pointed to by the <see cref="QuickLink2DotNet.QLImageData.PixelData" /> field upon success,
        /// or null on failure.
        /// </returns>
        public static Bitmap BitmapFromQLImageData(ref QLImageData imageData)
        {
            // Create a new Bitmap from the PixelData (8bpp Indexed).
            Bitmap bitmap;
            try
            {
                bitmap = new Bitmap(imageData.Width, imageData.Height, imageData.Width, PixelFormat.Format8bppIndexed, imageData.PixelData);
            }
            catch (ArgumentException) { return null; }

            // Set the palette of the image to 256 incremental shades of grey.
            ColorPalette greyScalePalette = bitmap.Palette;
            for (int i = 0; i < 256; i++)
            {
                greyScalePalette.Entries[i] = Color.FromArgb(255, i, i, i);
            }
            bitmap.Palette = greyScalePalette;

            return bitmap;
        }
    }
}