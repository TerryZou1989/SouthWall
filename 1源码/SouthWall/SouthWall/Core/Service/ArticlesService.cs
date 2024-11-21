namespace SouthWall
{
    public interface IArticlesService
    {
        Task<List<ArticlesEntity>> GetList(ArticlesEntity query);
        Task<ArticlesEntity?> GetById(string id);
        Task<int> Save(ArticlesEntity entity);
        Task Delete(string id);
    }
    public class ArticlesService : ServiceBase, IArticlesService
    {
        public ArticlesService(IArticlesDBAccess ArticlesDBAccess):base(ArticlesDBAccess) { }
        public Task<List<ArticlesEntity>> GetList(ArticlesEntity query)
        {
            return _ArticlesDBAccess.GetList(query);
        }
        public Task<ArticlesEntity?> GetById(string id)
        {
            return _ArticlesDBAccess.GetById(id);
        }
        public async Task<int> Save(ArticlesEntity entity)
        {
            return await _ArticlesDBAccess.Save(entity);
        }
        public Task Delete(string id)
        {
            return _ArticlesDBAccess.Delete(id);
        }
    }
}
