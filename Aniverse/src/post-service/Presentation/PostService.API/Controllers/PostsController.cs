using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        readonly IPublishEndpoint _publishEndpoint;
        public PostsController(IUnitOfWorkService service, IPublishEndpoint publishEndpoint)
        {
            _service = service;
            _publishEndpoint = publishEndpoint;
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
                return StatusCode(StatusCodes.Status502BadGateway, new Response
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
                return StatusCode(StatusCodes.Status404NotFound, new Response
                {
                    Status = "Error",
                    Message = exception.Message
                });
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Response
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
                await _publishEndpoint.Publish<PostCreateDto>(new PostCreateDto
                {
                    Content = post.Content
                });
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
