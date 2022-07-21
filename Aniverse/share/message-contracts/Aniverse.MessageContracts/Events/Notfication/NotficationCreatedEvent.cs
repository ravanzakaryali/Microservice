using MassTransit;

namespace Aniverse.MessageContracts.Events.Notfication
{
    public class NotficationCreatedEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public NotficationCreatedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
    }
}
