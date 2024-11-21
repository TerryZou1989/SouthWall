namespace SouthWall
{
    public interface ITimesService
    {
        Task<List<TimesEntity>> GetList(TimesEntity query);
        Task<TimesEntity?> GetById(string id);
        Task<int> Save(TimesEntity entity);
        Task Delete(string id);
    }
    public class TimesService : ServiceBase, ITimesService
    {
        public TimesService(ITimesDBAccess timesDBAccess):base(timesDBAccess) { }        
        public Task<List<TimesEntity>> GetList(TimesEntity query)
        {
            return _TimesDBAccess.GetList(query);
        }
        public Task<TimesEntity?> GetById(string id)
        {
            return _TimesDBAccess.GetById(id);
        }
        public async Task<int> Save(TimesEntity entity)
        {
            return await _TimesDBAccess.Save(entity);
        }
        public Task Delete(string id)
        {
            return _TimesDBAccess.Delete(id);
        }
    }
}
