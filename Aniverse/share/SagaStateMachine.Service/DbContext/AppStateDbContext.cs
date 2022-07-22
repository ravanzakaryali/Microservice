using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SagaStateMachine.Service.StateMaps;

namespace SagaStateMachine.Service.Instruments.Post
{
    public class AppStateDbContext : SagaDbContext
    {
        public AppStateDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get
            {
                yield return new PostStateMap();
                yield return new MessageStateMap();

            }
        }
    }
}
