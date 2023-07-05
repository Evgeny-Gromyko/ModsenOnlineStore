using ModsenOnlineStore.Common;
using ModsenOnlineStore.LogService.Domain.Entities;

namespace ModsenOnlineStore.LogService.Application.Interfaces
{
    public interface ILogService
    {
        public Task<DataResponseInfo<List<Log>>> GetAllLogsAsync();

        public Task<DataResponseInfo<Log>> GetLogByIdAsync(int id);
    }
}
