using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModsenOnlineStore.LogService.Application.Interfaces;
using System;

namespace ModsenOnlineStore.LogService.Infrastructure.Services
{
    public class DBLoggerProvider : ILoggerProvider
    {
        private readonly ILogRepository repository;
        private readonly IServiceProvider serviceProvider;
        public DBLoggerProvider(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<ILogRepository>();
        }

        //public DBLoggerProvider(ILogRepository repository)
        //{
        //    this.repository = repository;
        //}

        //categoryName is unnecessary but required by ILoggerProvider parameter
        public ILogger CreateLogger(string categoryName) 
        {
            return new DBLogger(serviceProvider); // repository
        }

        public void Dispose() { }
    }

    
}

