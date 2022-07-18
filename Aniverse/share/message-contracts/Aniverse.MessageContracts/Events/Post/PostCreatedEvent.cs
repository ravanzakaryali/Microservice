using MassTransit;

namespace Aniverse.MessageContracts.Events.Post
{
    public class PostCreatedEvent : CorrelatedBy<Guid>
    {
        public string PostId { get; set; }
        public Guid CorrelationId { get; set; }
        public PostCreatedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public string UserId { get; set; }
        public string Content { get; set; }
        public ICollection<Models.File> FilesName { get; set; }
    }
}
