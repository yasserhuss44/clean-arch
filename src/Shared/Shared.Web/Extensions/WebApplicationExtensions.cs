using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OwaspHeaders.Core.Extensions;
using OwaspHeaders.Core.Models;
using Shared.Web.Middleware;
using System.Reflection;

namespace Shared.Web.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureSharedWeb(
       this WebApplication app,
       IConfiguration configuration)
    {
        app.UseOwaspSecureHeaders();

        app.UseHttpLogging();

        app.UseResponseCaching();

        if (app.Environment.IsDevelopment())
        {
            //app.UseDeveloperExceptionPage();
        }

        if (!app.Environment.IsProduction())
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = false;
                c.RouteTemplate = "/swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Portal Backend API");
            });
        }

        app.UseCors();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseExceptionMiddleware();

        app.UseApiKeyAuth();

        app.UseSanitizerMiddleware();

        app.UseFeatureFlags();

        app.UseResponseCompression();

        app.UseAutoLog(configuration);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
    //TODO:: unify the add serilog in all projects
    /// <summary>
    /// add serilog and setting it as logging provider
    /// </summary>   
    public static IHostBuilder AddSerilog(
        this IHostBuilder host)
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

        host.UseSerilog();

        return host;
    }

    /// <summary>
    /// adds appsetting.json files into the IConfiguration
    /// </summary>
    public static IHostBuilder AddConfigurations(
        this IHostBuilder host ,
        string[] args)
    {
        Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        host.ConfigureAppConfiguration((hostContext , config) =>
        {
            config.SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json" , optional: false , reloadOnChange: true)
                  .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json" , optional: true , reloadOnChange: true)
                  .AddCommandLine(args);
        });

        return host;
    }

    public static int RunWebApp(
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

    public static void UseSanitizerMiddleware(
    this IApplicationBuilder app)
    => app.UseMiddleware<SanitizerMiddleware>();

    public static void UseApiKeyAuth(
        this IApplicationBuilder app)
        => app.UseMiddleware<ApiKeyMiddleware>();

    /// <summary>
    /// use it before UseEndpoints middleware directly
    /// </summary>   
    public static void UseAutoLog(
        this IApplicationBuilder app ,
        IConfiguration configuration)
    {
        if(configuration.GetValue<bool>(ConfigKeys.IsAPILogEnabled))
            app.UseMiddleware<AutoLogMiddleWare>();
    }

    public static void UseFeatureFlags(
        this IApplicationBuilder app)
        => app.UseMiddleware<FeatureFlagsMiddleware>();

    public static void UseOwaspSecureHeaders(
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

    public static void UseExceptionMiddleware(
       this IApplicationBuilder app)
       => app.UseMiddleware<ExceptionMiddleware>();
}