﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileService.API.DTO_s.Upload;
using FileService.API.Services.Abstractions.Storage.Azure;

namespace FileService.API.Services.Implementations.Storage.Azure
{
    public class AzureStorage : Storage, IAzureStorage
    {
        readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClinet;
        public AzureStorage(IConfiguration config)
        {
            _blobServiceClient = new BlobServiceClient(config["Storage:Azure"]);
        }
        public async Task DeleteAsync(string containerName, string file)
        {
            _blobContainerClinet = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _blobContainerClinet.GetBlobClient(file);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            _blobContainerClinet = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClinet.GetBlobs().Select(b => b.Name).ToList();
        }

        public bool HasFile(string containerName, string fileName)
        {
            _blobContainerClinet = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClinet.GetBlobs().Any(b => b.Name == fileName);
        }

        public async Task<List<UploadResponse>> UploadAsync(string containerName, IFormFileCollection files, string username = "")
        {
            _blobContainerClinet = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClinet.CreateIfNotExistsAsync();
            await _blobContainerClinet.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<UploadResponse> response = new();
            foreach (var file in files)
            {
                FileRenameDto fileRenameDto = new()
                {
                    ContainerName = containerName,
                    Filename = file.Name,
                    Username = username,
                };
                string fileNewName = FileRename(fileRenameDto, HasFile);
                BlobClient blobClient = _blobContainerClinet.GetBlobClient(file.FileName);
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
