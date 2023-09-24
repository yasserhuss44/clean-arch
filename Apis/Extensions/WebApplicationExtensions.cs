namespace Apis.Extensions;

public static class WebApplicationExtensions
{
    internal static IHostBuilder AddSerilog(
        this IHostBuilder host ,
        string[] args)
    {
        Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json" , optional: false , reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Testing"}.json" , optional: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        host = host.ConfigureAppConfiguration((hostContext , config) =>
        {
            config.SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json" , optional: false , reloadOnChange: true)
                  .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json" , optional: true , reloadOnChange: true)
                  .AddCommandLine(args);
        });

        host.UseSerilog();

        return host;
    }

    internal static WebApplication Configure(
        this WebApplication app ,
        IConfiguration configuration)
    {
        app.UseExceptionMiddleware();

        app.UseOwaspSecureHeaders();

        app.UseHttpLogging();

        app.UseResponseCaching();

        if(app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        if(!app.Environment.IsProduction())
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
                c.RouteTemplate = "/swagger/{documentName}/swaggerv2.json";
            });

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = false;
                c.RouteTemplate = "/swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json" , "Swagger V03");
                options.SwaggerEndpoint("/swagger/v1/swaggerv2.json" , "Swagger V02");
            });
        }

        app.UseCors();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        //app.UseApiKeyAuth();

        app.UseSanitizerMiddleware();

       // app.UseFeatureFlags();

        app.UseResponseCompression();

        app.UseAutoLog(configuration);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }

    internal static int RunWebApp(
      this WebApplication app)
    {
        try
        {
            Log.Information("Starting web host");

            app.Run();

            Log.Information("Started web host");

            return 0;
        }
        catch(Exception ex)
        {
            Log.Fatal(ex , "Host terminated unexpectedly");

            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void UseSanitizerMiddleware(
    this IApplicationBuilder app)
    => app.UseMiddleware<SanitizerMiddleware>();

    private static void UseApiKeyAuth(
        this IApplicationBuilder app)
        => app.UseMiddleware<ApiKeyMiddleware>();

    /// <summary>
    /// use it before UseEndpoints middleware directly
    /// </summary>   
    private static void UseAutoLog(
        this IApplicationBuilder app ,
        IConfiguration configuration)
    {
        if(configuration.GetValue<bool>(ConfigKeys.IsAPILogEnabled))
            app.UseMiddleware<AutoLogMiddleWare>();
    }

    //private static void UseFeatureFlags(
    //    this IApplicationBuilder app)
    //    => app.UseMiddleware<FeatureFlagsMiddleware>();

    private static void UseOwaspSecureHeaders(
        this IApplicationBuilder app)
    {
        //for custom config: https://github.com/GaProgMan/OwaspHeaders.Core
        app.UseSecureHeadersMiddleware(CustomOwaspConfiguration());
    }

    private static SecureHeadersMiddlewareConfiguration CustomOwaspConfiguration()
    {
        return SecureHeadersMiddlewareBuilder
            .CreateBuilder()
            .UseHsts()
            .UseXFrameOptions()
            .UseContentTypeOptions()
            //.UseContentDefaultSecurityPolicy()
            .UsePermittedCrossDomainPolicies()
            .UseReferrerPolicy()
            //.UseCacheControl()
            .UseExpectCt(string.Empty , enforce: true)
            .RemovePoweredByHeader()
            .Build();
    }

    private static void UseExceptionMiddleware(
       this IApplicationBuilder app)
       => app.UseMiddleware<ExceptionMiddleware>();
}
