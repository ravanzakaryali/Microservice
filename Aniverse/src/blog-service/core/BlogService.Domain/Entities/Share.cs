using BlogService.Domain.Common;

namespace BlogService.Domain.Entities
{
    public class Share : BaseEntity
    {
        public Guid SendUserId { get; set; }
        public Guid ReceivedUserId { get; set; }
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }
        public string BlogTitle { get; set; }
        public string BlogUrl { get; set; }
    }
}
