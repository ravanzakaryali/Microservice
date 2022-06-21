using PostService.Domain.Common;

namespace PostService.Domain.Entities
{
    public class Like : BaseEntity
    {
        public string UserId { get; set; }
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }
        public Post Post { get; set; }
        public Comment Comment { get; set; }
        public Guid LikeTypeId { get; set; }
        public LikeType LikeType { get; set; }
    }
}
