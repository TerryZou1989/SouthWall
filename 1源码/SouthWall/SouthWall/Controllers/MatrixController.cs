using Microsoft.AspNetCore.Mvc;

namespace SouthWall.Controllers
{
    public class MatrixController : PageControllerBase
    {
        public MatrixController(
            IAuthService authService,    
            IRequestLogsService requestLogsService,
             IDatasService datasService, 
             ITimesService timesService,
             IVideosService videosService,
             IAudiosService audiosService,
             IArticlesService articlesService,
             IMessagesService messagesService,
             IWebSitesService webSitesService,
             IShiJusService shiJusService,
             ITouXiangsService touXiangsService
             ):base(authService,
                 requestLogsService,
                 datasService,
                 timesService,
                 videosService,
                 audiosService,
                 articlesService,
                messagesService,
                webSitesService,
                shiJusService,
                touXiangsService)
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
        public async Task<IActionResult> Datas() {
            var list = await this._DatasService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
        public async Task<IActionResult> ShiJus()
        {
            var list = await this._ShiJusService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
        public async Task<IActionResult> TouXiangs()
        {
            var list = await this._TouXiangsService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
    }
}
