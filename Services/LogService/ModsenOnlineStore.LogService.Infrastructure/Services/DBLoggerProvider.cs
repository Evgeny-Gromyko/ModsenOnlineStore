using Microsoft.Extensions.Logging;
using ModsenOnlineStore.LogService.Application.Interfaces;

namespace ModsenOnlineStore.LogService.Infrastructure.Services
{
    public class DBLoggerProvider : ILoggerProvider
    {
        private readonly ILogRepository repository;

        public DBLoggerProvider(ILogRepository repository)
        {
            this.repository = repository;
        }

        public ILogger CreateLogger(string categoryName) 
        {
            return new DBLogger(repository);
        }

        public void Dispose() { }
    }
}

