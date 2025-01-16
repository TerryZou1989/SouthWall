using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SouthWall
{
    public interface IIPInfosDBAccess
    {
        Task<List<IPInfosEntity>> GetList(IPInfosEntity query);
        Task<IPInfosEntity?> GetById(string id);
        Task<IPInfosEntity?> GetByIP(string ip);
        Task<int> Save(IPInfosEntity entity);
        Task Delete(string id);
    }
    public class IPInfosDBAccess : DBAccessBase, IIPInfosDBAccess
    {
        public IPInfosDBAccess(SWDbContext context) : base(context) { }
        private Expression<Func<IPInfosEntity, bool>> GetExpression(IPInfosEntity query)
        {
            Expression<Func<IPInfosEntity, bool>> exp = t => true;
            if (query == null)
            {
                return exp;
            }
            if (!string.IsNullOrEmpty(query.F_Id))
            {
                exp = exp.And(t => t.F_Id == query.F_Id);
            }
            return exp;
        }
        public async Task<List<IPInfosEntity>> GetList(IPInfosEntity query)
        {
            var exp = GetExpression(query);
            return await _context.IPInfos.Where(exp).ToListAsync();
        }
        public async Task<IPInfosEntity?> GetById(string id)
        {
            return await _context.IPInfos.FirstOrDefaultAsync(t => t.F_Id == id);
        }
        public async Task<IPInfosEntity?> GetByIP(string ip)
        {
            return await _context.IPInfos.FirstOrDefaultAsync(t => t.F_IP == ip);
        }
        public async Task<int> Save(IPInfosEntity entity)
        {
            if (_context.IPInfos.Count(t => t.F_IP == entity.F_IP) == 0)
            {
                entity.InitCreate();
                _context.Add(entity);
                return await _context.SaveChangesAsync();
            }
            return 0;
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
