using MassTransit;

namespace Aniverse.MessageContracts.Events.Message
{
    public class SendMessageEvent :  CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public SendMessageEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime SenderDate { get; set; }

    }
}
