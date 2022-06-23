using PostService.Domain.Entities;
using PostService.Inerfaces.Interfaces;
using PostService.Persistence.DataContext;

namespace PostService.Repository.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly AppDbContext _context;
        public PostRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
