using DDDNerdStore.Core.DomainObjects;

namespace DDDNerdStore.Catalogo.Domain.Events;

public class ProdutoAbaixoEstoqueEvent : DomainEvent
{
    public int QuantidadeRestante { get; set; }

    public ProdutoAbaixoEstoqueEvent(Guid aggregateId, int quantidadeRestante) : base(aggregateId)
    {
        QuantidadeRestante = quantidadeRestante;
    }
}