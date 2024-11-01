using DDDNerdStore.Core.Data;

namespace DDDNerdStore.Vendas.Domain.Interfaces;

public interface IPedidoRepository : IRepository<Pedido>
{
    Task<Pedido> ObterPedidoPorIdAsync(Guid pedidoId);
    Task<IEnumerable<Pedido>> ObterListaPedidosPorClienteIdAsync(Guid clienteId);
    Task<Pedido> ObterPedidoRascunhoPorClientIdAsync(Guid pedidoId);
    void AdicionarPedido(Pedido pedido);
    void AtualizarPedido(Pedido pedido);

    Task<PedidoItem> ObterPedidoItemPorIdAsync(Guid pedidoItemId);
    Task<PedidoItem> ObterPedidoItemPorPedidoIdAsync(Guid pedidoId, Guid produtoId);
    void AdicionarItem(PedidoItem pedidoItem);
    void AtualizarItem(PedidoItem pedidoItem);
    void removerItem(PedidoItem pedidoItem);

    Task<Voucher> ObterVoucherPorCodigoAsync(string codigo);
}