﻿using Microsoft.AspNetCore.Mvc;
namespace SouthWall
{
    public class PageControllerBase : Controller
    {
        protected readonly IAuthService _AuthService;
        protected readonly ITimesService _TimesService;
        protected readonly IVideosService _VideosService;
        protected readonly IAudiosService _AudiosService;
        protected readonly IArticlesService _ArticlesService;
        protected readonly IMessagesService _MessagesService;
        protected readonly IWebSitesService _WebSitesService;
        protected readonly IShiJusService _ShiJusService;
        protected readonly ITouXiangsService _TouXiangsService;
        public PageControllerBase(
            IAuthService authService,
            ITimesService timesService,
            IVideosService videosService,
            IAudiosService audiosService,
            IArticlesService articlesService,
            IMessagesService messagesService,
            IWebSitesService webSitesService,
            IShiJusService shiJusService,
            ITouXiangsService touXiangsService
            )
        {
            _AuthService = authService;
            _TimesService = timesService;
            _VideosService = videosService;
            _AudiosService = audiosService;
            _ArticlesService = articlesService;
            _MessagesService = messagesService;
            _WebSitesService = webSitesService;
            _ShiJusService = shiJusService;
            _TouXiangsService = touXiangsService;
        }
        public async Task<IActionResult> Times()
        {
            var list = await this._TimesService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
        public async Task<IActionResult> Videos()
        {
            var list = await this._VideosService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
        public async Task<IActionResult> Audios()
        {
            var list = await this._AudiosService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
        public async Task<IActionResult> Articles()
        {
            var list = await this._ArticlesService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
        public async Task<IActionResult> Article(string id)
        {
            var entity = await this._ArticlesService.GetById(id);
            if(entity == null)
            {
                return RedirectToAction("Articles");
            }
            this.ViewData["entity"] = entity;
            return View();
        }
        public async Task<IActionResult> Messages()
        {
            var list = await this._MessagesService.GetList(null);
            var txlist = await this._TouXiangsService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            this.ViewData["txlist"] = txlist.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
        public async Task<IActionResult> WebSites()
        {
            var list = await this._WebSitesService.GetList(null);
            this.ViewData["list"] = list.OrderByDescending(t => t.F_CreateTime).ToList();
            return View();
        }
    }
}
