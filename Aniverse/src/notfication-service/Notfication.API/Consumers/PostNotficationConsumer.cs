using Aniverse.MessageContracts.Commands;
using Aniverse.MessageContracts.Models;
using MassTransit;
using Notfication.API.Service;

namespace Notfication.API.Consumers
{
    public class PostNotficationConsumer : IConsumer<IPostCommand>
    {
        private readonly IMongoDbService _service;
        public PostNotficationConsumer(IMongoDbService service)
        {
            _service = service;
        }
        public async Task Consume(ConsumeContext<IPostCommand> context)
        {
            var notfication = new DataAccessLayer.Entities.Notfication()
            {
                Content = context.Message.Content,
            };
            await _service.CreateAsync(notfication);
            var data = context.Message;
        }
    }
}
