namespace ChatService.API.Services.Abstractions.MongoDb
{
    public interface IMongoDbService<T>
    {
        public Task CreateAsync(T entity);
        public Task CreateRangeAsync(ICollection<T> entity);
        public Task<List<T>> GetAsync();
        public Task<List<T>> GetAsync(string id);
    }
}
