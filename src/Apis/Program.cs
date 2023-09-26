

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddWeb(builder.Configuration);

builder.Host.AddSerilog(args);

var app = builder.Build();

app.Configure(builder.Configuration);

return app.RunWebApp();