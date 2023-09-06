using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.Entities;

namespace Nordax.Bank.Recruitment.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Subscription>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<LoanApplication>().HasOne(c => c.UploadedFile).WithOne(a => a.LoanApplication);
        }
    }
}