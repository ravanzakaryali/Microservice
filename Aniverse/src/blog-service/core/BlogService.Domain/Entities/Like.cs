using BlogService.Domain.Common;

namespace BlogService.Domain.Entities
{
    public class Like : BaseEntity
    {
        public string UserId { get; set; }
        public string BlogId { get; set; }
        public string LikeTypeId { get; set; }
    }
}
