using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileService.API.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public FilesController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpPost("upload-images")] 
        public async Task<IActionResult> UploadImages()
        {
            string upload = Path.Combine(_environment.WebRootPath, "resource/product-images");
            if(!Directory.Exists(upload))
                Directory.CreateDirectory(upload);
            Random random = new();
            foreach (var file in Request.Form.Files)
            {
                string fullPath = Path.Combine(upload, $"{random.Next()}{Path.GetExtension(file.FileName)}");
                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            return Ok();
        }
    }
}
