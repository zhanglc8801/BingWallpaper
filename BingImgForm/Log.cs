using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingImgForm
{
    public class Log
    {
        public static void WritLog(string Content)
        {
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Log\\" + DateTime.Now.ToString("yyyyMMdd_HH:mm") + ".txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(Content);
            sw.Close();
        }
    }
}
