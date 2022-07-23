using Aniverse.MessageContracts;
using Aniverse.MessageContracts.Events.File;
using Aniverse.MessageContracts.Events.Message;
using Aniverse.MessageContracts.Events.Notfication;
using Aniverse.MessageContracts.Events.Post;
using MassTransit;
using SagaStateMachine.Service.Instruments.Post;

namespace SagaStateMachine.Service.StateMachines
{
    public class PostStateMachine : MassTransitStateMachine<PostStateInstance>
    {
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
        public PostStateMachine()
        {
            InstanceState(instance => instance.CurrentState);
            Event(() => PostStartedEvent,
              postStateInstance =>
              postStateInstance.CorrelateBy<string>(database => database.PostId, @event => @event.Message.PostId)
              .SelectId(e => Guid.NewGuid()));

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
                }),
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
                }));
            SetCompletedWhenFinalized();
        }
    }
}
