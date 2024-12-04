using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SouthWall
{
    public interface IAudiosDBAccess
    {
        Task<List<AudiosEntity>> GetList(AudiosEntity query);
        Task<AudiosEntity?> GetById(string id);
        Task<int> Save(AudiosEntity entity);
        Task Delete(string id);
    }
    public class AudiosDBAccess : DBAccessBase<AudiosEntity>, IAudiosDBAccess
    {
        public AudiosDBAccess(SWDbContext context) : base(context) { }
        private Expression<Func<AudiosEntity, bool>> GetExpression(AudiosEntity query)
        {
            Expression<Func<AudiosEntity, bool>> exp = t => true;
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
        public async Task<List<AudiosEntity>> GetList(AudiosEntity query)
        {
            var exp = GetExpression(query);
            return await _context.Audios.Where(exp).ToListAsync();
        }
        public async Task<AudiosEntity?> GetById(string id)
        {
            return await _context.Audios.FirstOrDefaultAsync(t => t.F_Id == id);
        }
        public async Task<int> Save(AudiosEntity entity)
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
                    obj.F_AudioUrl = entity.F_AudioUrl;
                    obj.F_Description = entity.F_Description;
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
