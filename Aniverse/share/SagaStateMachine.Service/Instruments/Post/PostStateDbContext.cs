using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SagaStateMachine.Service.Configurations;

namespace SagaStateMachine.Service.Instruments.Post
{
    public class PostStateDbContext : SagaDbContext
    {
        public PostStateDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get
            {
                yield return new PostStateMap();
            }
        }
    }
}
