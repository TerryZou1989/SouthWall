using Microsoft.AspNetCore.Mvc;
namespace SouthWall
{
    public class PageControllerBase : Controller
    {
        protected readonly IAuthService _AuthService;
        protected readonly ITimesService _TimesService;
        protected readonly IVideosService _VideosService;
        protected readonly IArticlesService _ArticlesService;
        protected readonly IMessagesService _MessagesService;
        protected readonly IShiJusService _ShiJusService;
        protected readonly ITouXiangsService _TouXiangsService;
        public PageControllerBase(
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

        public async Task<IActionResult> Times()
        {
            var list = await this._TimesService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
        public async Task<IActionResult> Videos()
        {
            var list = await this._VideosService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
        public async Task<IActionResult> Articles()
        {
            var list = await this._ArticlesService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
        public async Task<IActionResult> Messages()
        {
            var list = await this._MessagesService.GetList(null);
            var txlist = await this._TouXiangsService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            this.ViewData["txlist"] = txlist.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }

    }
}
