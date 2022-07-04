using FileService.API.Services.Abstractions.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileService.API.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IStorageService _storageService;
        public FilesController(IWebHostEnvironment environment, IStorageService storageService)
        {
            _environment = environment;
            _storageService = storageService;
        }
        [HttpPost("upload-images")] 
        public async Task<IActionResult> UploadImages()
        {
            var datas = await _storageService.UploadAsync("files", Request.Form.Files);
            return Ok(datas);
        }
        [HttpGet]
        public IActionResult GetFiles([FromQuery] string postid)
        {
            return Ok(postid);
        }
    }
}
