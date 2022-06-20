using BlogService.Domain.Common;

namespace BlogService.Domain.Entities
{
    public class Share : BaseEntity
    {
        public string PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostUrl { get; set; }
    }
}
