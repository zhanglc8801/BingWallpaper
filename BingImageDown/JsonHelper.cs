using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using System.Reflection;
using System.Globalization;

namespace BingImageDown
{
    public class JsonHelper
    {
        /// <summary>
        /// 使用JSON.NET 转换对象到JSON字符串
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ConvertToJosnString(object item)
        {
            if (item != null)
            {
                return JsonConvert.SerializeObject(item);
            }
            return "";
        }

        /// <summary>
        /// 使用JSON.NET 转换JSON字符串到.NET对象
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T ConvertToObject<T>(string strJson)
        {
            if (!string.IsNullOrEmpty(strJson))
            {
                return JsonConvert.DeserializeObject<T>(strJson, new JsonSerializerSettings() { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            return default(T);
        }

        /// <summary>
        /// 使用JSON.NET 转换对象到JSON字符串
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ConvertToJosnNS(object item)
        {
            if (item != null)
            {
                return JsonConvert.SerializeObject(item);
            }
            return "";
        }

    }
}
