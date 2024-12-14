using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Concurrent;

namespace SouthWall
{
    public interface IToolService
    {
        Task SyncData();
        Task AddDBPartition(DateTime end);
    }
    public class ToolService : ServiceBase, IToolService
    {
        public ToolService(
            IMDBAccess mDBAccess,
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
                mDBAccess,
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

        public async Task AddDBPartition(DateTime end)
        {
            int partitionkey = end.ToString("yyyyMMdd").ToInt32();
            List<string> tables = new List<string> { "t_requestlogs" };
            foreach (string table in tables) {
                var plist=await this._MDBAccess.QueryDBPartitions(table);
                var keys = plist.Select(t => { string key = t.Partition_Name; return key.Substring(1, 8).ToInt32(); }).ToList();
                var key = keys.Max().ToString();
                DateTime dt = Convert.ToDateTime(key.Substring(0, 4) + "-" + key.Substring(4, 2) + "-" + key.Substring(6, 2));
                while (dt <= end)
                {
                    string partitionName = "p" + dt.ToString("yyyyMMdd");
                    if (plist.Count(t => t.Partition_Name.ToUpper() == partitionName.ToUpper()) == 0)
                    {
                        string lessValue = dt.AddDays(1).ToString("yyyyMMdd");
                        await this._MDBAccess.AddDBPartition(table, partitionName, lessValue);
                    }
                    dt = dt.AddDays(1);
                }
            }
        }

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
