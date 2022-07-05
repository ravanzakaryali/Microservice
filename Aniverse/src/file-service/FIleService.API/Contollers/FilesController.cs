using FileService.API.DataAccess.Entities;
using FileService.API.Services.Abstractions.MongoDb;
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
        private readonly IMongoDbService _dbService;
        private readonly IConfiguration _configuration;
        public FilesController(IWebHostEnvironment environment, IStorageService storageService, IMongoDbService dbService, IConfiguration configuration)
        {
            _environment = environment;
            _storageService = storageService;
            _dbService = dbService;
            _configuration = configuration;
        }
        [HttpGet("upload-images")]
        public async Task<IActionResult> UploadImages()
        {
            //List<DbFile> files = new();
            //foreach (var file in Request.Form.Files)
            //{
            //    DbFile fileDb = new()
            //    {
            //        UserId = "2121",
            //        DataType = file.ContentType,
            //        Extension = Path.GetExtension(file.FileName),
            //        Name = file.FileName,
            //        StorageUrl = _configuration["Storage:Azure"],
            //    };
            //    files.Add(fileDb);
            //}
            //await _dbService.CreateRangeAsync(files);
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
