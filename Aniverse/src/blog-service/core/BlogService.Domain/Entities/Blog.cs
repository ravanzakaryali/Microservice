using BlogService.Domain.Common;

namespace BlogService.Domain.Entities
{
    public class Blog : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Descreption { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Share> Shares { get; set; }  
    }
}
