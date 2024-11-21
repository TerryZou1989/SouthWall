using Microsoft.AspNetCore.Mvc;

namespace SouthWall.Controllers
{
    public class MatrixController : PageControllerBase
    {
        public MatrixController(
            IAuthService authService,
             ITimesService timesService,
             IVideosService videosService,
             IArticlesService articlesService,
             IMessagesService messagesService,
             IShiJusService shiJusService
             ):base(authService,
                 timesService,
                 videosService,
                 articlesService,
                messagesService,
                shiJusService)
        {
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
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
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
        public async Task<IActionResult> ShiJus()
        {
            var list = await this._ShiJusService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
    }
}
