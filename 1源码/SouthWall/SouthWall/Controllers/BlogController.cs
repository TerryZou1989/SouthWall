using Microsoft.AspNetCore.Mvc;
using SouthWall.Models;
using System.Diagnostics;

namespace SouthWall.Controllers
{
    public class BlogController : PageControllerBase
    {
        private readonly IToolService _toolService;
        public BlogController(IAuthService authService,
            IRequestLogsService requestLogsService,
             IDatasService datasService, 
             ITimesService timesService,
             IVideosService videosService,
             IAudiosService audiosService,
             IArticlesService articlesService,
             IMessagesService messagesService,
             IWebSitesService webSitesService,
             IShiJusService shiJusService,
             ITouXiangsService touXiangsService,
             IToolService toolService
             ) : base(authService,
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
            _toolService = toolService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await Times();
        }
        public IActionResult Owner()
        {
            return View();
        }
    }
}
