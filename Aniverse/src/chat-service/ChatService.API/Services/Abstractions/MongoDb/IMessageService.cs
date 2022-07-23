using ChatService.API.DataAccess.Entities;

namespace ChatService.API.Services.Abstractions.MongoDb
{
    public interface IMessageService : IMongoDbService<Message>
    {
    }
}
