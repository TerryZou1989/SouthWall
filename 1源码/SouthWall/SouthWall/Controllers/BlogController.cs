using Microsoft.AspNetCore.Mvc;
using SouthWall.Models;
using System.Diagnostics;

namespace SouthWall.Controllers
{
    public class BlogController : PageControllerBase
    {
        public BlogController(IAuthService authService,
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
        [HttpGet]
        public Task<IActionResult> Index()
        {
            return Times();
        }
        public IActionResult Owner()
        {
            return View();
        }
    }
}
