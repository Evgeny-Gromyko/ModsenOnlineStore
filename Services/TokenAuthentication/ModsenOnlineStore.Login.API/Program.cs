using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Login.Application.Interfaces;
using ModsenOnlineStore.Login.Infrastructure.Data;
using ModsenOnlineStore.Login.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddTransient<IEmailConfirmationRepository, EmailConfirmationRepository>();

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
builder.Services.AddSwaggerGen();

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
