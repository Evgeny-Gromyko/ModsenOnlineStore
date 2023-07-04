using ModsenOnlineStore.Common;
using ModsenOnlineStore.LogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.LogService.Application.Interfaces
{
    public interface ILogService
    {
        public Task<DataResponseInfo<List<Log>>> GetAllLogsAsync();

        public Task<DataResponseInfo<Log>> GetLogByIdAsync(int id);
    }
}
