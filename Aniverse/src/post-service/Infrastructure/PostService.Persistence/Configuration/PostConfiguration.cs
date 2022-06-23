using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostService.Domain.Entities;

namespace PostService.Persistence.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Content).HasMaxLength(369);
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(b => b.Id).HasDefaultValueSql("NEWID()");
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
            builder.Property(b => b.UpdatedDate).HasDefaultValueSql("NULL");
        }
    }
}
