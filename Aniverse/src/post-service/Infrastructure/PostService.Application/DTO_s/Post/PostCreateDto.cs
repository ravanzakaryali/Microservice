using Microsoft.AspNetCore.Http;

namespace PostService.Application.DTO_s.Post
{
    public class PostCreateDto
    {
        public string UserId { get; set; }
        public string Content { get; set; }
        public ICollection<IFormFile> Files { get; set; }
    }
}
