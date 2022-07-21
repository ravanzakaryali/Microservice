using Aniverse.MessageContracts.Events.Notfication;
using MassTransit;
using Notfication.API.Service;

namespace Notfication.API.Consumers
{
    public class PostNotficationConsumer : IConsumer<NotficationCreatedEvent>
    {
        private readonly IMongoDbService _service;
        public PostNotficationConsumer(IMongoDbService service)
        {
            _service = service;
        }
        public async Task Consume(ConsumeContext<NotficationCreatedEvent> context)
        {
            var notfication = new DataAccessLayer.Entities.Notfication()
            {
                UserId = context.Message.UserId,
                Content = context.Message.Content,
                CreatedDate = DateTime.Now,
                Url = context.Message.Url,
            };
            await _service.CreateAsync(notfication);
        }
    }
}
