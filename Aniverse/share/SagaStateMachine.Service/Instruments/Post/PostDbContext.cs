using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace SagaStateMachine.Service.Instruments.Post
{
    public class PostDbContext : SagaDbContext
    {
        public PostDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get
            {
                yield break;
            }
        }
    }
}
