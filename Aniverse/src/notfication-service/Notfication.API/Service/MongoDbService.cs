using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Notfication.API.DataAccessLayer.Entities;

namespace Notfication.API.Service
{
    public class MongoDbService
    {
        private readonly IMongoCollection<DataAccessLayer.Entities.Notfication> _playlistCollection;
        public MongoDbService(IOptions<MongoDbSettings> mongoDBSettings)
        {
            MongoClient client = new(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playlistCollection = database.GetCollection<DataAccessLayer.Entities.Notfication>(mongoDBSettings.Value.CollectionName);
        }
        public async Task CreateAsync(DataAccessLayer.Entities.Notfication notfication)
        {
            await _playlistCollection.InsertOneAsync(notfication);
            return;
        }
        public async Task<List<DataAccessLayer.Entities.Notfication>> GetAsync()
        {
            return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
        }
    }
}
