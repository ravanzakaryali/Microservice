namespace Aniverse.MessageContracts.Commands
{
    public interface IPostCommand
    {
        public string UserId { get; set; }
        public string Content { get; set; }
    }
}
