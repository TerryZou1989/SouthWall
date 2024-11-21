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
        public PageControllerBase(
            IAuthService authService,
            ITimesService timesService,
            IVideosService videosService,
            IArticlesService articlesService,
            IMessagesService messagesService,
            IShiJusService shiJusService
            )
        {
            _AuthService = authService;
            _TimesService = timesService;
            _VideosService = videosService;
            _ArticlesService = articlesService;
            _MessagesService = messagesService;
            _ShiJusService = shiJusService;
        }
    }
}
