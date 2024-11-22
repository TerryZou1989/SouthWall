using Microsoft.AspNetCore.Mvc;
namespace SouthWall
{
    public class TZControllerBase : ControllerBase
    {
        protected readonly IAuthService _AuthService;
        protected readonly ITimesService _TimesService;
        protected readonly IVideosService _VideosService;
        protected readonly IArticlesService _ArticlesService;
        protected readonly IMessagesService _MessagesService;
        protected readonly IShiJusService _ShiJusService;
        protected readonly ITouXiangsService _TouXiangsService;
        public TZControllerBase(
            IAuthService authService,
            ITimesService timesService,
            IVideosService videosService,
            IArticlesService articlesService,
            IMessagesService messagesService,
            IShiJusService shiJusService,
            ITouXiangsService touXiangsService
            )
        {
            _AuthService = authService;
            _TimesService = timesService;
            _VideosService = videosService;
            _ArticlesService = articlesService;
            _MessagesService = messagesService;
            _ShiJusService = shiJusService;
            _TouXiangsService = touXiangsService;
        }

        protected Task<TZResponse> RunAction(CheckAuthType checkAuthType, WellDoneHandler action)
        {
            return Task.Run<TZResponse>(async () =>
            {
                try
                {
                    switch (checkAuthType)
                    {
                        case CheckAuthType.None:
                            break;
                        case CheckAuthType.User:
                            await CheckAuthUser();
                            break;
                        case CheckAuthType.App:
                            await CheckAuthApp();
                            break;
                    }
                }
                catch (Exception auth_ex)
                {
                    return Error403();
                }
                try
                {
                    if (action != null)
                    {
                        return await action();
                    }
                    return Error404();
                }
                catch (Exception ex)
                {
                    return Error500(ex.Message);
                }
            });
        }

        protected async Task CheckAuthUser()
        {
            try
            {
                if (Request.Query.ContainsKey("token"))
                {
                    string token = Request.Query["token"];
                    bool isAuth = await _AuthService.CheckToken(token);
                    if (isAuth)
                    {
                        return;
                    }
                }
                throw new Exception("Unauthorized");
            }
            catch (Exception ex)
            {
                throw new Exception("Unauthorized");
            }
        }

        protected async Task CheckAuthApp()
        {
            try
            {
                string appid = null; string appsecret = null;
                if (Request.Query.ContainsKey("appid"))
                {
                    appid = Request.Query["appid"];
                }
                if (Request.Query.ContainsKey("appsecret"))
                {
                    appsecret = Request.Query["appsecret"];
                }
                if (!string.IsNullOrEmpty(appid) && !string.IsNullOrEmpty(appsecret))
                {
                    bool isAuth = true;// await this.AuthService.CheckApp(appid, appsecret);
                    if (isAuth)
                    {
                        return;
                    }
                }
                throw new Exception("Unauthorized");
            }
            catch (Exception ex)
            {
                throw new Exception("Unauthorized");
            }
        }

        protected TZResponse Success()
        {
            return Success("success");
        }
        protected TZResponse Success(string msg)
        {
            return Success(msg, msg);
        }
        protected TZResponse Success(string msg, dynamic data)
        {
            return ResponseResult(200, msg, data);
        }
        protected TZResponse Error403()
        {
            return ResponseResult(403, "Unauthorized");
        }
        protected TZResponse Error404()
        {
            return ResponseResult(404, "404:no action");
        }
        protected TZResponse Error500()
        {
            return Error500("error");
        }
        protected TZResponse Error500(string msg)
        {
            return ResponseResult(500, msg);
        }
        protected TZResponse ResponseResult(int code, string msg)
        {
            return ResponseResult(code, msg, null);
        }
        protected TZResponse ResponseResult(int code, string msg, dynamic data)
        {
            return new TZResponse { Code = code, Message = msg, Data = data };
        }
    }

    public delegate Task<TZResponse> WellDoneHandler();
}
