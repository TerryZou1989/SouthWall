using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Runtime;
using static System.Formats.Asn1.AsnWriter;

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
                string action = context.Request.RouteValues["action"]?.ToString().ToLower();

                if (!IsBrowserRequest(context.Request) && action != "autoaddpartitions")
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
            var ipinfo = await GetIPInfo(ip);
            logEntity.F_DataId = request.RouteValues["id"]?.ToString();
            logEntity.F_Url = request.Path;
            logEntity.F_Method = request.Method;
            logEntity.F_UserAgent = request.Headers["User-Agent"];
            logEntity.F_Module = request.RouteValues["action"]?.ToString();
            logEntity.F_IP = ip;
            if (ipinfo != null)
            {
                logEntity.F_City = ipinfo.F_City;
                logEntity.F_Country = ipinfo.F_Country;
                logEntity.F_Province = ipinfo.F_Province;
                logEntity.F_Lat = ipinfo.F_Lat;
                logEntity.F_Lon = ipinfo.F_Lon;
                logEntity.F_District = ipinfo.F_District;
            }
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

        private async Task<IPInfosEntity> GetIPInfo(string ip)
        {
            using (var scope = _ServiceScopeFactory.CreateScope())
            {
                var ipInfosService = scope.ServiceProvider.GetRequiredService<IIPInfosService>();
                var ipinfoEntity = await ipInfosService.GetByIP(ip);
                if (ipinfoEntity != null)
                {
                    return ipinfoEntity;
                }
                //return null;
               // ip = "111.164.191.14";
                var ipinfo = IPHelper.GetIPInfoByipgeolocation(ip);
                if (ipinfo != null)
                {
                    ipinfoEntity = new IPInfosEntity
                    {
                        F_IP = ip,
                        F_City = ipinfo.City,
                        F_Country = ipinfo.Country,
                        F_Province = ipinfo.RegionName,
                        F_Lat = ipinfo.Lat,
                        F_Lon = ipinfo.Lon,
                        F_District = ipinfo.District
                    };
                    await ipInfosService.Save(ipinfoEntity);
                    return ipinfoEntity;
                }
            }
            return null;
        }
    }
}
