using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;
using System.Linq.Expressions;

namespace PostService.Inerfaces.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, 
                                  bool tracking = true,
                                  params string[] includes);
        Task<List<T>> GetAllAsync<TOrderBy>(int page,
                                            int size,
                                            Expression<Func<T, TOrderBy>> orderBy, 
                                            Expression<Func<T, bool>> predicate = null, 
                                            bool isOrderBy = true,
                                            bool tracking = true,
                                            params string[] includes);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate = null, 
                        bool tracking = true, 
                        params string[] include);
        Task<T> GetAsync(string id, 
                         bool tracking = true, 
                         params string[] include);
        Task<T> AddAsync(T entity);
        T Remove(T entity);
        Task<T> RemoveAsync(string id);
        T Update(T entity);
        Task<int> SaveAsync();
    }
}
