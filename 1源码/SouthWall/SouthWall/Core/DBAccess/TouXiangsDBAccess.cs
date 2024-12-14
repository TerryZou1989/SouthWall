using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SouthWall
{
    public interface ITouXiangsDBAccess
    {
        Task<List<TouXiangsEntity>> GetList(TouXiangsEntity query);
        Task<TouXiangsEntity?> GetById(string id);
        Task<int> Save(TouXiangsEntity entity);
        Task Delete(string id);
    }
    public class TouXiangsDBAccess : DBAccessBase, ITouXiangsDBAccess
    {
        public TouXiangsDBAccess(SWDbContext context) : base(context) { }
        private Expression<Func<TouXiangsEntity, bool>> GetExpression(TouXiangsEntity query)
        {
            Expression<Func<TouXiangsEntity, bool>> exp = t => true;
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
        public async Task<List<TouXiangsEntity>> GetList(TouXiangsEntity query)
        {
            var exp = GetExpression(query);
            return await _context.TouXiangs.Where(exp).ToListAsync();
        }
        public async Task<TouXiangsEntity?> GetById(string id)
        {
            return await _context.TouXiangs.FirstOrDefaultAsync(t => t.F_Id == id);
        }
        public async Task<int> Save(TouXiangsEntity entity)
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
                    obj.F_ImgSrc = entity.F_ImgSrc;
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
