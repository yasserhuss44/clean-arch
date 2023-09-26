namespace Transportation.Infrastructure.Persistence.Db;

public interface ITransportationUnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : TransportationDbContext
{
}

