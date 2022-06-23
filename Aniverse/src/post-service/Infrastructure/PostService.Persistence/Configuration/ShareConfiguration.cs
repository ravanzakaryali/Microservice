using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostService.Domain.Entities;

namespace PostService.Persistence.Configuration
{
    public class ShareConfiguration : IEntityTypeConfiguration<Share>
    {
        public void Configure(EntityTypeBuilder<Share> builder)
        {
            builder.Property(s => s.PostTitle).HasMaxLength(100);
            builder.Property(s => s.PostUrl).IsRequired()
                                            .HasMaxLength(100);
            builder.Property(s => s.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(b => b.Id).HasDefaultValueSql("NEWID()");
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
            builder.Property(b => b.UpdatedDate).HasDefaultValueSql("NULL");
        }
    }
}
