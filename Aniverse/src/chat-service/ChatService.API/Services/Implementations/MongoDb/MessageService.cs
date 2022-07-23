using ChatService.API.DataAccess.DB;
using ChatService.API.DataAccess.Entities;
using ChatService.API.Services.Abstractions.MongoDb;
using Microsoft.Extensions.Options;

namespace ChatService.API.Services.Implementations.MongoDb
{
    public class MessageService : MongoDbService<Message>, IMessageService
    {
        public MessageService(IOptions<MongoDbSettings> mongoDBSettings) : base(mongoDBSettings)
        {
        }
    }
}
