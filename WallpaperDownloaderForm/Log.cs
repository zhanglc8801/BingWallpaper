using System;
using System.Collections.Generic;
using System.Text;

namespace WallpaperDownloaderForm
{
    internal static class Log
    {
        internal static void WriteLog(string Content)
        {
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "logs\\" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(Content);
            sw.Close();
        }
    }
}
