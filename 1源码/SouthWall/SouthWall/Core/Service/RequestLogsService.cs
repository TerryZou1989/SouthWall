namespace SouthWall
{
    public interface IRequestLogsService
    {
        Task<List<RequestLogsEntity>> GetList(RequestLogsEntity query);
        Task<RequestLogsEntity?> GetById(string id);
        Task<int> Save(RequestLogsEntity entity);
        Task Delete(string id);
    }
    public class RequestLogsService : ServiceBase, IRequestLogsService
    {
        public RequestLogsService(IRequestLogsDBAccess requestLogsDBAccess):base(requestLogsDBAccess) { }        
        public Task<List<RequestLogsEntity>> GetList(RequestLogsEntity query)
        {
            return _RequestLogsDBAccess.GetList(query);
        }
        public Task<RequestLogsEntity?> GetById(string id)
        {
            return _RequestLogsDBAccess.GetById(id);
        }
        public async Task<int> Save(RequestLogsEntity entity)
        {
            return await _RequestLogsDBAccess.Save(entity);
        }
        public Task Delete(string id)
        {
            return _RequestLogsDBAccess.Delete(id);
        }
    }
}
