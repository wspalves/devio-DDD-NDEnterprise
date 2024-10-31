namespace DDDNerdStore.Vendas.Domain;

public class PedidoItem : Produto
{
    protected PedidoItem()
    {
    }

    public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
    {
        ProdutoId = produtoId;
        ProdutoNome = produtoNome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    public Guid PedidoId { get; private set; }

    public int Quantidade { get; private set; }

    public decimal ValorUnitario { get; private set; }


    public Pedido Pedido { get; set; }

    internal void AssociarPedido(Guid pedidoId) => PedidoId = pedidoId;
    public decimal CalcularValor() => ValorUnitario * Quantidade;
    internal void AdicionarUnidades(int unidade) => Quantidade += unidade;
    internal void AtualizarUnidades(int unidade) => Quantidade = unidade;
    
    public override bool EhValido()
    {
        return true;
    }
}