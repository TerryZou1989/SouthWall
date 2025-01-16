using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SouthWall
{
    public interface IStatisticsServiceService
    {
        Task<List<RequestAndIPCountEntity>> StatisticalRequestAndIPCount(DateTime start, DateTime end);
        Task<List<IPRequestCountEntity>> StatisticalIPRequestCount(DateTime date);
        Task<List<CountryRequestCountEntity>> StatisticalCountryRequestCount(DateTime start, DateTime end);
        Task<List<ProvinceRequestCountEntity>> StatisticalProvicneRequestCount(string country, DateTime start, DateTime end);
        Task<List<ProvinceIPCountEntity>> StatisticalProvicneIPCount(string country, DateTime start, DateTime end);
    }
    public class StatisticsServiceService : ServiceBase, IStatisticsServiceService
    {
        public StatisticsServiceService(IMDBAccess mdBAccess,
            IRequestLogsDBAccess requestLogsDBAccess,
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
            : base(mdBAccess,
                 requestLogsDBAccess,
                 ipInfosDBAccess,
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

        public Task<List<CountryRequestCountEntity>> StatisticalCountryRequestCount(DateTime start, DateTime end)
        {
            return _RequestLogsDBAccess.StatisticalCountryRequestCount(start, end);
        }

        public Task<List<IPRequestCountEntity>> StatisticalIPRequestCount(DateTime date)
        {
            return _RequestLogsDBAccess.StatisticalIPRequestCount(date);
        }

        public Task<List<ProvinceRequestCountEntity>> StatisticalProvicneRequestCount(string country, DateTime start, DateTime end)
        {
            return _RequestLogsDBAccess.StatisticalProvicneRequestCount(country, start, end);
        }

        public Task<List<RequestAndIPCountEntity>> StatisticalRequestAndIPCount(DateTime start, DateTime end)
        {
            return _RequestLogsDBAccess.StatisticalRequestAndIPCount(start, end);
        }
        public Task<List<ProvinceIPCountEntity>> StatisticalProvicneIPCount(string country, DateTime start, DateTime end)
        {
            return _RequestLogsDBAccess.StatisticalProvicneIPCount(country, start, end);
        }
    }
}
