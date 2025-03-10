using CrayonCloudSales.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CrayonCloudSales.Infrastructure.Data
{
    public class CloudSalesDbContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<SoftwareLicense> SoftwareLicenses { get; set; }

        public CloudSalesDbContext(DbContextOptions<CloudSalesDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Accounts)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.SoftwareLicenses)
                .WithOne(l => l.Account)
                .HasForeignKey(l => l.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                    new Customer { Id = 1, Name = "Contoso Ltd", Email = "admin@contoso.com", IsActive = true },
                    new Customer { Id = 2, Name = "Fabrikam Inc", Email = "admin@fabrikam.com", IsActive = true }
                    );

            modelBuilder.Entity<Account>().HasData(
                    new Account { Id = 1, CustomerId = 1, Name = "Contoso Main Account", CreatedDate = new DateTime(2023, 12, 3), IsActive = true },
                    new Account { Id = 2, CustomerId = 1, Name = "Contoso Development", CreatedDate = new DateTime(2024, 3, 2), IsActive = true },
                    new Account { Id = 3, CustomerId = 2, Name = "Fabrikam Main Account", CreatedDate = new DateTime(2021, 3, 5), IsActive = true }
                    );

            modelBuilder.Entity<SoftwareLicense>().HasData(
                    new SoftwareLicense
                    {
                        Id = 1,
                        AccountId = 1,
                        SoftwareServiceId = 1,
                        Quantity = 10,
                        State = LicenseState.Active,
                        PurchaseDate = new DateTime(2024, 4, 12),
                        ValidToDate = new DateTime(2025, 2, 1),
                        CcpSubscriptionId = "SUB-001"
                    },
                    new SoftwareLicense
                    {
                        Id = 2,
                        AccountId = 2,
                        SoftwareServiceId = 2,
                        Quantity = 5,
                        State = LicenseState.Active,
                        PurchaseDate = new DateTime(2024, 6, 12),
                        ValidToDate = new DateTime(2025, 1, 12),
                        CcpSubscriptionId = "SUB-002"
                    }
            );
        }
    }
}
