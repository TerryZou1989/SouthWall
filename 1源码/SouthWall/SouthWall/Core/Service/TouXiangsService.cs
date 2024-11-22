namespace SouthWall
{
    public interface ITouXiangsService
    {
        Task<List<TouXiangsEntity>> GetList(TouXiangsEntity query);
        Task<TouXiangsEntity?> GetById(string id);        
        Task<int> Save(TouXiangsEntity entity);
        Task Delete(string id);
    }
    public class TouXiangsService : ServiceBase, ITouXiangsService
    {
        public TouXiangsService(ITouXiangsDBAccess TouXiangsDBAccess):base(TouXiangsDBAccess) { }
        public Task<List<TouXiangsEntity>> GetList(TouXiangsEntity query)
        {
            return _TouXiangsDBAccess.GetList(query);
        }
        public Task<TouXiangsEntity?> GetById(string id)
        {
            return _TouXiangsDBAccess.GetById(id);
        }
        public async Task<int> Save(TouXiangsEntity entity)
        {
            return await _TouXiangsDBAccess.Save(entity);
        }
        public Task Delete(string id)
        {
            return _TouXiangsDBAccess.Delete(id);
        }
    }
}
