using FileService.API.DTO_s.Upload;

namespace FileService.API.Services.Abstractions.Storage
{
    public interface IStorage
    {
        Task<List<UploadResponse>> UploadAsync(string containerName, IFormFileCollection files, string username = "");
        Task DeleteAsync(string containerName, string file);
        List<string> GetFiles(string containerName);
        bool HasFile(string containerName, string fileName);
    }
}
