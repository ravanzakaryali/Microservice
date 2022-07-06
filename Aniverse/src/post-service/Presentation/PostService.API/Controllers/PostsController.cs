using Aniverse.MessageContracts;
using Aniverse.MessageContracts.Commands;
using Aniverse.MessageContracts.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.DTO_s.Common;
using PostService.Application.DTO_s.Post;
using PostService.Application.Exceptions.CommonExceptions;
using PostService.Application.Services;
using PostService.Infrastructure.Abstractions.Storage;

namespace PostService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        readonly IUnitOfWorkService _service;
        readonly IBus _bus;
        readonly IStorageService _storageService;
        public PostsController(IUnitOfWorkService service, IBus bus, IStorageService storageService)
        {
            _service = service;
            _bus = bus;
            _storageService = storageService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllAsync([FromQuery] QueryPaginate query)
        {
            try
            {
                return Ok(await _service.PostService.GetAllAsync(query));
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Application.DTO_s.Common.Response
                {
                    Status = "Error",
                    Message = exception.Message
                });
            }
        }
        [HttpGet("{postid}")]
        public async Task<ActionResult> GetAsync(string postid)
        {
            try
            {
                return Ok(await _service.PostService.GetAsync(postid));
            }
            catch (NotFoundException exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Application.DTO_s.Common.Response
                {
                    Status = "Error",
                    Message = exception.Message
                });
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Application.DTO_s.Common.Response
                {
                    Status = "Error",
                    Message = exception.Message
                });
            }
        }
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] PostCreateDto post)
        {
            try
            {
                ISendEndpoint endPoint = await _bus.GetSendEndpoint(new Uri(RabbitMqConstants.SendNotfication));
                await endPoint.Send<IPostCommand>(post);
                var newPost = await _service.PostService.Create(post);

                foreach (var file in post.Files)
                {
                    DbFile data = new()
                    {
                        Extension = Path.GetExtension(file.FileName),
                        FileName = file.FileName,
                        PostId = newPost.Id,
                        Size = file.Length,
                        Type = file.ContentType
                    };
                    ISendEndpoint endpointFiles = await _bus.GetSendEndpoint(new Uri(RabbitMqConstants.SendFileService));
                    await endpointFiles.Send<IFileCommand>(data);
                }
                await _storageService.UploadAsync("files", post.Files,"revanzli");
                return Ok(newPost);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Application.DTO_s.Common.Response
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
    }
}
