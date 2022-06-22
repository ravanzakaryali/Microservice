using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;
using PostService.Domain.Interfaces;
using PostService.Persistence.DataContext;
using System.Linq.Expressions;

namespace PostService.Repository.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> exp = null, bool tracking = true, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync<TOrderBy>(int page, int size, Expression<Func<T, TOrderBy>> orderBy, Expression<Func<T, bool>> exp = null, bool tracking = true, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate = null, bool tracking = true, params string[] include)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(string id, bool tracking = true, params string[] include)
        {
            throw new NotImplementedException();
        }
        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> AddRangeAsync(List<T> listModel)
        {
            throw new NotImplementedException();
        }
        public T Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public List<T> RemoveRange(List<T> listModel)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
