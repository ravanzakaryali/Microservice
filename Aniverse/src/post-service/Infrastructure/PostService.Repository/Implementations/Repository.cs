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

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null,
                                      bool tracking = true,
                                      params string[] includes)
        {
            IQueryable<T> query = GetQuery(includes);
            if (!tracking)
                query = Table.AsNoTracking();
            return predicate is null
                ? await query.FirstOrDefaultAsync()
                : await query.Where(predicate).FirstOrDefaultAsync();
        }
        public async Task<T> GetAsync(string id,
                                      bool tracking = true,
                                      params string[] includes)
        {
            IQueryable<T> query = GetQuery(includes);
            if (!tracking)
                query = Table.AsNoTracking();
            return id is null
                ? await query.FirstOrDefaultAsync()
                : await query.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
                                         bool tracking = true,
                                         params string[] includes)
        {
            IQueryable<T> query = GetQuery(includes);
            if (!tracking)
                query = Table.AsNoTracking();
            return predicate is null
                ? await query.ToListAsync()
                : await query.Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TOrderBy>(int page, int size,
                                                  Expression<Func<T, TOrderBy>> orderBy,
                                                  Expression<Func<T, bool>> predicate = null,
                                                  bool tracking = true,
                                                  bool isOrderBy = true,
                                                  params string[] includes)
        {
            IQueryable<T> query = GetQuery(includes);
            if (!tracking)
                query = Table.AsNoTracking();
            return predicate is null
                ? await query.ToListAsync()
                : isOrderBy ?
                await query.Where(predicate).OrderBy(orderBy).Skip((page - 1) * size).Take(size).ToListAsync()
                :
                await query.Where(predicate).OrderByDescending(orderBy).Skip((page - 1) * size).Take(size).ToListAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            var newEntity = await _context.Set<T>().AddAsync(entity);
            return newEntity.Entity;
        }
        public async Task<bool> IsAddAsync(T entity)
        {
            return await _context.Set<T>().AddAsync(entity) is null;
        }   
        public T Remove(T entity)
        {
            return _context.Set<T>().Remove(entity).Entity;
        }
        public bool IsRemove(T entity)
        {
            return _context.Set<T>().Remove(entity) is null;
        }
        public async Task<T> RemoveAsync(string id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == Guid.Parse(id));
        }
        public async Task<bool> IsRemoveAsync(string id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == Guid.Parse(id)) is null;
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public T Update(T entity)
        {
           return _context.Set<T>().Update(entity).Entity;
        }
        public bool IsUpdate(T entity)
        {
            return _context.Set<T>().Update(entity).Entity is null;
        }
        private IQueryable<T> GetQuery(params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }

    }
}
