namespace SouthWall
{
    public interface IIPInfosService
    {
        Task<List<IPInfosEntity>> GetList(IPInfosEntity query);
        Task<IPInfosEntity?> GetById(string id);
        Task<IPInfosEntity?> GetByIP(string ip);
        Task<int> Save(IPInfosEntity entity);
        Task Delete(string id);
    }
    public class IPInfosService : ServiceBase, IIPInfosService
    {
        public IPInfosService(IIPInfosDBAccess IPInfosDBAccess):base(IPInfosDBAccess) { }
        public Task<List<IPInfosEntity>> GetList(IPInfosEntity query)
        {
            return _IPInfosDBAccess.GetList(query);
        }
        public Task<IPInfosEntity?> GetById(string id)
        {
            return _IPInfosDBAccess.GetById(id);
        }
        public Task<IPInfosEntity?> GetByIP(string ip)
        {
            return _IPInfosDBAccess.GetByIP(ip);
        }
        public async Task<int> Save(IPInfosEntity entity)
        {
            return await _IPInfosDBAccess.Save(entity);
        }
        public Task Delete(string id)
        {
            return _IPInfosDBAccess.Delete(id);
        }
    }
}
