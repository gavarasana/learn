using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingImages
{
    public class WallpaperInfo
    {
        private readonly string _description;
        private Image _thumbnail;

        public WallpaperInfo(Image thumbnail, string description)
        {
            _thumbnail = thumbnail;
            _description = description;
        }

        public Image Thumbnail { get { return _thumbnail; }  }

        public string Description { get { return _description; } }
    }
}
