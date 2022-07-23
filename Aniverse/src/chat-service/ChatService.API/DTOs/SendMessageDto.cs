namespace ChatService.API.DTOs
{
    public class SendMessageDto
    {
        public string Content { get; set; }
        public DateTime SenderDate { get; set; }
        public bool IsDeleted { get; set; }
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
    }
}
