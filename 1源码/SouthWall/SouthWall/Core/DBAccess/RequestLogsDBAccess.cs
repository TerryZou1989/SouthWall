using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;
using System.Linq.Expressions;

namespace SouthWall
{
    public interface IRequestLogsDBAccess
    {
        Task<List<RequestLogsEntity>> GetList(RequestLogsEntity query);
        Task<RequestLogsEntity?> GetById(string id);
        Task<int> Save(RequestLogsEntity entity);
        Task Delete(string id);
        Task<List<RequestAndIPCountEntity>> StatisticalRequestAndIPCount(DateTime start, DateTime end);
        Task<List<IPRequestCountEntity>> StatisticalIPRequestCount(DateTime date);
        Task<List<CountryRequestCountEntity>> StatisticalCountryRequestCount();
        Task<List<ProvinceRequestCountEntity>> StatisticalProvicneRequestCount(string country);
    }
    public class RequestLogsDBAccess : DBAccessBase, IRequestLogsDBAccess
    {
        public RequestLogsDBAccess(SWDbContext context) : base(context) { }
        private Expression<Func<RequestLogsEntity, bool>> GetExpression(RequestLogsEntity query)
        {
            Expression<Func<RequestLogsEntity, bool>> exp = t => true;
            if (query == null)
            {
                return exp;
            }
            if (!string.IsNullOrEmpty(query.F_Id))
            {
                exp = exp.And(t => t.F_Id == query.F_Id);
            }
            if (!string.IsNullOrEmpty(query.F_DataId))
            {
                exp = exp.And(t => t.F_DataId == query.F_DataId);
            }
            if (!string.IsNullOrEmpty(query.F_Country))
            {
                exp = exp.And(t => t.F_Country == query.F_Country);
            }
            if (!string.IsNullOrEmpty(query.F_Province))
            {
                exp = exp.And(t => t.F_Province == query.F_Province);
            }
            if (!string.IsNullOrEmpty(query.F_City))
            {
                exp = exp.And(t => t.F_City == query.F_City);
            }
            if (!string.IsNullOrEmpty(query.F_Module))
            {
                exp = exp.And(t => t.F_Module == query.F_Module);
            }
            return exp;
        }
        public async Task<List<RequestLogsEntity>> GetList(RequestLogsEntity query)
        {
            var exp = GetExpression(query);
            return await _context.RequestLogs.Where(exp).ToListAsync();
        }
        public async Task<RequestLogsEntity?> GetById(string id)
        {
            return await _context.RequestLogs.FirstOrDefaultAsync(t => t.F_Id == id);
        }
        public async Task<int> Save(RequestLogsEntity entity)
        {
            entity.InitCreate();
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task Delete(string id)
        {
            var obj = await GetById(id);
            if (obj != null)
            {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }
        public Task<List<RequestAndIPCountEntity>> StatisticalRequestAndIPCount(DateTime start, DateTime end)
        {
            int partitionKeyStart = start.ToString("yyyyMMdd").ToInt32();
            int partitionKeyEnd = end.ToString("yyyyMMdd").ToInt32();
            string sql = @"SELECT F_PartitionKey,count(1) F_RequestCount,count(DISTINCT F_IP) F_IPCount 
                           FROM `t_requestlogs`
                           WHERE F_PartitionKey>=@startkey and F_PartitionKey<=@endkey                           
                           group by F_PartitionKey
                           ORDER BY F_PartitionKey asc;";
            return _context.Database.SqlQueryRaw<RequestAndIPCountEntity>(sql,
                new MySqlParameter("@startkey", partitionKeyStart), new MySqlParameter("@endkey", partitionKeyEnd)).ToListAsync();
        }
        public Task<List<IPRequestCountEntity>> StatisticalIPRequestCount(DateTime date)
        {
            int pkey = date.ToString("yyyyMMdd").ToInt32();
            string sql = @"SELECT F_IP,F_Country,F_Province,F_City,count(1) F_RequestCount 
                           FROM `t_requestlogs`
                           where F_PartitionKey=@pkey
                           group by F_IP,F_Country,F_Province,F_City
                           order by F_RequestCount desc;";
            return _context.Database.SqlQueryRaw<IPRequestCountEntity>(sql,
                new MySqlParameter("@pkey", pkey)).ToListAsync();
        }

        public Task<List<CountryRequestCountEntity>> StatisticalCountryRequestCount()
        {
            int pkey = DateTime.Now.ToString("yyyyMMdd").ToInt32();
            string sql = @"SELECT t.F_Country,t.F_RequestCount F_RequestTotal,IFNULL(s.F_RequestCount,0) F_DailyRequestCount 
                           from 
                           (SELECT F_Country,count(1) F_RequestCount
                           FROM `t_requestlogs`
                           group by F_Country) t
                           left join 
                           (SELECT F_Country,count(1) F_RequestCount
                           FROM `t_requestlogs`
                           where F_PartitionKey=@pkey
                           group by F_Country) s 
                           on t.F_Country=s.F_Country
                           ORDER BY F_RequestTotal desc;";
            return _context.Database.SqlQueryRaw<CountryRequestCountEntity>(sql,
                new MySqlParameter("@pkey", pkey)).ToListAsync();
        }

        public Task<List<ProvinceRequestCountEntity>> StatisticalProvicneRequestCount(string country)
        {
            int pkey = DateTime.Now.ToString("yyyyMMdd").ToInt32();
            string sql = @"SELECT t.F_Province,t.F_RequestCount F_RequestTotal,IFNULL(s.F_RequestCount,0) F_DailyRequestCount 
                           from 
                           (SELECT F_Province,count(1) F_RequestCount
                           FROM `t_requestlogs`
                           where F_Country=@country
                           group by F_Province) t
                           left join 
                           (SELECT F_Province,count(1) F_RequestCount
                           FROM `t_requestlogs`
                           where F_PartitionKey=@pkey
                           and F_Country=@country
                           group by F_Province) s 
                           on t.F_Province=s.F_Province
                           ORDER BY F_RequestTotal desc;";
            return _context.Database.SqlQueryRaw<ProvinceRequestCountEntity>(sql,
                new MySqlParameter("@pkey", pkey), new MySqlParameter("@country", country)
                ).ToListAsync();
        }
    }
}
