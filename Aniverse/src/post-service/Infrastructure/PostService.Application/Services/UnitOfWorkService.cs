using AutoMapper;
using PostService.Application.Services.Interfaces;
using PostService.Inerfaces;

namespace PostService.Application.Services
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UnitOfWorkService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IPostService _postService;
        public IPostService PostService => _postService ??= new Implementations.PostService(_unitOfWork,_mapper);
    }
}
