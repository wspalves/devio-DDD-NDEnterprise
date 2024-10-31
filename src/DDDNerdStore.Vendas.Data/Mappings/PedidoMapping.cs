using DDDNerdStore.Vendas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNerdStore.Vendas.Data.Mappings;

public class PedidoMapping : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(c => c.Codigo)
            .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");
        
        builder.Property(x => x.PedidoStatus)
            .IsRequired()
            .HasColumnType("tinyint");

        // 1 : N => Pedido : PedidoItems
        builder.HasMany(c => c.PedidoItems)
            .WithOne(p => p.Pedido)
            .HasForeignKey(p => p.PedidoId);

        builder.ToTable("Pedidos");
    }
}