using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SagaStateMachine.Service.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<Aniverse.MessageContracts.Models.File>
    {
        public void Configure(EntityTypeBuilder<Aniverse.MessageContracts.Models.File> builder)
        {
            builder.Property(f => f.Id).HasDefaultValueSql("NEWID()");
        }
    }
}
