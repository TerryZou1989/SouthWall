﻿using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
    }
}
