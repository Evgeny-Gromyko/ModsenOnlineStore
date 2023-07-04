using ModsenOnlineStore.LogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.LogService.Application.Interfaces
{
    public interface ILogRepository
    {
        public Task AddLogAsync(Log log);
        public Task<Log> GetLogByIdAsync(int id);
        public Task<List<Log>> GetAllLogsAsync();

    }
}
