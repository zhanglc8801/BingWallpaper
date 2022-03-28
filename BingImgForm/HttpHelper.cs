using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BingImgForm
{
    public class HttpHelper
    {
        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string SendPostApi(string url, string postData)
        {
            string strResult = "";
            string spanTime = string.Empty;
            TimeSpan startTime = new TimeSpan(DateTime.Now.Ticks);
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";
                myRequest.Accept = "application/json";
                myRequest.Timeout = 30000;
                if (postData != null)
                {
                    byte[] buffer = UTF8Encoding.UTF8.GetBytes(postData);
                    myRequest.ContentLength = buffer.Length;
                    Stream reqStream = myRequest.GetRequestStream();
                    reqStream.Write(buffer, 0, buffer.Length);
                    reqStream.Close();
                }
                else
                {
                    myRequest.ContentLength = 0;
                }
                HttpWebResponse HttpWResp = (HttpWebResponse)myRequest.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.UTF8);
                StringBuilder strBuilder = new StringBuilder();

                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }
                strResult = strBuilder.ToString();
                sr.Close();
                myStream.Close();
                if (myRequest != null)
                {
                    myRequest.Abort();
                }
                //记录调用接口后的时间  
                TimeSpan endTime = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan ts = endTime.Subtract(startTime).Duration();
                //计算时间间隔，求出调用接口所需要的时间  
                spanTime = ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分" + ts.Seconds.ToString() + "秒" + ts.Milliseconds.ToString();
                if (ts.Seconds > 10)
                {
                    return "接口调用超时";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            System.GC.Collect();
            return strResult;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool DownloadFile(string URL, string filename)
        {
            try
            {
                HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(URL);
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                Stream st = myrp.GetResponseStream();
                Stream so = new FileStream(filename, FileMode.Create);
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    so.Write(by, 0, osize);
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
                myrp.Close();
                Myrq.Abort();
                return true;
            }
            catch (Exception e)
            {
                Log.WritLog(e.Message);
                return false;
            }
        }
    }
}
