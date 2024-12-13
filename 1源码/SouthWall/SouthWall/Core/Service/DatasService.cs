namespace SouthWall
{
    public interface IDatasService
    {
        Task<List<DatasEntity>> GetList(DatasEntity query);
        Task<DatasEntity?> GetById(string id);
        Task<int> Save(DatasEntity entity);
        Task Delete(string id);
    }
    public class DatasService : ServiceBase, IDatasService
    {
        public DatasService(IDatasDBAccess datasDBAccess):base(datasDBAccess) { }        
        public Task<List<DatasEntity>> GetList(DatasEntity query)
        {
            return _DatasDBAccess.GetList(query);
        }
        public Task<DatasEntity?> GetById(string id)
        {
            return _DatasDBAccess.GetById(id);
        }
        public async Task<int> Save(DatasEntity entity)
        {
            return await _DatasDBAccess.Save(entity);
        }
        public Task Delete(string id)
        {
            return _DatasDBAccess.Delete(id);
        }
    }
}
