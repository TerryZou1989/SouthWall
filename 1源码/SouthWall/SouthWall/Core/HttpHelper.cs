namespace SouthWall
{
    public class HttpHelper
    {
        private static readonly HttpClient _httpClient;
        // 静态构造函数，确保 HttpClient 只初始化一次
        static HttpHelper()
        {
            _httpClient = new HttpClient();
        }
        // 发送 GET 请求的方法
        public static async Task<string> GetAsync(string url)
        {
            try
            {
                // 发送 GET 请求
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                // 确保响应成功
                response.EnsureSuccessStatusCode();

                // 获取并返回响应内容
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"GET 请求失败: {ex.Message}");
                return null;
            }
        }
    }
}
