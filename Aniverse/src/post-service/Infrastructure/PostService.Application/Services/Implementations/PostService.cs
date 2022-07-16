using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using PostService.Application.DTO_s.Common;
using PostService.Application.DTO_s.Post;
using PostService.Application.Exceptions.CommonExceptions;
using PostService.Application.Extensions;
using PostService.Application.Services.Interfaces;
using PostService.Domain.Entities;
using PostService.Inerfaces;

namespace PostService.Application.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        readonly ISendEndpointProvider _sendEndpointProvider;


        public PostService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<GetPostDto> GetAsync(string postname)
        {
            Post post = await _unitOfWork.PostRepository.GetAsync(p => p.Id == Guid.Parse(postname) && !p.IsDeleted);
            if(post is null)
            {
                throw new NotFoundException("Post not found");
            }
            return _mapper.Map<GetPostDto>(post);
        }
        public async Task<List<GetAllPostDto>> GetAllAsync(QueryPaginate query)
        {
            return _mapper.Map<List<GetAllPostDto>>(await _unitOfWork.PostRepository.GetAllAsync(query.Page, query.Size, p => p.CreatedDate, p => p.IsDeleted == false, true));
        }
        public async Task<GetPostDto> Create(PostCreateDto createDto)
        {
            createDto.UserId = _accessor.HttpContext.User.GetUserId();

            return _mapper.Map<GetPostDto>(await _unitOfWork.PostRepository.AddAsync(_mapper.Map<Post>(createDto)));
        }
    }
}
