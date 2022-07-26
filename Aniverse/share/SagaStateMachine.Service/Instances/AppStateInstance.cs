using MassTransit;
using System.ComponentModel.DataAnnotations.Schema;

namespace SagaStateMachine.Service.Instruments.Post
{
    public class AppStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public int MessageStateId { get; set; }
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public string Message { get; set; }
        public int PostStateId { get; set; }
        public string PostId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        [NotMapped]
        public List<Aniverse.MessageContracts.Models.File> FilesName { get; set; }
    }
}
