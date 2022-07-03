using Aniverse.MessageContracts.Commands;

namespace Aniverse.MessageContracts.Models
{
    public class Post : IPostCommand
    {
        public string UserId { get; set; }
        public string Content { get; set; }
    }
}
