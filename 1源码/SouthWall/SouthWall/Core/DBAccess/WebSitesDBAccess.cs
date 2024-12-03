using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SouthWall
{
    public interface IWebSitesDBAccess
    {
        Task<List<WebSitesEntity>> GetList(WebSitesEntity query);
        Task<WebSitesEntity?> GetById(string id);
        Task<int> Save(WebSitesEntity entity);
        Task Delete(string id);
    }
    public class WebSitesDBAccess : DBAccessBase<WebSitesEntity>, IWebSitesDBAccess
    {
        public WebSitesDBAccess(SWDbContext context) : base(context) { }
        private Expression<Func<WebSitesEntity, bool>> GetExpression(WebSitesEntity query)
        {
            Expression<Func<WebSitesEntity, bool>> exp = t => true;
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
        public async Task<List<WebSitesEntity>> GetList(WebSitesEntity query)
        {
            var exp = GetExpression(query);
            return await _context.WebSites.Where(exp).ToListAsync();
        }
        public async Task<WebSitesEntity?> GetById(string id)
        {
            return await _context.WebSites.FirstOrDefaultAsync(t => t.F_Id == id);
        }
        public async Task<int> Save(WebSitesEntity entity)
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
                    obj.F_CoverImg = entity.F_CoverImg;
                    obj.F_Title = entity.F_Title;
                    obj.F_Description = entity.F_Description;
                    obj.F_Url = entity.F_Url;
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
