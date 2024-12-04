using System.Net.Http;

namespace SouthWall.Core
{
    public class HttpHelper
    {
        public static async Task<Stream> GetFileStreamAsync(string fileUrl)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                // 发送 GET 请求
                HttpResponseMessage response = await httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode(); // 确保请求成功
                // 返回响应流
                return await response.Content.ReadAsStreamAsync();
            }
            catch (Exception ex)
            {
                return null; // 发生错误时返回 null
            }
        }
    }
}
