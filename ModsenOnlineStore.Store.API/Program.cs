using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Application.Services;
using ModsenOnlineStore.Store.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICouponService, CouponService>();
builder.Services.AddTransient<ICouponRepository, CouponRepository>();

var a = builder.Configuration.GetConnectionString("DefaultConnection");
var b = builder.Configuration.GetSection("MigrationsAssembly").Get<string>();

builder.Services.AddDbContext<DataContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly(builder.Configuration.GetSection("MigrationsAssembly").Get<string>())));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers();

var authOptions = builder.Configuration.GetSection("Auth").Get<AuthOptions>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await DbInitializer.SeedData(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
