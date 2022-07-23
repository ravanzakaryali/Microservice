using Aniverse.MessageContracts;
using Aniverse.MessageContracts.Events.Message;
using MassTransit;
using SagaStateMachine.Service.Instances;

namespace SagaStateMachine.Service.StateMachines
{
    public class MessageStateMachine : MassTransitStateMachine<MessageStateInstance>
    {
        public Event<SendMessageEvent> SendMessageEvent { get; set; }
        public Event<MessageCreatedEvent> MessageCreatedEvent { get; set; } 

        public State SendMessage { get; set; }
        public State MessagCreated { get; set; }
        public MessageStateMachine()
        {
            InstanceState(instance => instance.CurrentState);
            Event(() => MessageCreatedEvent,
                messageCreatedInstance =>
                messageCreatedInstance.CorrelateById(@event => @event.Message.CorrelationId));
            Initially(
                When(SendMessageEvent)
                .Then(context =>
                {
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.Message = context.Message.Message;
                }).TransitionTo(MessagCreated)
                .Send(new Uri($"queue:{RabbitMqConstants.SendMessageQueue}"), context => new MessageCreatedEvent(context.Saga.CorrelationId)
                {
                    Message = context.Message.Message,
                    SendDate = context.Message.SenderDate,
                    UserId = context.Message.UserId
                }));
        }
    }
}
