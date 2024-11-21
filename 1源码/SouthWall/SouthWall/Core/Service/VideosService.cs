namespace SouthWall
{
    public interface IVideosService
    {
        Task<List<VideosEntity>> GetList(VideosEntity query);
        Task<VideosEntity?> GetById(string id);
        Task<int> Save(VideosEntity entity);
        Task Delete(string id);
    }
    public class VideosService : ServiceBase, IVideosService
    {
        public VideosService(IVideosDBAccess videosDBAccess):base(videosDBAccess) { }
        public Task<List<VideosEntity>> GetList(VideosEntity query)
        {
            return _VideosDBAccess.GetList(query);
        }
        public Task<VideosEntity?> GetById(string id)
        {
            return _VideosDBAccess.GetById(id);
        }
        public async Task<int> Save(VideosEntity entity)
        {
            return await _VideosDBAccess.Save(entity);
        }
        public Task Delete(string id)
        {
            return _VideosDBAccess.Delete(id);
        }
    }
}
