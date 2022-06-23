using PostService.Application.DTO_s.Post;
using PostService.Domain.Entities;

namespace PostService.Application.Services.Interfaces
{
    public interface IPostService
    {
        public Task<List<PostGetDto>> GetAll(int page, int size);
        public Task<PostGetDto> GetAsync(string id);
    }
}
