using System.Text.Json;
using System.Text.Json.Serialization;

namespace WallpaperDownloader
{
    internal class Program
    {
        private const string baseUrl = "http://cn.bing.com";
        static async Task Main(string[] args)
        {
            string url = "http://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=8&mkt=zh-CN";
            string _ret = await ApiClient.SendPostApiAsync(url);
            ImageModel entity = JsonSerializer.Deserialize<ImageModel>(_ret);
            string imgurl = baseUrl + entity.images[0].url;
            string imgurl_4k = imgurl.Replace("_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp", "_UHD.jpg&rf=LaDigue_UHD.jpg&pid=hp&w=3840&h=2160&rs=1&c=4");
            string imgurl_full = imgurl.Replace("_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp", "_UHD.jpg");

            string imgName = entity.images[0].enddate;
            string imgName_4k = imgName, imgName_full = imgName;
            string imgMsg = entity.images[0].copyright.Replace("（", "(").Replace("）", ")");

            if (imgMsg.IndexOf('(') != -1)
                imgMsg = imgMsg.Substring(0, imgMsg.IndexOf('('));
            imgMsg = imgMsg.Replace("/", "-").Replace("|", "l").Replace("\\", "-").Replace(" ", "");
            if (imgMsg.LastIndexOf(' ') > 0)
            {
                imgMsg = imgMsg.Substring(0, imgMsg.Length - 1);
            }
            imgName += "(" + imgMsg + ")_1080P.jpg";
            imgName_4k += "(" + imgMsg + ")_4K.jpg";
            imgName_full += "(" + imgMsg + ")_original.jpg";

            string savePath = AppDomain.CurrentDomain.BaseDirectory + "images\\" + imgName;

            if (!File.Exists(savePath))
            {
                if (await ApiClient.DownloadFileAsync(imgurl, savePath))
                    Console.WriteLine("文件：" + entity.images[0].fullstartdate + "已下载完成！");
                else
                    Console.WriteLine("文件：" + entity.images[0].fullstartdate + "下载失败！");

            }

            string savePath_4K = AppDomain.CurrentDomain.BaseDirectory + "images\\" + imgName_4k;
            if (!File.Exists(savePath_4K))
            {
                if (await ApiClient.DownloadFileAsync(imgurl_4k, savePath_4K))
                    Console.WriteLine("文件：" + entity.images[0].fullstartdate + "(4K)已下载完成！");
                else
                    Console.WriteLine("文件：" + entity.images[0].fullstartdate + "下载失败！");

            }

            string savePath_Full = AppDomain.CurrentDomain.BaseDirectory + "images\\" + imgName_full;
            if (!File.Exists(savePath_Full))
            {
                if (await ApiClient.DownloadFileAsync(imgurl_full, savePath_Full))
                    Console.WriteLine("文件：" + entity.images[0].fullstartdate + "(original)已下载完成！");
                else
                    Console.WriteLine("文件：" + entity.images[0].fullstartdate + "下载失败！");

            }
        }
    }
}
