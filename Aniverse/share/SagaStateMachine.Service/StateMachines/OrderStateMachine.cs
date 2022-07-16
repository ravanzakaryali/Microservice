using MassTransit;
using SagaStateMachine.Service.Instruments.Post;

namespace SagaStateMachine.Service.StateMachines
{
    public class OrderStateMachine : MassTransitStateMachine<PostStateInstance>
    {
    }
}
