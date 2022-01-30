using Domain.ProviderDetails;
using Domain.Providers;
using Domain.ProviderServices;
using Domain.Services;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class EFContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ProviderService> ProviderService { get; set; }
        public DbSet<ProviderDetail> ProviderDetail { get; set; }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.UserName)
                .IsRequired();
        }
        #endregion
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }
    }
}