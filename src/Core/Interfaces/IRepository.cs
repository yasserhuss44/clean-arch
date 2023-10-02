using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Interfaces;
 

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    void Add(TEntity entity, CancellationToken cancellationToken);
    void Update(TEntity entity, CancellationToken cancellationToken);
    void Remove(TEntity entity, CancellationToken cancellationToken);
}