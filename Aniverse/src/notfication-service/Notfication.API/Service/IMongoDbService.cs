namespace Notfication.API.Service
{
    public interface IMongoDbService
    {
        public Task CreateAsync(DataAccessLayer.Entities.Notfication notfication);
        public Task<List<DataAccessLayer.Entities.Notfication>> GetAsync();
    }
}
