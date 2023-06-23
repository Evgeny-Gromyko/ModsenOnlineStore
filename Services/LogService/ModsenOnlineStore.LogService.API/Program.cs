using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModsenOnlineStore.LogService.Application.Interfaces;
using ModsenOnlineStore.LogService.Infrastructure.Data;
using ModsenOnlineStore.LogService.Infrastructure.Services;
using ModsenOnlineStore.LogService.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ILogRepository, LogRepository>();

// not working, error "Some services are not able to be constructed"
//builder.Services.AddScoped<ILoggerProvider, DBLoggerProvider>(); 

//how to pass ILoggerProvider inside DBLoggerProvider?
//builder.AddDBLogging(new DBLoggerProvider(repository));

//builder.AddProvider(new DBLoggerProvider(repository))

builder.Services.AddDbContext<DataContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly(builder.Configuration.GetSection("MigrationsAssembly").Get<string>())));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
