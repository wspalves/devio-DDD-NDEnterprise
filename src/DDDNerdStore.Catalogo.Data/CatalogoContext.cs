using DDDNerdStore.Catalogo.Domain;
using DDDNerdStore.Catalogo.Domain.Entities;
using DDDNerdStore.Core.Data;
using DDDNerdStore.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace DDDNerdStore.Catalogo.Data;

public class CatalogoContext(DbContextOptions<CatalogoContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries()
                     .Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataCadastro").IsModified = false;
            }
        }

        var saved = await SaveChangesAsync();
        return saved > 0;
    }
}