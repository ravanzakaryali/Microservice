using PostService.Application.DTO_s.Post;
using PostService.Domain.Entities;

namespace PostService.Application.Services.Interfaces
{
    public interface IPostService
    {
        public Task<List<GetAllPostDto>> GetAllAsync(int page, int size);
        public Task<GetPostDto> GetAsync(string id);
    }
}
