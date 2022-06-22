using PostService.Domain.Common;

namespace PostService.Domain.Entities
{
    public class Comment : BaseEntity 
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public ICollection<Like> Likes { get; set; }
        public Guid? ReplyCommentId { get; set; }
        public ICollection<Comment> ReplyComment { get; set; }
    }
}
