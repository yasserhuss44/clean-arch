
using School.Infrastructure.Persistence.Db;

namespace School.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddSchoolInfrastructure(
                                    this IServiceCollection services,
                                    IConfiguration configuration)
        {
            services.RegisterSchoolDBAndUnitOfWork(configuration);

            return services;
        }

        public static void RegisterSchoolDBAndUnitOfWork(
                        this IServiceCollection services,
                        IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SchoolConnection");

            // Configure DbContext with SQL Server
            services.AddDbContext<SchoolDbContext>(options =>
                                                   options.UseSqlServer(connectionString));

            services.AddScoped(typeof(ISchoolUnitOfWork<>), typeof(SchoolUnitOfWork<>));


        }
    }
}
