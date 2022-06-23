using PostService.Domain.Common;

namespace PostService.Domain.Entities
{
    public class Post : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public string Postname { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Share> Shares { get; set; }

    }
}
