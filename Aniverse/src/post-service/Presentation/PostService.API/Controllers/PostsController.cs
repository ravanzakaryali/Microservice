using Aniverse.MessageContracts;
using Aniverse.MessageContracts.Commands;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.DTO_s.Common;
using PostService.Application.DTO_s.Post;
using PostService.Application.Exceptions.CommonExceptions;
using PostService.Application.Services;

namespace PostService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        readonly IUnitOfWorkService _service;
        readonly IBus _bus;
        public PostsController(IUnitOfWorkService service, IBus bus)
        {
            _service = service;
            _bus = bus;
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
        [HttpGet("{postname}")]
        public async Task<ActionResult> GetAsync(string postname)
        {
            try
            {
                return Ok(await _service.PostService.GetAsync(postname));
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
        [HttpPost]
        public async Task<ActionResult> Create(PostCreateDto post)
        {
            try
            {
                Uri uri = new($"{ RabbitMqConstants.URI }/{RabbitMqConstants.NotificationServiceQueue}");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send<IPostCommand>(post);
                return Ok(await _service.PostService.Create(post));
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
