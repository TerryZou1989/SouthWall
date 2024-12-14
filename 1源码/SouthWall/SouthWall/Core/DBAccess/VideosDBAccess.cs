using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SouthWall
{
    public interface IVideosDBAccess
    {
        Task<List<VideosEntity>> GetList(VideosEntity query);
        Task<VideosEntity?> GetById(string id);
        Task<int> Save(VideosEntity entity);
        Task Delete(string id);
    }
    public class VideosDBAccess : DBAccessBase, IVideosDBAccess
    {
        public VideosDBAccess(SWDbContext context) : base(context) { }
        private Expression<Func<VideosEntity, bool>> GetExpression(VideosEntity query)
        {
            Expression<Func<VideosEntity, bool>> exp = t => true;
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
        public async Task<List<VideosEntity>> GetList(VideosEntity query)
        {
            var exp = GetExpression(query);
            return await _context.Videos.Where(exp).ToListAsync();
        }
        public async Task<VideosEntity?> GetById(string id)
        {
            return await _context.Videos.FirstOrDefaultAsync(t => t.F_Id == id);
        }
        public async Task<int> Save(VideosEntity entity)
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
                    obj.F_VideoUrl = entity.F_VideoUrl;
                    obj.F_VideoCode = entity.F_VideoCode;
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
