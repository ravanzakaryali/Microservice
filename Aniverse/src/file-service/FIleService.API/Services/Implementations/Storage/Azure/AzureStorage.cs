using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileService.API.DTO_s.Upload;
using FileService.API.Services.Abstractions.Storage.Azure;

namespace FileService.API.Services.Implementations.Storage.Azure
{
    public class AzureStorage : IAzureStorage
    {
        readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClinet;
        public AzureStorage(IConfiguration config)
        {
            _blobServiceClient = new BlobServiceClient(config["Storage:Azure"]);
        }
        public Task DeleteAsync(string containerName, string file)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFiles(string containerName)
        {
            throw new NotImplementedException();
        }

        public bool HasFile(string containerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UploadResponse>> UploadAsync(string containerName, IFormFileCollection files)
        {
            _blobContainerClinet = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClinet.CreateIfNotExistsAsync();
            await _blobContainerClinet.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<UploadResponse> response = new();
            foreach (var file in files)
            {
                BlobClient blobClient =  _blobContainerClinet.GetBlobClient(file.Name);
                await blobClient.UploadAsync(file.OpenReadStream());
                response.Add(new UploadResponse
                {
                     ContainerName = containerName,
                     FileName = file.Name,  
                });
            }
            return response;
        }
    }
}
