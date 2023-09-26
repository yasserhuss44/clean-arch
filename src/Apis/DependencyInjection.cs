
using Apis.Extensions;
using School.Application.Students;

namespace Apis;

public static class DependencyInjection
{
    internal static IServiceCollection AddWeb(
        this IServiceCollection services ,
        IConfiguration configuration)
    {
        //services.AddApiKey();
        Assembly[] assemblies = { typeof(StudentService).Assembly, typeof(DriverService).Assembly };       

        services.RegisterGenericRepositoryAndUOW();

        services.RegisterSchoolDBAndUnitOfWork(configuration);

        services.RegisterTransportationDBAndUnitOfWork(configuration);

        services.AddCore();

        services.AddControllers(options =>
                                options.Filters.Add<ApiExceptionFilterAttribute>());

        services.AddSwagger(configuration);

        services.AddHttpLogging(configuration);

        services.AddCorsPolicy(configuration);

        services.AddFluentValidation();

        services.AddHttpContextAccessor();

        services.AddCaptcha();

        services.AddHttpClient();

        services.AutoRegisterBusinessServices(assemblies); 

        services.AddAuthorization(options =>
        {
  //          options.AddAuthorizationPolicies();
        });

        services.AddJwtAuthentication(configuration);

        services.AddAutoMapperProfiles(assemblies);

        // services.AddFeatureFlags();

        services.AddResponseCompressionProviders();

        services.AddSanitizerMiddleware();

        services.AddExceptionMiddleware();

        return services;
    }
}
