using MassTransit;

namespace Aniverse.MessageContracts.Events.File
{
    public class FileFailedEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public FileFailedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public string UserId { get; set; }
        public ICollection<string> FilesName { get; set; }
    }
}
