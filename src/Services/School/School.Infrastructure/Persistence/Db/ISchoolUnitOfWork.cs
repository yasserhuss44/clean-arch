namespace School.Infrastructure.Persistence.Db;

public interface ISchoolUnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : SchoolDbContext
{
}

