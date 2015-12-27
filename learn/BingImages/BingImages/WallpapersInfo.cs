using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingImages
{
    public class WallpapersInfo
    {
        private readonly long _milliSeconds;
        private readonly WallpaperInfo[] _wallpapers;

        public WallpapersInfo(long milliSeconds, WallpaperInfo[] wallpapers)
        {
            _milliSeconds = milliSeconds;
            _wallpapers = wallpapers;
        }

        public long Duration { get { return _milliSeconds; } }

        public WallpaperInfo[] Wallpapers { get { return _wallpapers; } }
    }
}
