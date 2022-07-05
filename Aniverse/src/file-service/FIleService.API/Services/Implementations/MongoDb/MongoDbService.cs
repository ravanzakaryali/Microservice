using FileService.API.DataAccess.DB;
using FileService.API.DataAccess.Entities;
using FileService.API.Services.Abstractions.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileService.API.Services.Implementations.MongoDb
{
    public class MongoDbService : IMongoDbService
    {

        private readonly IMongoCollection<DbFile> _playlistCollection;
        public MongoDbService(IOptions<MongoDbSettings> mongoDBSettings)
        {
            MongoClient client = new(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playlistCollection = database.GetCollection<DbFile>(mongoDBSettings.Value.CollectionName);
        }
        public async Task CreateAsync(DbFile file)
        {
            await _playlistCollection.InsertOneAsync(file);
        }
        public async Task CreateRangeAsync(ICollection<DbFile> files)
        {
            await _playlistCollection.InsertManyAsync(files);
        }
        public async Task<List<DbFile>> GetAsync()
        {
            return await _playlistCollection.Find(new BsonDocument()).ToListAsync();

        }
    }
}
