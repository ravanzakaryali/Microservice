using AutoMapper;
using Microsoft.AspNetCore.Http;
using PostService.Application.Services.Interfaces;
using PostService.Inerfaces;

namespace PostService.Application.Services
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _accessor;

        public UnitOfWorkService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }

        private IPostService _postService;
        public IPostService PostService => _postService ??= new Implementations.PostService(_unitOfWork, _mapper, _accessor);
    }
}
