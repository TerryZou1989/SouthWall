namespace SouthWall
{
    public class ServiceBase
    {
        protected readonly ITimesDBAccess _TimesDBAccess;
        protected readonly IVideosDBAccess _VideosDBAccess;
        protected readonly IArticlesDBAccess _ArticlesDBAccess;
        protected readonly IMessagesDBAccess _MessagesDBAccess;
        protected readonly IWebSitesDBAccess _WebSitesDBAccess;
        protected readonly IShiJusDBAccess _ShiJusDBAccess;
        protected readonly ITouXiangsDBAccess _TouXiangsDBAccess;
        public ServiceBase() { }
        public ServiceBase(ITimesDBAccess timesDBAccess)
        {
            _TimesDBAccess = timesDBAccess;
        }
        public ServiceBase(IVideosDBAccess videosDBAccess)
        {
            _VideosDBAccess = videosDBAccess;
        }
        public ServiceBase(IArticlesDBAccess articlesDBAccess)
        {
            _ArticlesDBAccess = articlesDBAccess;
        }
        public ServiceBase(IMessagesDBAccess messagesDBAccess)
        {
            _MessagesDBAccess = messagesDBAccess;
        }
        public ServiceBase(IWebSitesDBAccess webSitesDBAccess)
        {
            _WebSitesDBAccess = webSitesDBAccess;
        }
        public ServiceBase(IShiJusDBAccess shiJusDBAccess)
        {
            _ShiJusDBAccess = shiJusDBAccess;
        }
        public ServiceBase(ITouXiangsDBAccess touXiangsDBAccess)
        {
            _TouXiangsDBAccess = touXiangsDBAccess;
        }
        public ServiceBase(
            ITimesDBAccess timesDBAccess,
            IVideosDBAccess videosDBAccess,
            IArticlesDBAccess articlesDBAccess,
            IMessagesDBAccess messagesDBAccess,
            IWebSitesDBAccess webSitesDBAccess,
            IShiJusDBAccess shiJusDBAccess,
            ITouXiangsDBAccess touXiangsDBAccess)
        {
            _TimesDBAccess = timesDBAccess;
            _VideosDBAccess = videosDBAccess;
            _ArticlesDBAccess = articlesDBAccess;
            _MessagesDBAccess = messagesDBAccess;
            _ShiJusDBAccess = shiJusDBAccess;
            _TouXiangsDBAccess = touXiangsDBAccess;
            _WebSitesDBAccess = webSitesDBAccess;
        }
    }
}
