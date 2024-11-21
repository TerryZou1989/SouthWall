namespace SouthWall
{
    public class ServiceBase
    {
        protected readonly ITimesDBAccess _TimesDBAccess;
        protected readonly IVideosDBAccess _VideosDBAccess;
        protected readonly IArticlesDBAccess _ArticlesDBAccess;
        protected readonly IMessagesDBAccess _MessagesDBAccess;
        protected readonly IShiJusDBAccess _ShiJusDBAccess;
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
        public ServiceBase(IShiJusDBAccess shiJusDBAccess)
        {
            _ShiJusDBAccess = shiJusDBAccess;
        }
        public ServiceBase(
            ITimesDBAccess timesDBAccess,
            IVideosDBAccess videosDBAccess,
            IArticlesDBAccess articlesDBAccess,
            IMessagesDBAccess messagesDBAccess,
            IShiJusDBAccess shiJusDBAccess)
        {
            _TimesDBAccess = timesDBAccess;
            _VideosDBAccess = videosDBAccess;
            _ArticlesDBAccess = articlesDBAccess;
            _MessagesDBAccess = messagesDBAccess;
            _ShiJusDBAccess = shiJusDBAccess;
        }
    }
}
