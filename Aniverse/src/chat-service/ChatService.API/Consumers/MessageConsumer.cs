using Aniverse.MessageContracts.Events.Message;
using MassTransit;

namespace ChatService.API.Consumers
{
    public class MessageConsumer : IConsumer<MessageCreatedEvent>
    {
        public Task Consume(ConsumeContext<MessageCreatedEvent> context)
        {
            //database save
            throw new NotImplementedException();
        }
    }
}
