using DDDNerdStore.Core.DomainObjects;

namespace DDDNerdStore.Vendas.Domain;

public abstract class Produto : Entity
{
    protected Produto()
    {
    }

    public Guid ProdutoId { get; protected set; }
    public string ProdutoNome { get; protected set; }
}