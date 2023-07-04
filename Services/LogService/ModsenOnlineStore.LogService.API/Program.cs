using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModsenOnlineStore.LogService.Application.Interfaces;
using ModsenOnlineStore.LogService.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(builder.Configuration.GetSection("MigrationsAssembly").Get<string>())
    )
);

builder.Services.AddLogging();
builder.Logging.ClearProviders();

builder.Services.AddTransient<ILogRepository, LogRepository>();
builder.Services.AddTransient<ILogService, LogService>();

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
