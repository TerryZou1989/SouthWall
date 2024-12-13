namespace SouthWall
{
    public interface IToolService
    {
        Task SyncData();
    }
    public class ToolService : ServiceBase, IToolService
    {
        public ToolService(
            IRequestLogsDBAccess requestLogsAccess,
             IDatasDBAccess datasDBAccess,
            ITimesDBAccess timesDBAccess,
            IVideosDBAccess videosDBAccess,
            IAudiosDBAccess audiosDBAccess,
            IArticlesDBAccess articlesDBAccess,
            IMessagesDBAccess messagesDBAccess,
            IWebSitesDBAccess webSitesDBAccess,
            IShiJusDBAccess shiJusDBAccess,
            ITouXiangsDBAccess touXiangsDBAccess
            ) : base(
                requestLogsAccess,
                datasDBAccess,
                timesDBAccess,
                videosDBAccess,
                audiosDBAccess,
                articlesDBAccess,
                messagesDBAccess,
                webSitesDBAccess,
                shiJusDBAccess,
                touXiangsDBAccess)
        { }

        public async Task SyncData()
        {
            var list = await _AudiosDBAccess.GetList(null);
            foreach (var item in list)
            {
                await _DatasDBAccess.Save(new DatasEntity
                {
                    F_Title = item.F_Title,
                    F_Description = item.F_Description,
                    F_CreateTime = item.F_CreateTime,
                    F_UpdateTime = item.F_UpdateTime,
                    F_Id = item.F_Id,
                    F_AudioUrl = item.F_AudioUrl,
                    F_CoverImg = item.F_CoverImg,
                    F_Type = 3
                });
            }
        }
    }
}
