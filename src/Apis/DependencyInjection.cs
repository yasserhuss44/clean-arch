 
namespace Apis;

public static class DependencyInjection
{
    internal static IServiceCollection AddWeb(
        this IServiceCollection services,
        Assembly[]assemblies)
    {
        //services.AddApiKey();

        foreach (var assembly in assemblies)
        {
            services.AddControllers()
                       .AddApplicationPart(assembly);

            services.AddAutoMapperProfiles(assembly);

            services.AddFluentValidation(assembly);
        }
        return services;
    }

    internal static void AddAutoMapperProfiles(
       this IServiceCollection services,Assembly assembly)
       => services.AddAutoMapper(assembly);

    internal static void AddFluentValidation(
        this IServiceCollection services, Assembly assembly)
        => services.AddValidatorsFromAssembly(assembly);
 
}
