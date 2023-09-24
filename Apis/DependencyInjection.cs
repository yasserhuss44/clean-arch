namespace Apis;

public static class DependencyInjection
{
    internal static IServiceCollection AddWeb(
        this IServiceCollection services ,
        IConfiguration configuration)
    {
        //services.AddApiKey();

        services.AddControllers(options =>
                                options.Filters.Add<ApiExceptionFilterAttribute>());

        services.AddSwagger(configuration);

        services.AddHttpLogging(configuration);

        services.AddCorsPolicy(configuration);

        services.AddFluentValidation();

        services.AddHttpContextAccessor();

        services.AddCaptcha();

        services.AddHttpClient();

        services.AddWebServices();

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
