using PostService.Domain.Common;

namespace PostService.Domain.Entities
{
    public class Share : BaseEntity
    {
        public Guid SendUserId { get; set; }
        public Guid ReceivedUserId { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public string PostTitle { get; set; }
        public string PostUrl { get; set; }
    }
}
