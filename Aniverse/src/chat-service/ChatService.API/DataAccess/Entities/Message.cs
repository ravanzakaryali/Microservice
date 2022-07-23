namespace ChatService.API.DataAccess.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime SenderDate { get; set; }
        public bool IsDeleted { get; set; }
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
    }
}
