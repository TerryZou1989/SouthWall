using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Runtime;

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
            var logEntity = new RequestLogsEntity();
            await RequestLog(context, logEntity);
            // 复制原始响应流
            var originalResponseBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                string action =context.Request.RouteValues["action"]?.ToString().ToLower();

                if (!IsBrowserRequest(context.Request)&&action!= "autoaddpartitions")
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden: Non-browser requests are not allowed.");
                }
                else
                {
                    // 调用下一个中间件
                    await _next(context);
                }
                // 记录响应信息
                await ResponseLog(context, logEntity);
                // 将响应流内容复制回原始响应流
                await responseBody.CopyToAsync(originalResponseBodyStream);
            }

            // 保存合并的日志
            await SaveLog(logEntity);
        }

        private bool IsBrowserRequest(HttpRequest request)
        {
            var userAgent = request.Headers["User-Agent"].ToString();
            return !string.IsNullOrEmpty(userAgent) &&
                   (userAgent.Contains("Mozilla") || userAgent.Contains("Chrome") || userAgent.Contains("Safari") || userAgent.Contains("Edge"));
        }

        private async Task RequestLog(HttpContext context, RequestLogsEntity logEntity)
        {
            var request = context.Request;
            string ip = GetClientRealIp(request);
            var ipinfo = await IPHelper.GetIPInfo(ip);
            logEntity.F_DataId = request.RouteValues["id"]?.ToString();
            logEntity.F_Url = request.Path;
            logEntity.F_Method = request.Method;
            logEntity.F_UserAgent = request.Headers["User-Agent"];
            logEntity.F_Module = request.RouteValues["action"]?.ToString();
            logEntity.F_IP = ip;
            logEntity.F_City = ipinfo.City;
            logEntity.F_Country = ipinfo.Country;
            logEntity.F_Province = ipinfo.RegionName;
            logEntity.F_Lat = ipinfo.Lat;
            logEntity.F_Lon = ipinfo.Lon;
        }
        private async Task ResponseLog(HttpContext context, RequestLogsEntity logEntry)
        {
            var response = context.Response;
            response.Body.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            logEntry.F_StatusCode = response.StatusCode;
        }

        private async Task SaveLog(RequestLogsEntity logEntry)
        {
            using (var scope = _ServiceScopeFactory.CreateScope())
            {
                var requestLogsService = scope.ServiceProvider.GetRequiredService<IRequestLogsService>();
                await requestLogsService.Save(logEntry);
            }
        }

        private string GetClientRealIp(HttpRequest request)
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
