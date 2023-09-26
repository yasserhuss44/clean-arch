
using School.Application.Students;

namespace Apis;

public static class DependencyInjection
{
    internal static IServiceCollection AddWeb(
        this IServiceCollection services ,
        IConfiguration configuration)
    {
        //services.AddApiKey();

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

        services.AutoRegisterBusinessServices(typeof(StudentService).Assembly);
        
        services.AutoRegisterBusinessServices(typeof(DriverService).Assembly);

        services.AddAuthorization(options =>
        {
  //          options.AddAuthorizationPolicies();
        });

        services.AddJwtAuthentication(configuration);

        services.AddAutoMapperProfiles();

       // services.AddFeatureFlags();

        services.AddResponseCompressionProviders();

        services.AddSanitizerMiddleware();

        services.AddExceptionMiddleware();

        return services;
    }
}
