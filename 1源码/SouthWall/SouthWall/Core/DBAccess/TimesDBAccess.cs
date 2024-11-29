using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SouthWall
{
    public interface ITimesDBAccess
    {
        Task<List<TimesEntity>> GetList(TimesEntity query);
        Task<TimesEntity?> GetById(string id);
        Task<int> Save(TimesEntity entity);
        Task Delete(string id);
    }
    public class TimesDBAccess : DBAccessBase<TimesEntity>, ITimesDBAccess
    {
        public TimesDBAccess(SWDbContext context) : base(context) { }
        private Expression<Func<TimesEntity, bool>> GetExpression(TimesEntity query)
        {
            Expression<Func<TimesEntity, bool>> exp = t => true;
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
        public async Task<List<TimesEntity>> GetList(TimesEntity query)
        {
            var exp = GetExpression(query);
            return await _context.Times.Where(exp).ToListAsync();
        }
        public async Task<TimesEntity?> GetById(string id)
        {
            return await _context.Times.FirstOrDefaultAsync(t => t.F_Id == id);
        }
        public async Task<int> Save(TimesEntity entity)
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
                    obj.F_Content = entity.F_Content;
                    obj.F_Imgs = entity.F_Imgs;
                    obj.F_Video = entity.F_Video;
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
