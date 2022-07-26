using Aniverse.MessageContracts;
using Aniverse.MessageContracts.Events.File;
using Aniverse.MessageContracts.Events.Message;
using Aniverse.MessageContracts.Events.Notfication;
using Aniverse.MessageContracts.Events.Post;
using MassTransit;
using SagaStateMachine.Service.Instruments.Post;

namespace SagaStateMachine.Service.StateMachines
{
    public class AppStateMachine : MassTransitStateMachine<AppStateInstance>
    {
        public Event<SendMessageEvent> SendMessageEvent { get; set; }
        public Event<MessageCreatedEvent> MessageCreatedEvent { get; set; }

        public State MessagCreated { get; set; }
        //public Event<PostCompletedEvent> PostCompletedEvent { get; set; }
        public Event<PostCreatedEvent> PostCreatedEvent { get; set; }
        public Event<NotficationCreatedEvent> NotficationCreatedEvent { get; set; }
        //public Event<PostFailedEvent> PostFailedEvent { get; set; }
        public Event<PostStartedEvent> PostStartedEvent { get; set; }
        //public Event<FileFailedEvent> FileFailedEvent { get; set; }
        //public State PostCompleted { get; set; }
        public State PostCreated { get; set; }
        public State MessageCreated { get; set; }
        public State NotficationCreated { get; set; }
        //public State PostFailed { get; set; }
        public State PostStarted { get; set; }
        //public State FileFailed { get; set; }   
        //public State FileFiled { get; set; }
        public AppStateMachine()
        {
            InstanceState(instance => instance.CurrentState);
            Event(() => PostStartedEvent,
              postStateInstance =>
              postStateInstance.CorrelateBy<string>(database => database.PostId, @event => @event.Message.PostId)
              .SelectId(e => Guid.NewGuid()));
            Event(() => SendMessageEvent,
                sendStateInstance =>
                sendStateInstance.CorrelateBy<string>(database => database.SenderUserId, @event => @event.Message.SenderUserId)
                .SelectId(e=>Guid.NewGuid()));
            Event(() => MessageCreatedEvent,
             messageCreatedInstance =>
             messageCreatedInstance.CorrelateById(@event => @event.Message.CorrelationId));


            Event(() => NotficationCreatedEvent,
                notficationStateInstance =>
                notficationStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

            Event(() => PostCreatedEvent,
              postStateInstance =>
              postStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

            Initially(
                When(PostStartedEvent)
                .Then(context =>
                {
                    context.Saga.PostId = context.Message.PostId;
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.Content = context.Message.Content;
                    context.Saga.FilesName = context.Message.FilesName;
                }).TransitionTo(PostCreated)
                .Send(new Uri($"queue:{RabbitMqConstants.FileServiceSendQueue}"), context => new PostCreatedEvent(context.Saga.CorrelationId)
                {
                    Content = context.Message.Content,
                    UserId = context.Message.UserId,
                    PostId = context.Message.PostId,
                    FilesName = context.Message.FilesName,
                }).Finalize(),
                When(PostStartedEvent)
                .Then(context =>
                {
                    context.Saga.UserId = context.Message.UserId;
                }).TransitionTo(NotficationCreated)
                .Send(new Uri($"queue:{RabbitMqConstants.NotificationServiceQueue}"), context => new NotficationCreatedEvent(context.Saga.CorrelationId)
                {
                    UserId = context.Message.UserId,
                    Content = context.Message.Content,
                    Url = context.Message.PostId
                }),
                 When(SendMessageEvent)
                .Then(context =>
                {
                    context.Saga.ReceiverUserId = context.Message.ReceiverUserId;
                    context.Saga.SenderUserId = context.Message.SenderUserId;
                    context.Saga.Message = context.Message.Message;
                }).TransitionTo(MessagCreated)
                .Send(new Uri($"queue:{RabbitMqConstants.SendMessageQueue}"), context => new MessageCreatedEvent(context.Saga.CorrelationId)
                {
                    Message = context.Message.Message,
                    SendDate = context.Message.SenderDate,
                    SenderUserId = context.Message.SenderUserId,
                    ReceiverUserId = context.Message.ReceiverUserId,
                }).Finalize());
            SetCompletedWhenFinalized();
        }
    }
}
