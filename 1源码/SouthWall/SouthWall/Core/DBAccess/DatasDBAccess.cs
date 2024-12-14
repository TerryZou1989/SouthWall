using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SouthWall
{
    public interface IDatasDBAccess
    {
        Task<List<DatasEntity>> GetList(DatasEntity query);
        Task<DatasEntity?> GetById(string id);
        Task<int> Save(DatasEntity entity);
        Task Delete(string id);
    }
    public class DatasDBAccess : DBAccessBase, IDatasDBAccess
    {
        public DatasDBAccess(SWDbContext context) : base(context) { }
        private Expression<Func<DatasEntity, bool>> GetExpression(DatasEntity query)
        {
            Expression<Func<DatasEntity, bool>> exp = t => true;
            if (query == null)
            {
                return exp;
            }
            if (!string.IsNullOrEmpty(query.F_Id))
            {
                exp = exp.And(t => t.F_Id == query.F_Id);
            }
            if (query.F_Type.HasValue)
            {
                exp=exp.And(t=>t.F_Type == query.F_Type);
            }
            return exp;
        }
        public async Task<List<DatasEntity>> GetList(DatasEntity query)
        {
            var exp = GetExpression(query);
            return await _context.Datas.Where(exp).ToListAsync();
        }
        public async Task<DatasEntity?> GetById(string id)
        {
            return await _context.Datas.FirstOrDefaultAsync(t => t.F_Id == id);
        }
        public async Task<int> Save(DatasEntity entity)
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
                    obj.F_Title=entity.F_Title;
                    obj.F_Description=entity.F_Description;
                    obj.F_Content=entity.F_Content;
                    obj.F_Url=entity.F_Url;
                    obj.F_CoverImg=entity.F_CoverImg;
                    obj.F_Imgs = entity.F_Imgs;
                    obj.F_VideoUrl=entity.F_VideoUrl;
                    obj.F_AudioUrl=entity.F_AudioUrl;
                    obj.F_IFrameCode=entity.F_IFrameCode;
                    obj.F_Type = entity.F_Type;
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
