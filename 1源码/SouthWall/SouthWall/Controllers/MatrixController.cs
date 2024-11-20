using Microsoft.AspNetCore.Mvc;

namespace SouthWall.Controllers
{
    public class MatrixController : Controller
    {
        protected readonly IAuthService _AuthService;
        protected readonly ITimesService _TimesService;
        public MatrixController(
            IAuthService authService,
             ITimesService timesService)
        {
            _AuthService = authService;
            _TimesService = timesService;
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
            this.ViewData["timeslist"] = list.OrderByDescending(t=>t.F_CreateTime).ToList();
            return View();
        }
        public IActionResult Videos()
        {
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
