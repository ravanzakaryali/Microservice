using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SagaStateMachine.Service.Instruments.Post
{
    public class PostStateMap : SagaClassMap<PostStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<PostStateInstance> entity, ModelBuilder model)
        {
            entity.Property(x => x.UserId).IsRequired();
            entity.Property(x => x.PostId).IsRequired();
            entity.Property(x => x.Content).IsRequired();
        }
    }
}
