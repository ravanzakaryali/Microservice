using BlogService.Domain.Common;

namespace BlogService.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Descreption { get; set; }
        public Guid UserId { get; set; }
        public Guid BlogId { get; set; } 
        public ICollection<Like> Likes { get; set; }
    }
}
