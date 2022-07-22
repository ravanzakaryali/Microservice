using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SagaStateMachine.Service.Instances;

namespace SagaStateMachine.Service.StateMaps
{
    public class MessageStateMap : SagaClassMap<MessageStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<MessageStateInstance> entity, ModelBuilder model)
        {
            base.Configure(entity, model);
        }
    }
}
