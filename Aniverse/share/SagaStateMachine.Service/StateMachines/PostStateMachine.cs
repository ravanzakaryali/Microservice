using Aniverse.MessageContracts;
using Aniverse.MessageContracts.Events.File;
using Aniverse.MessageContracts.Events.Post;
using MassTransit;
using SagaStateMachine.Service.Instruments.Post;

namespace SagaStateMachine.Service.StateMachines
{
    public class PostStateMachine : MassTransitStateMachine<PostStateInstance>
    {
        //public Event<PostCompletedEvent> PostCompletedEvent { get; set; }
        public Event<PostCreatedEvent> PostCreatedEvent { get; set; }
        //public Event<PostFailedEvent> PostFailedEvent { get; set; }
        public Event<PostStartedEvent> PostStartedEvent { get; set; }
        //public Event<FileFailedEvent> FileFailedEvent { get; set; }
        //public State PostCompleted { get; set; }
        public State PostCreated { get; set; }
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
            //Event(() => FileCompletedEventstirng,
            //    fileCompletedInstance =>
            //    fileCompletedInstance.CorrelateById(@event => @event.Message.CorrelationId));
            Event(() => PostCreatedEvent,
              postStateInstance =>
              postStateInstance.CorrelateById(@event => @event.Message.CorrelationId));

            Initially(When(PostStartedEvent)
                .Then(context =>
                {
                    context.Instance.PostId = context.Data.PostId;
                    context.Instance.UserId = context.Data.UserId;
                    context.Instance.Content = context.Data.Content;
                    context.Instance.FilesName = context.Data.FilesName;
                }).TransitionTo(PostCreated)
                .Send(new Uri($"queue:{RabbitMqConstants.FileServiceSendQueue}"), context => new PostCreatedEvent(context.Instance.CorrelationId)
                {
                    Content = context.Data.Content,
                    UserId = context.Data.UserId,
                    PostId = context.Data.PostId,
                    FilesName = context.Data.FilesName,
                }));
            SetCompletedWhenFinalized();
        }
    }
}
