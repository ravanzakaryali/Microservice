using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;
using PostService.Domain.Entities;
using PostService.Persistence.Configuration;

namespace PostService.Persistence.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Share> Shares { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LikeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new ShareConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
        }
    }
}
