using DDDNerdStore.Core.DomainObjects;
using DDDNerdStore.Vendas.Domain.Enums;

namespace DDDNerdStore.Vendas.Domain;

public class Voucher : Entity
{
    protected Voucher()
    {
    }

    public string Codigo { get; private set; }

    public decimal? Percentual { get; private set; }

    public decimal? ValorDesconto { get; private set; }

    public int Quantidade { get; private set; }

    public TipoDescontoVoucher TipoDescontoVoucher { get; private set; }

    public DateTime DataCriacao { get; private set; }

    public DateTime? DataUtilizacao { get; private set; }

    public DateTime DataValidade { get; private set; }

    public bool Ativo { get; private set; }

    public bool Utilizado { get; private set; }

    public ICollection<Pedido> Pedidos { get; set; }
}