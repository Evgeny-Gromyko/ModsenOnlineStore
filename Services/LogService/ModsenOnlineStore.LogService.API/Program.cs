using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModsenOnlineStore.LogService.Application.Interfaces;
using ModsenOnlineStore.LogService.Infrastructure.Data;
using ModsenOnlineStore.LogService.Infrastructure.Services;
using ModsenOnlineStore.LogService.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);


// not working, error "Some services are not able to be constructed"
//builder.Services.AddScoped<ILoggerProvider, DBLoggerProvider>(); 

//how to pass ILoggerProvider inside DBLoggerProvider?

//builder.Services.AddSingleton<DataContext>();
//builder.Services.AddSingleton<DbContextOptions>(); not Working
builder.Services.AddDbContext<DataContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly(builder.Configuration.GetSection("MigrationsAssembly").Get<string>())));

builder.Logging.Services.AddScoped<ILogger, DBLogger>(); // always Singleton
builder.Logging.Services.AddScoped<ILoggerFactory, LoggerFactory>();
builder.Logging.Services.AddScoped<ILoggerProvider, DBLoggerProvider>();
builder.Services.AddScoped<ILogRepository, LogRepository>();





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
