using Aniverse.MessageContracts.Events.Message;
using ChatService.API.DataAccess.Entities;
using ChatService.API.Services.Abstractions.MongoDb;
using MassTransit;

namespace ChatService.API.Consumers
{
    public class MessageConsumer : IConsumer<MessageCreatedEvent>
    {
        readonly IMessageService _messageService;

        public MessageConsumer(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task Consume(ConsumeContext<MessageCreatedEvent> context)
        {
            Message message = new()
            {
                Content = context.Message.Message,
                SenderUserId = context.Message.SenderUserId,
                ReceiverUserId = context.Message.ReceiverUserId,
                SenderDate = DateTime.UtcNow
            };
            await _messageService.CreateAsync(message);
        }
    }
}
