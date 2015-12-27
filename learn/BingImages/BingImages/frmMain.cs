using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BingImages
{
    public partial class frmMain : Form
    {
        private const string _catalogUri = "http://www.bing.com/hpimagearchive.aspx?format=xml&idx=0&n=8&mbl=1&mkt=en-ww";
        private const string _imageUri = "http://bing.com{0}_1920x1080.jpg";

        public frmMain()
        {
            InitializeComponent();
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var results = SyncDownload();

            RefreshContent(results);
        }

        private static WallpapersInfo SyncDownload()
        {
            var sw = Stopwatch.StartNew();
            var client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 0, 30);
            var catalogXmlString = client.GetStringAsync(_catalogUri).Result;
            var xDoc = XDocument.Parse(catalogXmlString);
            var wallpapers = xDoc.Root.Elements("image")
                                .Select(el => new { Desc = el.Element("copyright").Value, Url = el.Element("urlBase").Value })
                                .Select(item => new { Desc = item.Desc, FullImageData = client.DownloadImage(string.Format(_imageUri, item.Url)) })
                                .Select(item => new WallpaperInfo(Helper.GetThumbnail(item.FullImageData), item.Desc)).ToArray();
            
            sw.Stop();
            return new WallpapersInfo(sw.ElapsedMilliseconds, wallpapers);
        }

        private int GetItemTop(int height, int index)
        {
            return index * (height + 8) + 8;
        }

        private void RefreshContent(WallpapersInfo info)
        {
            resultPanel.Controls.Clear();

            resultPanel.Controls.AddRange(info.Wallpapers.SelectMany((wp, i) =>
            {
                return new Control[]
                {
                    new PictureBox
                    {
                        Left = 4,
                        Image = wp.Thumbnail,
                        AutoSize = true,
                        Top = GetItemTop(wp.Thumbnail.Height,i)
                    },
                    new Label
                    {
                        Left = wp.Thumbnail.Width + 8,
                        Top = GetItemTop(wp.Thumbnail.Height,i),
                        Text = wp.Description,
                        AutoSize = true
                    }

                };
            }).ToArray());

            TimeTaken.Text = string.Format("Time: {0}ms", info.Duration);


        }
            
        
    }
}
