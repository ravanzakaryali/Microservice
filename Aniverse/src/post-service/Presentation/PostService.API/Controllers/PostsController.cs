using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.DTO_s.Common;
using PostService.Application.Exceptions.CommonExceptions;
using PostService.Application.Services;

namespace PostService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly UnitOfWorkService _service;
        public PostsController(UnitOfWorkService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll(QueryPaginate query)
        {
            try
            {
                return  Ok(await _service.PostService.GetAllAsync(query));
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
    }
}
