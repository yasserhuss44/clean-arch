using School.Infrastructure.Integration.Transport;

Assembly[] assemblies = { typeof(StudentService).Assembly, typeof(DriverService).Assembly, typeof(TransportProxy).Assembly };

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddInfrastructure();

builder.Services.AddCore();

builder.Services.AddSharedWebServices(builder.Configuration);

builder.Services.AddWeb(assemblies);

builder.Host.AddConfigurations(args);

builder.Host.AddSerilog();

var app = builder.Build();

app.ConfigureSharedWeb(builder.Configuration);

return app.RunWebApp();