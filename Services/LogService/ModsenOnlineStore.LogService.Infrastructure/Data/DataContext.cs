using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.LogService.Domain.Entities;

namespace ModsenOnlineStore.LogService.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<Log> Logs { get; set; }
        
    }
}
