using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace SouthWall
{
    public interface IMessagesService
    {
        Task<List<MessagesEntity>> GetList(MessagesEntity query);
        Task<MessagesEntity?> GetById(string id);
        Task<int> Save(MessagesEntity entity);
        Task Delete(string id);
        Task Reply(string id, string content);
    }
    public class MessagesService : ServiceBase, IMessagesService
    {
        public MessagesService(IMessagesDBAccess messagesDBAccess) : base(messagesDBAccess) { }
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
            await Com.SendMessageToEmail(entity.F_UserName, entity.F_Content, Config.AdminEmail);
            if (!string.IsNullOrEmpty(entity.F_UserEmail))
            {
                await Com.AutoReplyMessageToEmail(entity.F_UserName, entity.F_Content, entity.F_UserEmail);
            }
            return await _MessagesDBAccess.Save(entity);
        }
        public Task Delete(string id)
        {
            return _MessagesDBAccess.Delete(id);
        }

        public async Task Reply(string id, string content)
        {
            var message = await GetById(id);
            if (!string.IsNullOrEmpty(message.F_UserEmail))
            {
                await Com.ReplyMessageToEmail(message.F_UserName, content, message.F_Content, message.F_UserEmail);
            }
        }
    }
}
