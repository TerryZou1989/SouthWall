namespace SouthWall
{
    public class ServiceBase
    {
        protected readonly IMDBAccess _MDBAccess;
        protected readonly IIPInfosDBAccess _IPInfosDBAccess;
        protected readonly IRequestLogsDBAccess _RequestLogsDBAccess;
        protected readonly IDatasDBAccess _DatasDBAccess;
        protected readonly ITimesDBAccess _TimesDBAccess;
        protected readonly IVideosDBAccess _VideosDBAccess;
        protected readonly IAudiosDBAccess _AudiosDBAccess;
        protected readonly IArticlesDBAccess _ArticlesDBAccess;
        protected readonly IMessagesDBAccess _MessagesDBAccess;
        protected readonly IWebSitesDBAccess _WebSitesDBAccess;
        protected readonly IShiJusDBAccess _ShiJusDBAccess;
        protected readonly ITouXiangsDBAccess _TouXiangsDBAccess;
        public ServiceBase() { }
        public ServiceBase(IMDBAccess mDBAccess)
        {
           _MDBAccess = mDBAccess;
        }
        public ServiceBase(IRequestLogsDBAccess requestLogsDBAccess)
        {
            _RequestLogsDBAccess = requestLogsDBAccess;
        }
        public ServiceBase(IIPInfosDBAccess ipInfosDBAccess)
        {
            _IPInfosDBAccess = ipInfosDBAccess;
        }
        public ServiceBase(IDatasDBAccess datasDBAccess)
        {
            _DatasDBAccess = datasDBAccess;
        }
        public ServiceBase(ITimesDBAccess timesDBAccess)
        {
            _TimesDBAccess = timesDBAccess;
        }
        public ServiceBase(IVideosDBAccess videosDBAccess)
        {
            _VideosDBAccess = videosDBAccess;
        }
        public ServiceBase(IAudiosDBAccess audiosDBAccess)
        {
            _AudiosDBAccess = audiosDBAccess;
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
            IMDBAccess mdBAccess,
            IRequestLogsDBAccess  requestLogsDBAccess,
            IIPInfosDBAccess ipInfosDBAccess,
            IDatasDBAccess datasDBAccess,
            ITimesDBAccess timesDBAccess,
            IVideosDBAccess videosDBAccess,
            IAudiosDBAccess audiosDBAccess,
            IArticlesDBAccess articlesDBAccess,
            IMessagesDBAccess messagesDBAccess,
            IWebSitesDBAccess webSitesDBAccess,
            IShiJusDBAccess shiJusDBAccess,
            ITouXiangsDBAccess touXiangsDBAccess)
        {
            _MDBAccess = mdBAccess;
            _RequestLogsDBAccess = requestLogsDBAccess;
            _IPInfosDBAccess = ipInfosDBAccess;
            _DatasDBAccess = datasDBAccess;
            _TimesDBAccess = timesDBAccess;
            _VideosDBAccess = videosDBAccess;
            _AudiosDBAccess = audiosDBAccess;
            _ArticlesDBAccess = articlesDBAccess;
            _MessagesDBAccess = messagesDBAccess;
            _ShiJusDBAccess = shiJusDBAccess;
            _TouXiangsDBAccess = touXiangsDBAccess;
            _WebSitesDBAccess = webSitesDBAccess;
        }
    }
}
