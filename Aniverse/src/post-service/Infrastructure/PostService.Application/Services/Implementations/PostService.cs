using AutoMapper;
using PostService.Application.DTO_s.Post;
using PostService.Application.Services.Interfaces;
using PostService.Inerfaces;

namespace PostService.Application.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetPostDto> GetAsync(string id)
        {
            return _mapper.Map<GetPostDto>(await _unitOfWork.PostRepository.GetAsync(p => p.Id == Guid.Parse(id) && !p.IsDeleted));
        }

        public async Task<List<GetAllPostDto>> GetAllAsync(int page = 1, int size = 4)
        {
            return _mapper.Map<List<GetAllPostDto>>(await _unitOfWork.PostRepository.GetAllAsync(page, size, p => p.CreatedDate, p => p.IsDeleted == false, true));
        }
    }
}
