namespace Aniverse.MessageContracts.Events.Post
{
    public class PostStartedEvent
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public List<Models.File> FilesName { get; set; }
    }
}
