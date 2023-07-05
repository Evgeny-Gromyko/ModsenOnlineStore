using ModsenOnlineStore.Common;
using ModsenOnlineStore.LogService.Application.Interfaces;
using ModsenOnlineStore.LogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.LogService.Infrastructure.Services
{
    public class LogServise : ILogService
    {
        private readonly ILogRepository repository;
        public LogServise(ILogRepository repository) 
        {
            this.repository = repository;
        }

        public async Task<DataResponseInfo<List<Log>>> GetAllLogsAsync()
        {
            var logs = await repository.GetAllLogsAsync();
            if (logs.Count == 0)
            {
                return new DataResponseInfo<List<Log>> (data: null, success: false, message: "still no logs");
            }
            return new DataResponseInfo<List<Log>>(data: logs, success: true, message: "logs");
        }

        public async Task<DataResponseInfo<Log>> GetLogByIdAsync(int id)
        {
            var log = await repository.GetLogByIdAsync(id);
            if (log is null)
            {
                return new DataResponseInfo<Log>(data: null, success: false, message: "no such log");
            }
            return new DataResponseInfo<Log>(data: log, success: true, message: "log");
        }
    }
}
