namespace SouthWall
{
    public interface IAudiosService
    {
        Task<List<AudiosEntity>> GetList(AudiosEntity query);
        Task<AudiosEntity?> GetById(string id);
        Task<int> Save(AudiosEntity entity);
        Task Delete(string id);
    }
    public class AudiosService : ServiceBase, IAudiosService
    {
        public AudiosService(IAudiosDBAccess audiosDBAccess):base(audiosDBAccess) { }
        public Task<List<AudiosEntity>> GetList(AudiosEntity query)
        {
            return _AudiosDBAccess.GetList(query);
        }
        public Task<AudiosEntity?> GetById(string id)
        {
            return _AudiosDBAccess.GetById(id);
        }
        public async Task<int> Save(AudiosEntity entity)
        {
            return await _AudiosDBAccess.Save(entity);
        }
        public Task Delete(string id)
        {
            return _AudiosDBAccess.Delete(id);
        }
    }
}
