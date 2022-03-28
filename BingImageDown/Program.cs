using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BingImageDown
{
    class Program
    {
        private const string baseUrl = "http://cn.bing.com";
        static void Main(string[] args)
        {
            string url = "http://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=100";
            #region Url说明
            //https://oss.so/article/55
            //根据上面地址的结构，暂时确定就三项属性有效:
            //1、format，非必要。我理解为输出格式，不存在或者不等于js，即为xml格式，等于js时，输出json格式；
            //2、idx，非必要。不存在或者等于0时，输出当天的图片，-1为已经预备用于明天显示的信息，1则为昨天的图片，idx最多获取到之前16天的图片信息；*
            //3、n，必要。这是输出信息的数量，比如n = 1，即为1条，以此类推，至多输出8条；*
            //*号注释：此处要注意的是，是否正常的输出信息，与n和idx有关，通过idx的值，就可以获得之前bing所使用的背景图片的信息了。 
            #endregion
            string _ret = HttpHelper.SendPostApi(url, null);
            ImageModel entity = JsonHelper.ConvertToObject<ImageModel>(_ret);
            string imgurl = baseUrl + entity.images[0].url;
            string imgurl_4k = imgurl.Replace("_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp", "_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4");
            string imgurl_full = imgurl.Replace("_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp", "_UHD.jpg");

            string imgName = entity.images[0].enddate;
            string imgName_4k = imgName, imgName_full = imgName;
            string imgMsg = entity.images[0].copyright.Replace("（","(").Replace("）",")");

            if (imgMsg.IndexOf('(')!=-1)
                imgMsg = imgMsg.Substring(0, imgMsg.IndexOf('('));
            imgMsg= imgMsg.Replace("/","-").Replace("|","l").Replace("\\","-").Replace(" ","");
            if(imgMsg.LastIndexOf(' ')>0)
            {
                imgMsg = imgMsg.Substring(0, imgMsg.Length - 1);
            }
            imgName += "(" + imgMsg + ").jpg";
            imgName_4k+= "(" + imgMsg + ")_4K.jpg";
            imgName_full += "(" + imgMsg + ")_原始.jpg";

            string savePath = AppDomain.CurrentDomain.BaseDirectory + "images\\" + imgName;
            if (!File.Exists(savePath))
            {
                if(HttpHelper.DownloadFile(imgurl, savePath))
                    Console.WriteLine("文件：" + entity.images[0].fullstartdate + "已下载完成！");
                else
                    Console.WriteLine("文件：" + entity.images[0].fullstartdate + "下载失败！");

            }   

            string savePath_4K = AppDomain.CurrentDomain.BaseDirectory + "images\\" + imgName_4k;
            if (!File.Exists(savePath_4K))
            {
                if (HttpHelper.DownloadFile(imgurl_4k, savePath_4K))
                    Console.WriteLine("文件：" + entity.images[0].fullstartdate + "(4K)已下载完成！");
                else
                    Console.WriteLine("文件：" + entity.images[0].fullstartdate + "下载失败！");

            }

            string savePath_Full = AppDomain.CurrentDomain.BaseDirectory + "images\\" + imgName_full;
            if (!File.Exists(savePath_Full))
            {
                if (HttpHelper.DownloadFile(imgurl_full, savePath_Full))
                    Console.WriteLine("文件：" + entity.images[0].fullstartdate + "(原始)已下载完成！");
                else
                    Console.WriteLine("文件：" + entity.images[0].fullstartdate + "下载失败！");

            }
            //Console.Write("按任意键退出...");
            //Console.ReadKey(true);
        }
    }
}
