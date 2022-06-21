using BlogService.Domain.Common;

namespace BlogService.Domain.Entities
{
    public class Hastag : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
