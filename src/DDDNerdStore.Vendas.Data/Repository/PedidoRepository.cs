using DDDNerdStore.Core.Data;
using DDDNerdStore.Vendas.Domain;
using DDDNerdStore.Vendas.Domain.Enums;
using DDDNerdStore.Vendas.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DDDNerdStore.Vendas.Data.Repository;

public class PedidoRepository : IPedidoRepository
{
    private readonly VendasContext _context;

    public PedidoRepository(VendasContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Pedido> ObterPedidoPorIdAsync(Guid pedidoId)
    {
        return await _context.Pedidos.FindAsync(pedidoId);
    }

    public async Task<IEnumerable<Pedido>> ObterListaPedidosPorClienteIdAsync(Guid clienteId)
    {
        return await _context.Pedidos.AsNoTracking().Where(p => p.ClienteId == clienteId).ToListAsync();
    }

    public async Task<Pedido> ObterPedidoRascunhoPorIdAsync(Guid pedidoId)
    {
        var pedido = await _context.Pedidos.FirstOrDefaultAsync(p =>
            p.PedidoStatus == PedidoStatus.Rascunho && p.Id == pedidoId);

        if (pedido == null)
            return null;

        await _context.Entry(pedido).Collection(i => i.PedidoItems).LoadAsync();

        if (pedido.VoucherId != null)
            await _context.Entry(pedido).Reference(i => i.Voucher).LoadAsync();

        return pedido;
    }

    public void AdicionarPedido(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
    }

    public void AtualizarPedido(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
    }

    public async Task<PedidoItem> ObterPedidoItemPorIdAsync(Guid pedidoItemId)
    {
        return await _context.PedidoItems.FindAsync(pedidoItemId);
    }

    public async Task<PedidoItem> ObterPedidoItemPorPedidoIdAsync(Guid pedidoId, Guid produtoId)
    {
        return await _context.PedidoItems.FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.PedidoId == pedidoId);
    }

    public void AdicionarItem(PedidoItem pedidoItem)
    {
        _context.PedidoItems.Add(pedidoItem);
    }

    public void AtualizarItem(PedidoItem pedidoItem)
    {
        _context.PedidoItems.Update(pedidoItem);
    }

    public void removerItem(PedidoItem pedidoItem)
    {
        _context.PedidoItems.Remove(pedidoItem);
    }

    public async Task<Voucher> ObterVoucherPorCodigoAsync(string codigo)
    {
        return await _context.Vouchers.FirstOrDefaultAsync(x => x.Codigo == codigo);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}