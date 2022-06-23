using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostService.Domain.Entities;

namespace PostService.Persistence.Configuration
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.Property(l=>l.UserId).IsRequired();
            builder.Property(l => l.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(l => l.Id).HasDefaultValueSql("NEWID()");

        }
    }
}
