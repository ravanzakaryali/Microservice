using Aniverse.MessageContracts.Commands;
using Aniverse.MessageContracts.Events.Post;
using FileService.API.DataAccess.Entities;
using FileService.API.Services.Abstractions.MongoDb;
using MassTransit;

namespace FileService.API.Consumers
{
    public class PostFileConsumer : IConsumer<PostCreatedEvent>
    {
        private readonly IMongoDbService _service;
        private readonly IConfiguration _configuration;

        public PostFileConsumer(IMongoDbService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<PostCreatedEvent> context)
        {
            List<DbFile> files = new();
            foreach (var fileItem in context.Message.FilesName)
            {
                DbFile newFileItem = new()
                {
                    CreatedDate = DateTime.Now,
                    DataType = fileItem.Type,
                    PostId = context.Message.PostId,
                    UserId = context.Message.UserId,
                    Name = fileItem.Name,
                    Extension = Path.GetExtension(fileItem.Name),
                    Size = fileItem.Size,
                    StorageUrl = "https://aniversefiles.blob.core.windows.net",
                };
                files.Add(newFileItem);
            }
            await _service.CreateRangeAsync(files);
        }
    }
}
