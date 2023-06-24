using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModsenOnlineStore.LogService.Application.Interfaces;
using ModsenOnlineStore.LogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.LogService.Infrastructure.Services
{
    public class DBLogger : ILogger
    {

        private readonly ILogRepository repository;

        public DBLogger(ILogRepository repository)
        {
            this.repository = repository;
        }
//        public DBLogger(IServiceProvider serviceProvider)
//        {
//            repository = serviceProvider.GetService<ILogRepository>();
//        }


        public void Log<TState>( // is it possible to make it async?
            LogLevel logLevel, EventId eventId,
            TState context, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            repository.AddLog(new Log() //TState - log creating context 
            {
                logLevel = logLevel.ToString(),
                eventId = Convert.ToInt32(eventId.Id),
                dateTime = DateTime.Now,
                message = formatter(context, exception),
            });
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable BeginScope<TState>(TState state) => null;

    }
}
