using BlogService.Domain.Common;

namespace BlogService.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string UserId { get; set; }
        public string BlogId { get; set; } 
        public string Descreption { get; set; }
        public string CommentId { get; set; }
        public ICollection<Comment> ReplyComments { get; set; }
        public ICollection<Hastag> Hastags { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
