using FluentValidation;
using FluentValidation.AspNetCore;
using Logging.Har;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Web.Middleware;
using Shared.Web.Security;
using Shared.Web.Security.Authorization;
using Shared.Web.Security.Services;
using SixLaborsCaptcha.Mvc.Core;
using System.IO.Compression;
using System.Reflection;

namespace Shared.Web.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddSharedWebServices(
       this IServiceCollection services ,
       Assembly[] assemblies,
       IConfiguration configuration)
    {

        services.AddApiKey();

        services.AddControllers();

        services.AddSwagger(configuration);

        services.AddHttpLogging(configuration);

        services.AddCorsPolicy(configuration);

        services.AddFluentValidation();

        services.AddHttpContextAccessor();

        services.AddCaptcha();

        services.AddHttpClient();

        services.AddSecurityServices();

        services.AddAuthorization(options =>
        {
            options.AddAuthorizationPolicies();
        });

        services.AddJwtAuthentication(configuration);

        services.AddAutoMapperProfiles();

        services.AddFeatureFlags();

        services.AddResponseCompressionProviders();

        services.AddSanitizerMiddleware();

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
   this IServiceCollection services, Assembly assembly)
   => services.AddAutoMapper(assembly);

    internal static void AddFluentValidation(
        this IServiceCollection services, Assembly assembly)
        => services.AddValidatorsFromAssembly(assembly);

    public static void AddApiKey(
        this IServiceCollection services)
        => services.AddTransient<ApiKeyMiddleware>();

    public static void AddSecurityServices(
        this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<TokenService>()
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

    public static void AddAuthorizationPolicies(
        this AuthorizationOptions options)
    {
        var type = typeof(IAuthorizationRequirementConfig);

        var types = AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => type.IsAssignableFrom(p));

        foreach(var currentType in types.Where(t => t.Name != nameof(IAuthorizationRequirementConfig)))
        {
            var requirement = (IAuthorizationRequirementConfig)Activator.CreateInstance(currentType);

            options.AddPolicy(requirement.PolicyName , policy =>
            {
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);

                policy.RequireAuthenticatedUser();

                policy.Requirements.Add(new AuthorizationMatrixRequirement(
                    requirement.RequestType  ,
                    requirement.AuthorizationAccessLevel.ToInt() ));
            });
        }
    }

    public static void AddResponseCompressionProviders(
        this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest;
        });

        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.SmallestSize;
        });
    }
    

    internal static void AddSanitizerMiddleware(
        this IServiceCollection services)
        => services.AddTransient<SanitizerMiddleware>();

    internal static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services ,
        IConfiguration configuration)
    {
        var signingKey = Encoding.UTF8.GetBytes(configuration.GetValue<string>(ConfigKeys.JwtEncryptionKey));

        services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwtOptions =>
        {
            jwtOptions.SaveToken = true;
            jwtOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false ,
                ValidateIssuer = false ,
                ValidateIssuerSigningKey = true ,
                IssuerSigningKey = new SymmetricSecurityKey(signingKey) ,
                ValidateLifetime = true
            };
        });

        return services;
    }

    internal static void AddFeatureFlags(
       this IServiceCollection services)
       => services.AddTransient<FeatureFlagsMiddleware>();

    internal static void AddAutoMapperProfiles(
        this IServiceCollection services)
        => services.AddAutoMapper(typeof(SecurityMappingProfile).Assembly);

    internal static void AddSwagger(
        this IServiceCollection services ,
        IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1" , new OpenApiInfo { Title = "NDP API" , Version = "v1" });

            c.AddServer(new OpenApiServer
            {
                Url = configuration.GetValue<string>(ConfigKeys.ApiHost)
            });

            c.AddSecurityDefinition("Bearer" , new OpenApiSecurityScheme()
            {
                Name = "Authorization" ,
                Type = SecuritySchemeType.Http ,
                Scheme = "bearer" ,
                BearerFormat = "JWT" ,
                In = ParameterLocation.Header ,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        In = ParameterLocation.Header
                    },
                    Array.Empty<string>()
                }
            });

            c.AddSecurityDefinition("ApiKey" , new OpenApiSecurityScheme
            {
                Description = "ApiKey must appear in header" ,
                Type = SecuritySchemeType.ApiKey ,
                Name = SecurityConstants.ApiKeyHeaderName ,
                In = ParameterLocation.Header ,
                Scheme = "ApiKeyScheme"
            });

            var key = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme ,
                    Id = "ApiKey"
                } ,
                In = ParameterLocation.Header
            };

            var requirement = new OpenApiSecurityRequirement
            {
                {
                    key,
                    new List<string>()
                }
            };

            c.AddSecurityRequirement(requirement);
        });
    }

    internal static void AddHttpLogging(
        this IServiceCollection services ,
        IConfiguration configuration)
    {
        if(configuration.GetValue<bool>(ConfigKeys.IsAPILogEnabled)
                        .IsFalsy())
            return;

        services.AddSingleton<IHarLogger , HarLogger>();
    }

    internal static void AddCorsPolicy(
        this IServiceCollection services ,
        IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins(configuration.GetValue<string>(ConfigKeys.AllowedCors).Split(','))
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
            });
        });
    }

    internal static void AddFluentValidation(
        this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddFluentValidationAutoValidation(x => x.DisableDataAnnotationsValidation = true);
    }

    internal static void AddCaptcha(
        this IServiceCollection services)
    {
        //customize captcha here https://github.com/aliasadidev/SixLaborsCaptcha#change 
        services.AddSixLabCaptcha(x =>
        {
            x.FontSize = 30;
            x.FontStyle = SixLabors.Fonts.FontStyle.Bold;
            x.DrawLines = 2;
            x.NoiseRate = 100;
            x.Width = 300;
            x.MaxRotationDegrees = 0;
        });
    }
}