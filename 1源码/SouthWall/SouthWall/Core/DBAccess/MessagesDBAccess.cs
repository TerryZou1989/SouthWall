using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SouthWall
{
    public interface IMessagesDBAccess
    {
        Task<List<MessagesEntity>> GetList(MessagesEntity query);
        Task<MessagesEntity?> GetById(string id);
        Task<int> Save(MessagesEntity entity);
        Task Delete(string id);
    }
    public class MessagesDBAccess : DBAccessBase<MessagesEntity>, IMessagesDBAccess
    {
        public MessagesDBAccess(SWDbContext context) : base(context) { }
        private Expression<Func<MessagesEntity, bool>> GetExpression(MessagesEntity query)
        {
            Expression<Func<MessagesEntity, bool>> exp = t => true;
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
        public async Task<List<MessagesEntity>> GetList(MessagesEntity query)
        {
            var exp = GetExpression(query);
            return await _context.Messages.Where(exp).ToListAsync();
        }
        public async Task<MessagesEntity?> GetById(string id)
        {
            return await _context.Messages.FirstOrDefaultAsync(t => t.F_Id == id);
        }
        public async Task<int> Save(MessagesEntity entity)
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
                    obj.F_Content=entity.F_Content;
                    obj.F_UserName=entity.F_UserName;
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
