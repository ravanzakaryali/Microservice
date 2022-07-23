using MassTransit;

namespace SagaStateMachine.Service.Instances
{
    public class MessageStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public string Message { get; set; }
    }

}
