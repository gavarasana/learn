using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingImages
{
    public static class Helper
    {
        public static Image GetThumbnail(Stream imageStream)
        {
            using (imageStream)
            {
                var fullBitmap = Image.FromStream(imageStream);
                return new Bitmap(fullBitmap, 192, 108);                
            }
        }
    }
}
