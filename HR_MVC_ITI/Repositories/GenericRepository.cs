using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        if (includes == null || includes.Length == 0)
        {
            return await _dbSet.FindAsync(id);
        }

        IQueryable<T> query = _dbSet;
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
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
