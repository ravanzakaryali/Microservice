using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

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
                yield return new AppStateMap();

            }
        }
    }
}
