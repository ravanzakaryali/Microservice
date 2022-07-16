using MassTransit;

namespace Aniverse.MessageContracts.Events.Post
{
    public class PostCreatedEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public PostCreatedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public string UserId { get; set; }
        public string Content { get; set; }
        public ICollection<string> FilesName { get; set; } 
    }
}
