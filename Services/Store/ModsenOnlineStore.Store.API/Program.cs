using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.OrderProductInterfaces;
using ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;
using ModsenOnlineStore.Store.Application.Services.OrderProductServices;
using ModsenOnlineStore.Store.Application.Services.ProductTypeServices;
using ModsenOnlineStore.Store.Infrastructure.Data;
using ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces;
using ModsenOnlineStore.Store.Application.Interfaces.CouponInterfaces;
using ModsenOnlineStore.Store.Application.Services.CommentServices;
using ModsenOnlineStore.Store.Application.Interfaces.ProductInterfaces;
using ModsenOnlineStore.Store.Application.Services.CouponServices;
using ModsenOnlineStore.Store.Application.Services.ProductServices;
using ModsenOnlineStore.Store.Application.Services.OrderService;
using ModsenOnlineStore.Store.Application.Interfaces.OrderInterfaces;
using FluentValidation;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;
using FluentValidation.AspNetCore;
using ModsenOnlineStore.Store.Domain.Validators.ProductTypeValidators;
using ModsenOnlineStore.Store.Domain.DTOs.ProductDTOs;
using ModsenOnlineStore.Store.Domain.Validators.ProductValidators;
using ModsenOnlineStore.Store.Domain.DTOs.OrderDTOs;
using ModsenOnlineStore.Store.Domain.Validators.OrderValidators;
using ModsenOnlineStore.Store.Domain.DTOs.OrderProductDTOs;
using ModsenOnlineStore.Store.Domain.Validators.OrderProductValidators;
using ModsenOnlineStore.Store.Domain.DTOs.CouponDTOs;
using ModsenOnlineStore.Store.Domain.Validators.CouponValidators;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;
using ModsenOnlineStore.Store.Domain.Validators.CommentValidators;
using ModsenOnlineStore.Store.Application.Middleware;
using ModsenOnlineStore.Common.Interfaces;
using ModsenOnlineStore.Common.Services;
using ModsenOnlineStore.Store.Application.Interfaces.OrderPaymentConfirmationInterfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICouponService, CouponService>();
builder.Services.AddTransient<ICouponRepository, CouponRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderProductService, OrderProductService>();
builder.Services.AddTransient<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddTransient<IProductTypeService, ProductTypeService>();
builder.Services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IRabbitMQMessagingService, RabbitMQMessagingService>();
builder.Services.AddTransient<IOrderPaymentConfirmationRepository, OrderPaymentConfirmationRepository>();



builder.Services.AddTransient<IValidator<AddUpdateProductTypeDTO>, AddUpdateProductTypeValidator>();
builder.Services.AddTransient<IValidator<AddProductDTO>, AddProductValidator>();
builder.Services.AddTransient<IValidator<UpdateProductDTO>, UpdateProductValidator>();
builder.Services.AddTransient<IValidator<AddOrderDTO>, AddOrderValidator>();
builder.Services.AddTransient<IValidator<UpdateOrderDTO>, UpdateOrderValidator>();
builder.Services.AddTransient<IValidator<AddProductToOrderDTO>, AddProductToOrderValidator>();
builder.Services.AddTransient<IValidator<AddCouponDTO>, AddCouponValidator>();
builder.Services.AddTransient<IValidator<AddCommentDTO>, AddCommentValidator>();
builder.Services.AddTransient<IValidator<UpdateCommentDTO>, UpdateCommentValidator>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

var a = builder.Configuration.GetConnectionString("DefaultConnection");
var b = builder.Configuration.GetSection("MigrationsAssembly").Get<string>();

builder.Services.AddDbContext<DataContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly(builder.Configuration.GetSection("MigrationsAssembly").Get<string>())));


builder.Services.AddControllers();

var authOptions = builder.Configuration.GetSection("Auth").Get<AuthOptions>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = authOptions.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


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

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();

    // await DbInitializer.SeedData(context)
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
