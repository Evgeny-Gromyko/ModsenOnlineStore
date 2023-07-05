using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Login.Domain.Entities;

namespace ModsenOnlineStore.Login.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(p => p.Money).HasPrecision(18, 4);
        }
    }
}
