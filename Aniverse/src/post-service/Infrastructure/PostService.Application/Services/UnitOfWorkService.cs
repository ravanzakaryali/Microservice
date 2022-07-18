using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using PostService.Application.Interfaces.Storage;
using PostService.Application.Services.Interfaces;
using PostService.Inerfaces;

namespace PostService.Application.Services
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        readonly IHttpContextAccessor _accessor;
        readonly ISendEndpointProvider _sendEndpointProvider;
        readonly IStorageService _storageService;

        public UnitOfWorkService(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IHttpContextAccessor accessor, 
            ISendEndpointProvider sendEndpointProvider,
            IStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
            _sendEndpointProvider = sendEndpointProvider;
            _storageService = storageService;
        }

        private IPostService _postService;
        public IPostService PostService => _postService ??= new Implementations.PostService(
                _unitOfWork, 
                _mapper, 
                _accessor, 
                _sendEndpointProvider,
                _storageService);
    }
}
