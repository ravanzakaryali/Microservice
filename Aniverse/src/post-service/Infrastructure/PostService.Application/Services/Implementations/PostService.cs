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

        public Task<PostGetDto> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PostGetDto>> GetAll(int page = 1, int size = 4)
        {
            return _mapper.Map<List<PostGetDto>>(await _unitOfWork.PostRepository.GetAllAsync(page, size, p => p.CreatedDate, p => p.IsDeleted == false, true));
        }
    }
}
