using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostService.Domain.Entities;

namespace PostService.Persistence.Configuration
{
    public class BlogConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
            builder.Property(b => b.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.Content).HasMaxLength(369);
        }
    }
}
