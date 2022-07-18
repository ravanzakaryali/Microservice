using Microsoft.AspNetCore.Http;

namespace PostService.Application.DTO_s.Post
{
    public class PostCreateDto
    {
        public string Content { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
