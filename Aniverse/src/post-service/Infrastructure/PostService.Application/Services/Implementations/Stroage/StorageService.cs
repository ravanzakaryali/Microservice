using Microsoft.AspNetCore.Http;
using PostService.Application.DTO_s.Upload;
using PostService.Application.Interfaces.Storage;

namespace PostService.Application.Implementations.Stroage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public Task DeleteAsync(string containerName, string file) =>
            _storage.DeleteAsync(containerName, file);

        public List<string> GetFiles(string containerName) =>
            _storage.GetFiles(containerName);
        public bool HasFile(string containerName, string fileName) =>
            _storage.HasFile(containerName, fileName);

        public Task<List<UploadResponse>> UploadAsync(string containerName, List<IFormFile> files, string
             username = "") =>
            _storage.UploadAsync(containerName, files, username);

    }
}
