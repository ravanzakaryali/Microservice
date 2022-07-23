using MassTransit;

namespace Aniverse.MessageContracts.Events.Message
{
    public class MessageCreatedEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public MessageCreatedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public string Message { get; set; }
        public string UserId { get; set; }
        public DateTime SendDate { get; set; }
    }
}