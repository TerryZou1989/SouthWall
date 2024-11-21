using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SouthWall
{
    public interface IShiJusDBAccess
    {
        Task<List<ShiJusEntity>> GetList(ShiJusEntity query);
        Task<ShiJusEntity?> GetById(string id);
        Task<int> Save(ShiJusEntity entity);
        Task Delete(string id);
    }
    public class ShiJusDBAccess : DBAccessBase<ShiJusEntity>, IShiJusDBAccess
    {
        public ShiJusDBAccess(SWDbContext context) : base(context) { }
        private Expression<Func<ShiJusEntity, bool>> GetExpression(ShiJusEntity query)
        {
            Expression<Func<ShiJusEntity, bool>> exp = t => true;
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
        public async Task<List<ShiJusEntity>> GetList(ShiJusEntity query)
        {
            var exp = GetExpression(query);
            return await _context.ShiJus.Where(exp).ToListAsync();
        }
        public async Task<ShiJusEntity?> GetById(string id)
        {
            return await _context.ShiJus.FirstOrDefaultAsync(t => t.F_Id == id);
        }
        public async Task<int> Save(ShiJusEntity entity)
        {
            if (string.IsNullOrEmpty(entity.F_Id))
            {
                entity.InitCreate();
                _context.Add(entity);
            }
            else
            {
                var obj = await GetById(entity.F_Id);
                if (obj != null)
                {
                    obj.F_P = entity.F_P;
                    obj.F_N = entity.F_N;
                    obj.InitUpdate();
                    _context.Update(obj);
                }
                else
                {
                    entity.F_Id = null;
                    return await Save(entity);
                }
            }
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
