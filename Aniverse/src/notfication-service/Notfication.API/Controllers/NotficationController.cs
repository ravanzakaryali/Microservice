using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notfication.API.Service;


namespace Notfication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotficationController : ControllerBase
    {
        private readonly IMongoDbService _service;
        private readonly IHttpContextAccessor _context;
        public NotficationController(IMongoDbService service, IHttpContextAccessor context)
        {
            _service = service;
            _context = context;
        }


        [HttpGet]
        public async Task<List<DataAccessLayer.Entities.Notfication>> Get()
        {
            return await _service.GetAsync();
        }
        [HttpPost]  
        public async Task<IActionResult> Post(DataAccessLayer.Entities.Notfication nofication)
        {

            await _service.CreateAsync(nofication);
            return CreatedAtAction(nameof(Get), new { id = nofication.Id }, nofication);
        }
    }
}
