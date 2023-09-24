namespace Infrastructure.Persisitence.Db;

public interface ISchoolUnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : SchoolDbContext
{
}

