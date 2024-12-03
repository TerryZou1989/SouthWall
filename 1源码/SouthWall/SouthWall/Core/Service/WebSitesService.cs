namespace SouthWall
{
    public interface IWebSitesService
    {
        Task<List<WebSitesEntity>> GetList(WebSitesEntity query);
        Task<WebSitesEntity?> GetById(string id);
        Task<int> Save(WebSitesEntity entity);
        Task Delete(string id);
    }
    public class WebSitesService : ServiceBase, IWebSitesService
    {
        public WebSitesService(IWebSitesDBAccess WebSitesDBAccess):base(WebSitesDBAccess) { }
        public Task<List<WebSitesEntity>> GetList(WebSitesEntity query)
        {
            return _WebSitesDBAccess.GetList(query);
        }
        public Task<WebSitesEntity?> GetById(string id)
        {
            return _WebSitesDBAccess.GetById(id);
        }
        public async Task<int> Save(WebSitesEntity entity)
        {
            return await _WebSitesDBAccess.Save(entity);
        }
        public Task Delete(string id)
        {
            return _WebSitesDBAccess.Delete(id);
        }
    }
}
