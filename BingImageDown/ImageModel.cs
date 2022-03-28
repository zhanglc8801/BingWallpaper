using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BingImageDown
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

        public string copyright { get; set; }

        public string copyrightlink { get; set; }

        public string quiz { get; set; }

        public string hsh { get; set; }
    }
}
