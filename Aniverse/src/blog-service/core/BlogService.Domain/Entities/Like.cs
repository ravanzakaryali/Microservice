using BlogService.Domain.Common;

namespace BlogService.Domain.Entities
{
    public class Like : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid BlogId { get; set; }
        public Guid CommentId { get; set; }
        public Blog Blog { get; set; }
        public Comment Comment { get; set; }
        public Guid LikeTypeId { get; set; }
        public LikeType LikeType { get; set; }
    }
}
