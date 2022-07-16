using MassTransit;

namespace Aniverse.MessageContracts.Events.File
{
    public class FileCompletedEvent : CorrelatedBy<Guid>
    {
        public FileCompletedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public string UserId { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
