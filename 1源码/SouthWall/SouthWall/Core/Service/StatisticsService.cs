using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SouthWall
{
    public interface IStatisticsServiceService
    {
        Task<List<RequestAndIPCountEntity>> StatisticalRequestAndIPCount(DateTime start, DateTime end);
        Task<List<IPRequestCountEntity>> StatisticalIPRequestCount(DateTime date);
        Task<List<CountryRequestCountEntity>> StatisticalCountryRequestCount();
        Task<List<ProvinceRequestCountEntity>> StatisticalProvicneRequestCount(string country);
    }
    public class StatisticsServiceService : ServiceBase, IStatisticsServiceService
    {
        public StatisticsServiceService(IMDBAccess mdBAccess,
            IRequestLogsDBAccess requestLogsDBAccess,
            IDatasDBAccess datasDBAccess,
            ITimesDBAccess timesDBAccess,
            IVideosDBAccess videosDBAccess,
            IAudiosDBAccess audiosDBAccess,
            IArticlesDBAccess articlesDBAccess,
            IMessagesDBAccess messagesDBAccess,
            IWebSitesDBAccess webSitesDBAccess,
            IShiJusDBAccess shiJusDBAccess,
            ITouXiangsDBAccess touXiangsDBAccess)
            : base(mdBAccess,
                 requestLogsDBAccess,
                 datasDBAccess,
                 timesDBAccess,
                    videosDBAccess,
                    audiosDBAccess,
                    articlesDBAccess,
                    messagesDBAccess,
                    webSitesDBAccess,
                    shiJusDBAccess,
                    touXiangsDBAccess
                 )
        { }

        public Task<List<CountryRequestCountEntity>> StatisticalCountryRequestCount()
        {
            return _RequestLogsDBAccess.StatisticalCountryRequestCount();
        }

        public Task<List<IPRequestCountEntity>> StatisticalIPRequestCount(DateTime date)
        {
            return _RequestLogsDBAccess.StatisticalIPRequestCount(date);
        }

        public Task<List<ProvinceRequestCountEntity>> StatisticalProvicneRequestCount(string country)
        {
            return _RequestLogsDBAccess.StatisticalProvicneRequestCount(country);
        }

        public Task<List<RequestAndIPCountEntity>> StatisticalRequestAndIPCount(DateTime start, DateTime end)
        {
            return _RequestLogsDBAccess.StatisticalRequestAndIPCount(start, end);
        }
    }
}
