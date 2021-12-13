using System.Reflection;
using Core.Domain;
using Core.SeedWork;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class Context : DbContext
{
    private readonly DbContextOptions<Context> contextOptions;

    public Context(DbContextOptions<Context> contextOptions) : base(contextOptions)
    {
        this.contextOptions = contextOptions;
    }
    public DbSet<Trainer> Trainer { get; set; }
    public DbSet<Training> Training { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyDateTimeConverters();
    }

    public override int SaveChanges()
    {
        RemoveEnumerationsFromTracker();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        RemoveEnumerationsFromTracker();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public void Seed()
    {
        Trainer.Add(new Trainer("Victor"));
        Trainer.Add(new Trainer("Maxime"));
    }

    private void RemoveEnumerationsFromTracker()
    {
        ChangeTracker.Entries()
                     .Where(ee => ee.Entity is Enumeration)
                     .ToList()
                     .ForEach(ee => ee.State = EntityState.Detached);
    }
}
