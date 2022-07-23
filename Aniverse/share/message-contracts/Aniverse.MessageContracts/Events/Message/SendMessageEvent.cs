using MassTransit;

namespace Aniverse.MessageContracts.Events.Message
{
    public class SendMessageEvent 
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime SenderDate { get; set; }

    }
}
