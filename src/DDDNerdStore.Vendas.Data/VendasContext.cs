using DDDNerdStore.Core.Data;
using DDDNerdStore.Core.Messages;
using DDDNerdStore.Vendas.Domain;
using Microsoft.EntityFrameworkCore;

namespace DDDNerdStore.Vendas.Data;

public class VendasContext(DbContextOptions<VendasContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItems { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VendasContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }

        modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1000).IncrementsBy(1);
        
        base.OnModelCreating(modelBuilder);
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