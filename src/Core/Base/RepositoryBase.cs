using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base;
 
public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id);
    }
    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync();
    }

    public void Add(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
    }

    public void Remove(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
    }
}