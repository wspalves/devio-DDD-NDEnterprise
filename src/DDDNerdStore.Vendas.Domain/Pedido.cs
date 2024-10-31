using DDDNerdStore.Core.DomainObjects;
using DDDNerdStore.Vendas.Domain.Enums;

namespace DDDNerdStore.Vendas.Domain;

public class Pedido : Entity, IAggregateRoot
{
    protected Pedido()
    {
        _pedidoItems = new List<PedidoItem>();
    }

    public Pedido(Guid clienteId, bool voucherUtilizado, decimal desconto, decimal valorTotal)
    {
        ClienteId = clienteId;
        VoucherUtilizado = voucherUtilizado;
        Desconto = desconto;
        ValorTotal = valorTotal;
    }

    public int Codigo { get; private set; }
    public Guid ClienteId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool VoucherUtilizado { get; private set; }
    public decimal Desconto { get; private set; }
    public decimal ValorTotal { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public PedidoStatus PedidoStatus { get; private set; }
    private readonly List<PedidoItem> _pedidoItems;
    public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;
    public Voucher Voucher { get; private set; }

    public void AplicarVoucher(Voucher voucher)
    {
        Voucher = voucher;
        VoucherUtilizado = true;
        CalcularValorPedido();
    }

    public void CalcularValorPedido()
    {
        ValorTotal = _pedidoItems.Sum(p => p.CalcularValor());
        CalcularValorTotalDesconto();
    }

    public void CalcularValorTotalDesconto()
    {
        if (!VoucherUtilizado) return;

        decimal desconto = 0;
        var valor = ValorTotal;

        if (Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Porcentagem)
        {
            if (Voucher.Percentual.HasValue)
            {
                desconto = (valor * Voucher.Percentual.Value) / 100;
                valor -= desconto;
            }
        }
        else
        {
            if (Voucher.ValorDesconto.HasValue)
            {
                desconto = Voucher.ValorDesconto.Value;
                valor -= desconto;
            }
        }

        ValorTotal = valor < 0 ? 0 : valor;
        Desconto = desconto;
    }

    public bool PedidoItemExistente(PedidoItem pedido) => _pedidoItems.Any(p => p.ProdutoId == pedido.ProdutoId);

    public void AdicionarItem(PedidoItem pedidoItem)
    {
        if (!pedidoItem.EhValido()) return;

        pedidoItem.AssociarPedido(Id);
        if (PedidoItemExistente(pedidoItem))
        {
            var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId);
            itemExistente.AdicionarUnidades(itemExistente.Quantidade);
            pedidoItem = itemExistente;

            _pedidoItems.Remove(pedidoItem);
        }

        pedidoItem.CalcularValor();
        _pedidoItems.Add(pedidoItem);

        CalcularValorPedido();
    }

    public void RemoverItem(PedidoItem pedidoItem)
    {
        if (!pedidoItem.EhValido()) return;

        var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId);

        if (itemExistente == null)
            throw new DomainException("Produto não existente no pedido.");

        _pedidoItems.Remove(itemExistente);

        CalcularValorPedido();
    }

    public void AtualizarItem(PedidoItem pedidoItem)
    {
        if (!pedidoItem.EhValido()) return;

        pedidoItem.AssociarPedido(Id);

        var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId);

        if (itemExistente == null)
            throw new DomainException("Produto não existente no pedido.");

        _pedidoItems.Remove(itemExistente);
        _pedidoItems.Add(pedidoItem);

        CalcularValorPedido();
    }

    public void AtualizarUnidades(PedidoItem pedidoItem, int unidades)
    {
        pedidoItem.AtualizarUnidades(unidades);
        AtualizarItem(pedidoItem);
    }

    public void TornarRascunho() => PedidoStatus = PedidoStatus.Rascunho;
    public void IniciarPedido() => PedidoStatus = PedidoStatus.Iniciado;
    public void FinalizarPedido() => PedidoStatus = PedidoStatus.Pago;
    public void CancelarPedido() => PedidoStatus = PedidoStatus.Cancelado;

    public static class PedidoFactory
    {
        public static Pedido NovoPedidoRascunho(Guid clienteId)
        {
            var pedido = new Pedido()
            {
                ClienteId = clienteId,
            };

            pedido.TornarRascunho();
            return pedido;
        }
    }
}