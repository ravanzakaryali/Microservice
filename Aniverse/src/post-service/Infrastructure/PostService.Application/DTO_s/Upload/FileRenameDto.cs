using Microsoft.AspNetCore.Http;

namespace PostService.Application.DTO_s.Upload
{
    public class FileRenameDto
    {
        public string ContainerName { get; set; }
        public string Username { get; set; }
        public IFormFile File { get; set; }
    }
}
