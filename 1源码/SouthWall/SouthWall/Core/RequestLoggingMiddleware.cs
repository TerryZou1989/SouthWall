using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;

namespace SouthWall
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _ServiceScopeFactory;

        public RequestLoggingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _ServiceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await RequestLog(context);
            // 调用下一个中间件
            await _next(context);
        }

        private async Task RequestLog(HttpContext context)
        {
            using (var scope = _ServiceScopeFactory.CreateScope())
            {
                var requestLogsService = scope.ServiceProvider.GetRequiredService<IRequestLogsService>();
                var request = context.Request;
                string ip = GetClientRealIp(request);
                var ipinfo = await IPHelper.GetIPInfo(ip);
                await requestLogsService.Save(
                 new RequestLogsEntity
                 {
                     F_DataId = request.RouteValues["id"]?.ToString(),
                     F_Url = request.Path,
                     F_Method = request.Method,
                     F_UserAgent = request.Headers["User-Agent"],
                     F_Module = request.RouteValues["action"]?.ToString(),
                     F_IP = ip,
                     F_City = ipinfo.City,
                     F_Country = ipinfo.Country,
                     F_Province = ipinfo.RegionName,
                     F_Lat = ipinfo.Lat,
                     F_Lon = ipinfo.Lon
                 });
            }
        }

        public string GetClientRealIp(HttpRequest request)
        {
            // 获取代理头 X-Forwarded-For 或自定义 Real-IP
            var realIp = request.Headers["X-Forwarded-For"].FirstOrDefault();

            // 如果没有 X-Forwarded-For，则检查是否有 Real-IP
            if (string.IsNullOrEmpty(realIp))
            {
                realIp = request.Headers["X-Real-IP"].FirstOrDefault();
            }

            // 如果都没有，使用客户端直接的 IP
            if (string.IsNullOrEmpty(realIp))
            {
                realIp = request.HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            return realIp;
        }
    }
}
