using Microsoft.AspNetCore.Mvc;
namespace SouthWall
{
    public class TZControllerBase : ControllerBase
    {
        protected readonly IAuthService _AuthService;
        protected readonly IDatasService _DatasService;
        protected readonly IRequestLogsService _RequestLogsService;
        protected readonly IIPInfosService _IPInfosService;
        protected readonly ITimesService _TimesService;
        protected readonly IVideosService _VideosService;
        protected readonly IAudiosService _AudiosService;
        protected readonly IArticlesService _ArticlesService;
        protected readonly IMessagesService _MessagesService;
        protected readonly IWebSitesService _WebSitesService;
        protected readonly IShiJusService _ShiJusService;
        protected readonly ITouXiangsService _TouXiangsService;
        protected readonly IStatisticsServiceService _StatisticsServiceService;
        public TZControllerBase(
            IAuthService authService,
            IRequestLogsService requestLogsService,
            IIPInfosService ipInfosService,
            IDatasService datasService,
            ITimesService timesService,
            IVideosService videosService,
            IAudiosService audiosService,
            IArticlesService articlesService,
            IMessagesService messagesService,
            IWebSitesService webSitesService,
            IShiJusService shiJusService,
            ITouXiangsService touXiangsService,
            IStatisticsServiceService statisticsServiceService
            )
        {
            _AuthService = authService;
            _RequestLogsService = requestLogsService;
            _IPInfosService= ipInfosService;
            _DatasService = datasService;
            _TimesService = timesService;
            _VideosService = videosService;
            _AudiosService = audiosService;
            _ArticlesService = articlesService;
            _MessagesService = messagesService;
            _WebSitesService = webSitesService;
            _ShiJusService = shiJusService;
            _TouXiangsService = touXiangsService;
            _StatisticsServiceService = statisticsServiceService;
        }
        /// <summary>
        /// imgs 逗号隔开即可
        /// </summary>
        /// <param name="entity"></param>
        protected Task<int> SaveTimes(TimesType type, DatasEntity entity)
        {
            entity.F_Type = 0;
            if (!string.IsNullOrEmpty(entity.F_Imgs))
            {
                string[] imgs=entity.F_Imgs.Split(";");
                List<object> imglist = new List<object>();
                foreach (string s in imgs) 
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        imglist.Add(new { imgId = Guid.NewGuid().ToString(), imgSrc = s });
                    }
                }
                entity.F_Imgs = imglist.ToJson();
            }
            switch (type)
            {
                case TimesType.Article:
                    entity.F_Title = $"文韵阁‌新收录一篇文章《{entity.F_Title}》";
                    break;
                case TimesType.Video:
                    entity.F_Title = $"光影阁新收录一段视频《{entity.F_Title}》";
                    break;
                case TimesType.WebSite:
                    entity.F_Title = $"梦链阁新收录一个网站《{entity.F_Title}》";
                    break;
                case TimesType.Audio:
                    entity.F_Title = $"天籁阁新收录一首音乐《{entity.F_Title}》";
                    break;
            }
            return this._DatasService.Save(entity);
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
