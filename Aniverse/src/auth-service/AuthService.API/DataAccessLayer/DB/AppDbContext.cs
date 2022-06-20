using Microsoft.EntityFrameworkCore;

namespace AuthService.API.DataAccessLayer.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<DbContext> options) : base(options) { }
    }
}
