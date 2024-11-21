using Microsoft.AspNetCore.Mvc;

namespace SouthWall.Controllers
{
    public class MatrixController : Controller
    {
        protected readonly IAuthService _AuthService;
        protected readonly ITimesService _TimesService;
        protected readonly IVideosService _VideosService;
        public MatrixController(
            IAuthService authService,
             ITimesService timesService,
             IVideosService videosService)
        {
            _AuthService = authService;
            _TimesService = timesService;
            _VideosService = videosService;
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
            var list =await this._TimesService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t=>t.F_CreateTime).ToList();
            return View();
        }
        public async Task<IActionResult> Videos()
        {
            var list = await this._VideosService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
        public IActionResult Articles()
        {
            return View();
        }
        public IActionResult Messages()
        {
            return View();
        }
    }
}
