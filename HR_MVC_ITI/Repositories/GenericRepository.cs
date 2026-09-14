using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Data;
using Microsoft.EntityFrameworkCore;

namespace HR_MVC_ITI.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly HRDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(HRDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
       _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
