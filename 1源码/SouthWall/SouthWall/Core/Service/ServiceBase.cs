namespace SouthWall
{
    public class ServiceBase
    {
        protected readonly ITimesDBAccess _TimesDBAccess;
        protected readonly IVideosDBAccess _VideosDBAccess;
        public ServiceBase() { }
        public ServiceBase(ITimesDBAccess timesDBAccess)
        {
            _TimesDBAccess = timesDBAccess;
        }
        public ServiceBase(IVideosDBAccess videosDBAccess)
        {
            _VideosDBAccess = videosDBAccess;
        }
        public ServiceBase(ITimesDBAccess timesDBAccess,IVideosDBAccess videosDBAccess)
        {
            _TimesDBAccess = timesDBAccess;
            _VideosDBAccess = videosDBAccess;
        }
    }
}
