using Microsoft.EntityFrameworkCore;
using PensionSystem.Domain.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PensionSystem.Infrastructure.Data;

public class PensionDbContext : DbContext
{
    public PensionDbContext(DbContextOptions<PensionDbContext> options) : base(options)
    {
        
    }
    public DbSet<Member> Members { get; set; }
    public DbSet<Contribution> Contributions { get; set; }
    public DbSet<Employer> Employers { get; set; }
    public DbSet<Benefit> Benefits { get; set; }
    public DbSet<TransactionHistory> TransactionHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            b.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            b.Property(x => x.Email).IsRequired().HasMaxLength(200);
            b.HasQueryFilter(m => !m.IsDeleted);
        });

        modelBuilder.Entity<Contribution>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Amount).HasColumnType("decimal(18,2)");
            b.Property(x => x.ReferenceNumber).IsRequired().HasMaxLength(100);
            b.HasIndex(x => new { x.MemberId, x.ContributionDate });
        });
        
        modelBuilder.Entity<Employer>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.CompanyName).IsRequired().HasMaxLength(100);
            b.Property(x => x.RegistrationNumber).IsRequired().HasMaxLength(100);
        });


        modelBuilder.Entity<Employer>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.CompanyName).IsRequired().HasMaxLength(100);
            b.Property(x => x.RegistrationNumber).IsRequired().HasMaxLength(100);
        });

        base.OnModelCreating(modelBuilder);
    }

 
}
