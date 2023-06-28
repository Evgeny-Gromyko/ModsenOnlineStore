using ModsenOnlineStore.EmailAuthentication.Application.Interfaces;
using ModsenOnlineStore.EmailAuthentication.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IEmailSendingService, EmailSendingService>();
builder.Services.AddTransient<IVerificationCodeGeneratior, VerificationCodeGeneratior>();
builder.Services.AddHostedService<RabbitMQBackgroundConsumerService>();


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
