using ModsenOnlineStore.LogService.Domain.Entities;

namespace ModsenOnlineStore.LogService.Application.Interfaces
{
    public interface ILogRepository
    {
        public Task AddLogAsync(Log log);
        public Task<Log> GetLogByIdAsync(int id);
        public Task<List<Log>> GetAllLogsAsync();

    }
}
