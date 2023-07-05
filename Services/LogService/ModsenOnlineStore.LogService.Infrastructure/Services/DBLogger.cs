using Microsoft.Extensions.Logging;
using ModsenOnlineStore.LogService.Application.Interfaces;
using ModsenOnlineStore.LogService.Domain.Entities;

namespace ModsenOnlineStore.LogService.Infrastructure.Services
{
    public class DBLogger : ILogger
    {

        private readonly ILogRepository repository;

        public DBLogger(ILogRepository repository)
        {
            this.repository = repository;
        }

        public void Log<TState>(
            LogLevel logLevel, EventId eventId,
            TState context, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            repository.AddLogAsync(new Log() 
            {
                LogLevel = logLevel.ToString(),
                EventId = Convert.ToInt32(eventId.Id),
                DateTime = DateTime.Now,
                Message = formatter(context, exception),
            });
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
