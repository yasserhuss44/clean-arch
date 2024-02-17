 
using Transportation.Infrastructure.Persistence.Db;

namespace Transportation.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTransportInfrastructure(
                            this IServiceCollection services,
                            IConfiguration configuration)
        {
            services.RegisterTransportationDBAndUnitOfWork(configuration);

            return services;
        }

        public static void RegisterTransportationDBAndUnitOfWork(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TransportationConnection");

            // Configure DbContext with SQL Server
            services.AddDbContext<TransportationDbContext>(options =>
                                                   options.UseSqlServer(connectionString));

            services.AddScoped(typeof(ITransportationUnitOfWork<>), typeof(TransportationUnitOfWork<>));
        }
    }
}


