using FileService.API.DataAccess.Entities;

namespace FileService.API.Services.Abstractions.MongoDb
{
    public interface IMongoDbService
    {
        public Task CreateAsync(DbFile file);
        public Task CreateRangeAsync(ICollection<DbFile> file);
        public Task<List<DbFile>> GetAsync();

    }
}
