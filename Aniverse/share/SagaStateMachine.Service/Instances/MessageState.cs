using MassTransit;

namespace SagaStateMachine.Service.Instances
{
    public class MessageState
    {
        public int Id { get; set; }
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public string Message { get; set; }
    }

}
