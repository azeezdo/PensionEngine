using Microsoft.EntityFrameworkCore;
using PensionSystem.Domain.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PensionSystem.Infrastructure.Data;

public class PensionDbContext: DbContext
{
    public PensionDbContext(DbContextOptions<PensionDbContext> options) : base(options)
    {
        
    }

    public DbSet<Member> Members => Set<Member>();
    public DbSet<Contribution> Contributions => Set<Contribution>();
    public DbSet<Employer> Employers => Set<Employer>();
    public DbSet<Benefit> Benefits => Set<Benefit>();
    public DbSet<TransactionHistory> TransactionHistories => Set<TransactionHistory>();

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

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.UpdatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                var action = entry.State == EntityState.Added ? "Created" : entry.State == EntityState.Modified ? "Updated" : "Deleted";
                var json = JsonSerializer.Serialize(entry);
                var hist = new TransactionHistory(entry.Entity.Id, entry.Entity.GetType().Name, action, json);
                TransactionHistories.Add(hist);
            }
            
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
