﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SouthWall
{
    public interface IArticlesDBAccess
    {
        Task<List<ArticlesEntity>> GetList(ArticlesEntity query);
        Task<ArticlesEntity?> GetById(string id);
        Task<int> Save(ArticlesEntity entity);
        Task Delete(string id);
    }
    public class ArticlesDBAccess : DBAccessBase, IArticlesDBAccess
    {
        public ArticlesDBAccess(SWDbContext context) : base(context) { }
        private Expression<Func<ArticlesEntity, bool>> GetExpression(ArticlesEntity query)
        {
            Expression<Func<ArticlesEntity, bool>> exp = t => true;
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
        public async Task<List<ArticlesEntity>> GetList(ArticlesEntity query)
        {
            var exp = GetExpression(query);
            return await _context.Articles.Where(exp).ToListAsync();
        }
        public async Task<ArticlesEntity?> GetById(string id)
        {
            return await _context.Articles.FirstOrDefaultAsync(t => t.F_Id == id);
        }
        public async Task<int> Save(ArticlesEntity entity)
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
                    obj.F_Content = entity.F_Content;
                    obj.F_ArticleUrl = entity.F_ArticleUrl;
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
