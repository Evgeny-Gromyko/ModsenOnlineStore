using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Common.Interfaces;
using ModsenOnlineStore.Common.Services;
using ModsenOnlineStore.EmailAuthentication.Infrastructure.Services;
using ModsenOnlineStore.Login.Application.Interfaces;
using ModsenOnlineStore.Login.Application.Services;
using ModsenOnlineStore.Login.Domain.DTOs.UserDTOs;
using ModsenOnlineStore.Login.Domain.Validators.UserValidators;
using ModsenOnlineStore.Login.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IUserMoneyService, UserMoneyService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddTransient<IEmailConfirmationRepository, EmailConfirmationRepository>();
builder.Services.AddTransient<IRabbitMQMessagingService, RabbitMQMessagingService>();

builder.Services.AddTransient<IValidator<AddUserDTO>, AddUserValidator>();
builder.Services.AddTransient<IValidator<UpdateUserDTO>, UpdateUserValidator>();

builder.Services.AddHostedService<RabbitMQBackgroundConsumerService>();


builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));

var a = builder.Configuration.GetConnectionString("DefaultConnection");
var b = builder.Configuration.GetSection("MigrationsAssembly").Get<string>();

builder.Services.AddDbContext<DataContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly(builder.Configuration.GetSection("MigrationsAssembly").Get<string>())));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

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



builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await DbInitializer.SeedData(context);
}

app.MapControllers();

app.Run();
