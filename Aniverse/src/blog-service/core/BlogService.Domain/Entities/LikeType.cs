using BlogService.Domain.Common;

namespace BlogService.Domain.Entities
{
    public class LikeType : BaseEntity
    {
        public string Name { get; set; }
        public string IconSrc { get; set; }
    }
}
