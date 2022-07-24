using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SagaStateMachine.Service.Instruments.Post
{
    public class AppStateMap : SagaClassMap<AppStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<AppStateInstance> entity, ModelBuilder model)
        {
        }
    }
}
