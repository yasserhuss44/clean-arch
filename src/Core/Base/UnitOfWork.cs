using Core.Interfaces;


namespace Core.Base;
public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext :  DbContext
{
    private readonly DbContext _context;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories == null)
        {
            _repositories = new Dictionary<Type, object>();
        }

        var entityType = typeof(TEntity);

        if (!_repositories.ContainsKey(entityType))
        {
            _repositories[entityType] = new RepositoryBase<TEntity>(_context);
        }

        return (IRepository<TEntity>)_repositories[entityType];
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}