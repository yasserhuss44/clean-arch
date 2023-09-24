using Application;
using Core.Base;
using Infrastructure.Persisitence.Db;
using Infrastructure.Persistence.Db;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddIntegration();

builder.Services.AddCore();

builder.Services.AutoRegisterBusinessServices();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure DbContext with SQL Server
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

builder.Services.AddScoped(typeof(ISchoolUnitOfWork<>), typeof(SchoolUnitOfWork<>));
 
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

//builder.Services.AddScoped(typeof(DbContext), typeof(SchoolDbContext));

 
//services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));


builder.Services.AddWeb(builder.Configuration);

builder.Host.AddSerilog(args);

var app = builder.Build();

app.Configure(builder.Configuration);

return app.RunWebApp();