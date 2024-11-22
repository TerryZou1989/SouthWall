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
             IShiJusService shiJusService,
             ITouXiangsService touXiangsService
             ) : base(authService,
                 timesService,
                 videosService,
                 articlesService,
                messagesService,
                shiJusService,
                touXiangsService)
        {
           
        }

        public IActionResult Index()
        {
            return RedirectToAction("Times");
        }
        public IActionResult Owner()
        {
            return View();
        }
    }
}
