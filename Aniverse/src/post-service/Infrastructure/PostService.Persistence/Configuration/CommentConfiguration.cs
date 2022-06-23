using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostService.Domain.Entities;

namespace PostService.Persistence.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(c => c.Content).HasMaxLength(369)
                                          .IsRequired();
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(b => b.Id).HasDefaultValueSql("NEWID()");
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
            builder.Property(b => b.UpdatedDate).HasDefaultValueSql("NULL");
        }
    }
}
