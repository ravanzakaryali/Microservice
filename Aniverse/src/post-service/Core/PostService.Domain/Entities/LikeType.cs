using PostService.Domain.Common;

namespace PostService.Domain.Entities
{
    public class LikeType : BaseEntity
    {
        public string Name { get; set; }
        public string IconSrc { get; set; }
    }
}
