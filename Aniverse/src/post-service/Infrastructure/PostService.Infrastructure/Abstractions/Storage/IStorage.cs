using Microsoft.AspNetCore.Http;
using PostService.Application.DTO_s.Upload;

namespace PostService.Infrastructure.Abstractions.Storage
{
    public interface IStorage
    {
        Task<List<UploadResponse>> UploadAsync(string containerName, IFormFileCollection files, string username = "");
        Task DeleteAsync(string containerName, string file);
        List<string> GetFiles(string containerName);
        bool HasFile(string containerName, string fileName);
    }
}
