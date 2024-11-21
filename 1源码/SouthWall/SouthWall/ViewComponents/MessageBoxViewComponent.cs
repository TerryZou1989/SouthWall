using Microsoft.AspNetCore.Mvc;

namespace SouthWall.ViewComponents
{
    public class MessageBoxViewComponent: ViewComponentBase
    {
        public MessageBoxViewComponent(IAuthService authService,
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

        public override async Task<IViewComponentResult> InvokeAsync()
        {
            var shiju = await this._ShiJusService.GetByRandom();
            this.ViewData["shiju"] = shiju;
            return View();
        }
    }
}
