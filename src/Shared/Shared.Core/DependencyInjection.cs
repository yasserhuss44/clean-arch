using Core.Interfaces;
using Core.Services;
using Core.Utilities;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPDFServices();

        services.AutoRegisterCoreServices();

        return services;
    }

    internal static IServiceCollection AddPDFServices(
        this IServiceCollection services)
    {
        services.AddSingleton<IPdfService , DinkPDFfService>();

        services.AddSingleton(typeof(IConverter) , new SynchronizedConverter(new PdfTools()));

        return services;
    }

    private static void AutoRegisterCoreServices(
        this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<ClockService>()
                              .AddClasses(classes => classes.AssignableTo<IScopedService>())
                                  .AsImplementedInterfaces()
                                  .WithScopedLifetime()
                              .AddClasses(classes => classes.AssignableTo<ITransientService>())
                                  .AsImplementedInterfaces()
                                  .WithTransientLifetime()
                              .AddClasses(classes => classes.AssignableTo<ISingltonService>())
                                  .AsImplementedInterfaces()
                                  .WithSingletonLifetime());
    }
}