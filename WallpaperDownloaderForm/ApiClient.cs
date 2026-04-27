using System;
using System.Collections.Generic;
using System.Text;

namespace WallpaperDownloaderForm
{
    internal static class ApiClient
    {
        // 关键：HttpClient 必须声明为静态单例，避免 Socket 耗尽
        private static readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// 发送 POST 请求（异步版，推荐）
        /// </summary>
        /// <param name="url">目标地址</param>
        /// <param name="postData">JSON 字符串或其他文本内容</param>
        /// <returns>服务器返回的字符串内容</returns>
        internal static async Task<string> SendPostApiAsync(string url, string postData = "")
        {
            try
            {
                // 1. 将数据封装为 HttpContent
                // 这里默认使用 application/json，如果是表单提交可以改为 "application/x-www-form-urlencoded"
                using (HttpContent content = new StringContent(postData, Encoding.UTF8, "application/json"))
                {
                    // 2. 发送 POST 请求
                    using (HttpResponseMessage response = await _httpClient.PostAsync(url, content))
                    {
                        // 3. 检查状态码（如果非 200-299 会抛出异常）
                        response.EnsureSuccessStatusCode();

                        // 4. 读取返回内容
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteLog($"API请求异常: {e.Message}");
                return string.Empty;
            }
        }

        internal static async Task<bool> DownloadFileAsync(string url, string filename)
        {
            try
            {
                // 获取响应头（不立即下载整个内容）
                using (HttpResponseMessage response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    // 建立流连接
                    using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
                           fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None, 81920, true))
                    {
                        // 高效复制流
                        await contentStream.CopyToAsync(fileStream);
                    }
                }

                // 统一设置时间（原逻辑是固定为今天的 08:00）
                DateTime targetTime = DateTime.Today.AddHours(8);
                File.SetCreationTime(filename, targetTime);
                File.SetLastWriteTime(filename, targetTime);
                File.SetLastAccessTime(filename, targetTime);

                return true;
            }
            catch (Exception e)
            {
                Log.WriteLog($"下载失败: {url}, 错误: {e.Message}");
                return false;
            }
        }
    }
}
