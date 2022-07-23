using ChatService.API.DataAccess.DB;
using ChatService.API.DataAccess.Entities;
using ChatService.API.Services.Abstractions.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChatService.API.Services.Implementations.MongoDb
{
    public class MongoDbService<T> : IMongoDbService<T>
    {
        private readonly IMongoCollection<T> _playlistCollection;
        public MongoDbService(IOptions<MongoDbSettings> mongoDBSettings)
        {
            MongoClient client = new(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playlistCollection = database.GetCollection<T>(mongoDBSettings.Value.CollectionName);
        }

        public async Task CreateAsync(T entity)
        {
            await _playlistCollection.InsertOneAsync(entity);
        }

        public async Task CreateRangeAsync(ICollection<T> entities)
        {
            await _playlistCollection.InsertManyAsync(entities);
        }

        public async Task<List<T>> GetAsync()
        {
            return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<T>> GetAsync(string id)
        {
            return await _playlistCollection.Find(id).ToListAsync();
        }
    }
}
