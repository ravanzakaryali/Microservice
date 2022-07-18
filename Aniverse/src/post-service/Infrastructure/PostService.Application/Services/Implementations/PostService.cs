using Aniverse.MessageContracts;
using Aniverse.MessageContracts.Events.Post;
using Aniverse.MessageContracts.Models;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using PostService.Application.DTO_s.Common;
using PostService.Application.DTO_s.Post;
using PostService.Application.DTO_s.Upload;
using PostService.Application.Exceptions.CommonExceptions;
using PostService.Application.Extensions;
using PostService.Application.Interfaces.Storage;
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
        readonly IStorageService _storageService;

        public PostService(
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
        public async Task<GetPostDto> GetAsync(string postname)
        {
            Post post = await _unitOfWork.PostRepository.GetAsync(p => p.Id == Guid.Parse(postname) && !p.IsDeleted);
            if (post is null)
            {
                throw new NotFoundException("Post not found");
            }
            return _mapper.Map<GetPostDto>(post);
        }
        public async Task<List<GetAllPostDto>> GetAllAsync(QueryPaginate query)
        {
            return _mapper.Map<List<GetAllPostDto>>(await _unitOfWork.PostRepository.GetAllAsync(query.Page, query.Size, p => p.CreatedDate, p => p.IsDeleted == false, true));
        }

        public IMapper Get_mapper()
        {
            return _mapper;
        }

        public async Task<GetPostDto> Create(PostCreateDto createDto)
        {
            Post newPost = _mapper.Map<Post>(createDto);
            
            List<UploadResponse> responses = await _storageService.UploadAsync("files", createDto.Files, _accessor.HttpContext.User.GetLoggedInUserName());
            
            newPost.UserId = Guid.Parse(_accessor.HttpContext.User.GetUserId());

            Post postDb = await _unitOfWork.PostRepository.AddAsync(newPost);
            GetPostDto resultPost = _mapper.Map<GetPostDto>(postDb);

            List<Aniverse.MessageContracts.Models.File> filesName = new();
            foreach (var file in responses)
            {   
                Aniverse.MessageContracts.Models.File newFile = new()
                {
                    Name = file.FileName,
                    ContainerName = "files",
                    PostId = postDb.Id.ToString(),
                    UserId = resultPost.UserId,
                    Size = file.Size,
                };
                filesName.Add(newFile);
            }
            PostStartedEvent postStarted = new()
            {
                Content = newPost.Content,
                FilesName = filesName,
                PostId = postDb.Id.ToString(),
                UserId = newPost.UserId.ToString(),
            };
            ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new($"queue:{RabbitMqConstants.StateMachine}"));
            await sendEndpoint.Send<PostStartedEvent>(postStarted);
            return resultPost;
        }
    }
}
