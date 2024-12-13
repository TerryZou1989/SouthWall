using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SouthWall
{
    public interface IRequestLogsDBAccess
    {
        Task<List<RequestLogsEntity>> GetList(RequestLogsEntity query);
        Task<RequestLogsEntity?> GetById(string id);
        Task<int> Save(RequestLogsEntity entity);
        Task Delete(string id);
    }
    public class RequestLogsDBAccess : DBAccessBase<RequestLogsEntity>, IRequestLogsDBAccess
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

    }
}
