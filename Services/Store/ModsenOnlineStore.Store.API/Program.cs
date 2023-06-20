using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Application.Services;
using ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;
using ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;
using ModsenOnlineStore.Store.Application.Services.OrderProductServices;
using ModsenOnlineStore.Store.Application.Services.ProductTypeServices;
using ModsenOnlineStore.Store.Infrastructure.Data;
using ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces;
using ModsenOnlineStore.Store.Application.Services.CommentServices;
using ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces;
using ModsenOnlineStore.Store.Application.Services.ProductServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICouponService, CouponService>();
builder.Services.AddTransient<ICouponRepository, CouponRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderProductService, OrderProductService>();
builder.Services.AddTransient<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddTransient<IProductTypeService, ProductTypeService>();
builder.Services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var a = builder.Configuration.GetConnectionString("DefaultConnection");
var b = builder.Configuration.GetSection("MigrationsAssembly").Get<string>();

builder.Services.AddDbContext<DataContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly(builder.Configuration.GetSection("MigrationsAssembly").Get<string>())));


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
}

app.UseHttpsRedirection();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();

    await DbInitializer.SeedData(context);
}

app.MapControllers();

app.Run();
