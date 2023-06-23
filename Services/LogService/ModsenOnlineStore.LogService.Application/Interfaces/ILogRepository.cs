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
        public void AddLog(Log log);
        public Task<Log> GetLogById(int id);
        public Task<List<Log>> GetAllLogs();

    }
}
