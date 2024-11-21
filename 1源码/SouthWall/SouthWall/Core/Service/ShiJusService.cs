namespace SouthWall
{
    public interface IShiJusService
    {
        Task<List<ShiJusEntity>> GetList(ShiJusEntity query);
        Task<ShiJusEntity?> GetById(string id);
        Task<ShiJusEntity?> GetByRandom();
        Task<int> Save(ShiJusEntity entity);
        Task Delete(string id);
    }
    public class ShiJusService : ServiceBase, IShiJusService
    {
        public ShiJusService(IShiJusDBAccess shiJusDBAccess):base(shiJusDBAccess) { }
        public Task<List<ShiJusEntity>> GetList(ShiJusEntity query)
        {
            return _ShiJusDBAccess.GetList(query);
        }
        public Task<ShiJusEntity?> GetById(string id)
        {
            return _ShiJusDBAccess.GetById(id);
        }
        public async Task<int> Save(ShiJusEntity entity)
        {
            return await _ShiJusDBAccess.Save(entity);
        }
        public Task Delete(string id)
        {
            return _ShiJusDBAccess.Delete(id);
        }

        public async Task<ShiJusEntity?> GetByRandom()
        {
            var list =await this.GetList(null);
            int randomInt = Com.RandomInt(list.Count);
            return list[randomInt];
        }
    }
}
