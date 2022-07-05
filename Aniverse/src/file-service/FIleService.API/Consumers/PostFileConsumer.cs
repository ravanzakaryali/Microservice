using Aniverse.MessageContracts.Commands;
using FileService.API.DataAccess.Entities;
using FileService.API.Services.Abstractions.MongoDb;
using MassTransit;

namespace FileService.API.Consumers
{
    public class PostFileConsumer : IConsumer<IFileCommand>
    {
        private readonly IMongoDbService _service;
        private readonly IConfiguration _configuration;

        public PostFileConsumer(IMongoDbService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<IFileCommand> context)
        {
            DbFile file = new()
            {
                UserId = context.Message.UserId,
                PostId = context.Message.PostId,
                DataType = context.Message.Type,
                Extension = context.Message.Extension,
                Name = context.Message.FileName,
                Size = context.Message.Size,
                StorageUrl = _configuration["Storage:AzureURL"],
            };
            await _service.CreateAsync(file);
        }
    }
}
