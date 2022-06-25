using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public NotficationController(IMongoDbService service)
        {
            _service = service;
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
