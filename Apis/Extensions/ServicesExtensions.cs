using Application.Services.SystemFeatures;

namespace Apis.Extensions;

public static class ServicesExtensions
{
    //internal static void AddFeatureFlags(
    //    this IServiceCollection services)
    //    => services.AddTransient<FeatureFlagsMiddleware>();

    internal static void AddAutoMapperProfiles(
        this IServiceCollection services)
        => services.AddAutoMapper(typeof(StudentService), typeof(StudentMapping));

    internal static void AddSwagger(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "NDP API", Version = "v1" });

            c.AddServer(new OpenApiServer
            {
                Url = configuration.GetValue<string>(ConfigKeys.ApiHost)
            });

            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "ApiKey must appear in header",
                Type = SecuritySchemeType.ApiKey,
                Name = SecurityConstants.ApiKeyHeaderName,
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Scheme = "bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
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
                    }, Array.Empty<string>()
                },
                { 
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        },
                        In = ParameterLocation.Header
                    }, Array.Empty<string>() 
                }
            });

            c.CustomOperationIds(apiDesc =>
            {
                return $"{apiDesc.ActionDescriptor.RouteValues["controller"]}{apiDesc.ActionDescriptor.RouteValues["action"]}_{apiDesc.HttpMethod}";
            });

        });
    }

    internal static void AddHttpLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration.GetValue<bool>(ConfigKeys.IsAPILogEnabled)
                        .IsFalsy())
            return;

      //  services.AddSingleton<IHarLogger,HarLogger>(/*p => new HARLogger(configuration.GetValue<string>(ConfigKeys.LogPath))*/);

        services.AddTransient<AutoLogMiddleWare>();
    }

    internal static void AddCorsPolicy(
        this IServiceCollection services,
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

      var x=   AssemblyScanner
        .FindValidatorsInAssembly(typeof(StudentService).Assembly, true);

        services.AddValidatorsFromAssemblyContaining<StudentService>();

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

    internal static void AddApiKey(
        this IServiceCollection services)
        => services.AddTransient<ApiKeyMiddleware>();

    internal static void AddSanitizerMiddleware(
        this IServiceCollection services)
        => services.AddTransient<SanitizerMiddleware>();

    internal static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
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
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                ValidateLifetime = true
            };
        });

        return services;
    }

    internal static void AddWebServices(
        this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<StudentService>()
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

    //internal static void AddAuthorizationPolicies(
    //    this AuthorizationOptions options)
    //{
    //    var type = typeof(IAuthorizationRequirementConfig);

    //    var types = AppDomain.CurrentDomain.GetAssemblies()
    //        .SelectMany(s => s.GetTypes())
    //        .Where(p => type.IsAssignableFrom(p));

    //    foreach (var currentType in types.Where(t => t.Name != nameof(IAuthorizationRequirementConfig)))
    //    {
    //        var requirement = (IAuthorizationRequirementConfig)Activator.CreateInstance(currentType);

    //        options.AddPolicy(requirement.PolicyName, policy =>
    //        {
    //            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);

    //            policy.RequireAuthenticatedUser();

    //            policy.Requirements.Add(new AuthorizationMatrixRequirement(
    //                requirement.RequestType.ToInt(),
    //                requirement.AuthorizationAccessLevel.ToInt(),
    //                requirement.IsAllowedForExternal));
    //        });
    //    }
    //}

    internal static void AddResponseCompressionProviders(
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

    internal static void AddExceptionMiddleware(
        this IServiceCollection services)
        => services.AddTransient<ExceptionMiddleware>();


    public static void AutoRegisterBusinessServices(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<StudentService>()
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
