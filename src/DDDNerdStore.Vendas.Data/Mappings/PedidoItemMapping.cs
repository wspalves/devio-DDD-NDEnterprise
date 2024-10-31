using DDDNerdStore.Vendas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;

namespace DDDNerdStore.Vendas.Data.Mappings;

public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
    }

    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProdutoNome)
            .IsRequired()
            .HasColumnType("varchar(200)");

        // N : 1 => PedidoItems : Pedido
        builder.HasOne(c => c.Pedido)
            .WithMany(p => p.PedidoItems);

        builder.ToTable("PedidoItens");
    }
}