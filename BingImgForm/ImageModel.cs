using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingImgForm
{
    public class ImageModel
    {
        public List<ImageList> images { get; set; }
    }

    public class ImageList
    {
        public string startdate { get; set; }

        public string fullstartdate { get; set; }

        public string enddate { get; set; }

        public string url { get; set; }

        public string url_full {
            get {
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


        public string copyright { get; set; }

        public string copyrightlink { get; set; }

        public string quiz { get; set; }

        public string hsh { get; set; }

        public string ImageTitie {
            get {
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
                return url + "|" + ImageTitie;
            }
        }
    }
}
