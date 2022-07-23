using MassTransit;

namespace Aniverse.MessageContracts.Events.Message
{
    public class SendMessageEvent 
    {
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public string Message { get; set; }
        public DateTime SenderDate { get; set; }

    }
}
