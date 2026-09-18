using System.Linq.Expressions;

namespace HR_MVC_ITI.Models.IRepository;

public interface IGenericRepository<T> where T : class
{ 
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    
    void Delete(T entity);
}
