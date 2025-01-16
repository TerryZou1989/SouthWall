namespace SouthWall
{
    public class HttpHelper
    {
        private static readonly HttpClient _httpClient;
        // 静态构造函数，确保 HttpClient 只初始化一次
        static HttpHelper()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout=TimeSpan.FromSeconds(10);
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36 Edg/132.0.0.0");
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
