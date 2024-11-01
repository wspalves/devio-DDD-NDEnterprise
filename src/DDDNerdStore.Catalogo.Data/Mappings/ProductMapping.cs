using DDDNerdStore.Catalogo.Domain;
using DDDNerdStore.Catalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNerdStore.Catalogo.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnType("varchar(500)");

        builder.Property(x => x.Imagem)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.OwnsOne(c => c.Dimensoes, cm =>
        {
            cm.Property(c => c.Altura)
                .HasColumnName("Altura")
                .HasColumnType("int");

            cm.Property(c => c.Largura)
                .HasColumnName("Largura")
                .HasColumnType("int");

            cm.Property(c => c.Profundidade)
                .HasColumnName("Profundidade")
                .HasColumnType("int");
        });

        builder.ToTable("Produtos");
    }
}