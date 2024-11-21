using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace SouthWall
{
    public interface IMessagesService
    {
        Task<List<MessagesEntity>> GetList(MessagesEntity query);
        Task<MessagesEntity?> GetById(string id);
        Task<int> Save(MessagesEntity entity);
        Task Delete(string id);
    }
    public class MessagesService : ServiceBase, IMessagesService
    {
        public MessagesService(IMessagesDBAccess messagesDBAccess):base(messagesDBAccess) { }
        public Task<List<MessagesEntity>> GetList(MessagesEntity query)
        {
            return _MessagesDBAccess.GetList(query);
        }
        public Task<MessagesEntity?> GetById(string id)
        {
            return _MessagesDBAccess.GetById(id);
        }
        public async Task<int> Save(MessagesEntity entity)
        {
            await Com.SendMessageToEmail(entity.F_UserName,entity.F_Content,Config.AdminEmail);
            return await _MessagesDBAccess.Save(entity);
        }
        public Task Delete(string id)
        {
            return _MessagesDBAccess.Delete(id);
        }
    }
}
