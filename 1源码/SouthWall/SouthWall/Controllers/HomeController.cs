using Microsoft.AspNetCore.Mvc;
using SouthWall.Models;
using System.Diagnostics;

namespace SouthWall.Controllers
{
    public class HomeController : PageControllerBase
    {
        public HomeController(IAuthService authService,
             ITimesService timesService,
             IVideosService videosService,
             IArticlesService articlesService,
             IMessagesService messagesService,
             IShiJusService shiJusService
             ) : base(authService,
                 timesService,
                 videosService,
                 articlesService,
                messagesService,
                shiJusService)
        {
           
        }

        public IActionResult Index()
        {
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

        public IActionResult Owner()
        {
            return View();
        }

        public async Task<IActionResult> MessageBox() {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
