using MassTransit;

namespace SagaStateMachine.Service.Instances
{
    public class MessageStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }

}
