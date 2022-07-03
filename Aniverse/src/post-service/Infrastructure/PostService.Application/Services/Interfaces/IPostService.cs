using PostService.Application.DTO_s.Common;
using PostService.Application.DTO_s.Post;

namespace PostService.Application.Services.Interfaces
{
    public interface IPostService
    {
        public Task<List<GetAllPostDto>> GetAllAsync(QueryPaginate query);
        public Task<GetPostDto> GetAsync(string postname);
        public Task<GetPostDto> Create(PostCreateDto createDto);
    }
}
