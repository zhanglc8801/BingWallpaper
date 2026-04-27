using System;
using System.Collections.Generic;
using System.Text;

namespace WallpaperDownloaderForm
{
    public class ImageModel
    {
        public List<ImagesItem> images { get; set; }
        public Tooltips tooltips { get; set; }
    }

    public class ImagesItem
    {
        public string startdate { get; set; }
        public string fullstartdate { get; set; }
        public string enddate { get; set; }
        public string url { get; set; }
        public string urlbase { get; set; }
        public string copyright { get; set; }
        public string copyrightlink { get; set; }
        public string title { get; set; }
        public string quiz { get; set; }
        public bool wp { get; set; }
        public string hsh { get; set; }
        public int drk { get; set; }
        public int top { get; set; }
        public int bot { get; set; }
        public List<object> hs { get; set; }

        public string ImageTitie
        {
            get
            {
                string str = copyright;
                if (str.IndexOf('(') != -1)
                    str = str.Substring(0, copyright.IndexOf('('));
                str = str.Replace("/", "_").Replace("|", "1").Replace("\\", "_");
                if (str.LastIndexOf(' ') > 0)
                {
                    str = str.Substring(0, str.Length - 1);
                }
                return str;
            }
        }

        public string urlStr
        {
            get
            {
                return url + "|" + ImageTitie + "|" + title;
            }
        }

        public string url_full
        {
            get
            {
                return url.Replace("_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp", "_UHD.jpg|" + ImageTitie);
            }
        }

        public string url_2k
        {
            get
            {
                return url.Replace("_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp", "_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=1920&h=1080&rs=1&c=4|" + ImageTitie);
            }
        }

        public string url_4k
        {
            get
            {
                return url.Replace("_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp", "_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4|" + ImageTitie);
            }
        }
        
    }

    public class Tooltips
    {
        public string loading { get; set; }
        public string previous { get; set; }
        public string next { get; set; }
        public string walle { get; set; }
        public string walls { get; set; }
    }
}
