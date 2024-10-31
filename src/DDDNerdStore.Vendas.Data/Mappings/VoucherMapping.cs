using DDDNerdStore.Vendas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDNerdStore.Vendas.Data.Mappings;

public class VoucherMapping : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Codigo)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(x => x.TipoDescontoVoucher)
            .IsRequired()
            .HasColumnType("tinyint");

        // 1 : N => Voucher : Pedidos
        builder.HasMany(c => c.Pedidos)
            .WithOne(p => p.Voucher)
            .HasForeignKey(p => p.VoucherId);

        builder.ToTable("Vouchers");
    }
}