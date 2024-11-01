using DDDNerdStore.Core.Communication.Mediator;
using DDDNerdStore.Core.Messages;
using DDDNerdStore.Core.Messages.CommonMessages.Notifications;
using DDDNerdStore.Vendas.Domain;
using DDDNerdStore.Vendas.Domain.Interfaces;
using MediatR;

namespace DDDNerdStore.Vendas.Application.Commands;

public class PedidoCommandHandler : IRequestHandler<AdicionarItemPedidoCommand, bool>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMediatorHandler _mediator;

    public PedidoCommandHandler(IPedidoRepository pedidoRepository, IMediatorHandler mediator)
    {
        _pedidoRepository = pedidoRepository;
        _mediator = mediator;
    }

    public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
    {
        if (!ValidarComando(message))
            return false;

        var pedido = await _pedidoRepository.ObterPedidoRascunhoPorIdAsync(message.ClienteId);
        var pedidoItem = new PedidoItem(message.ProdutoId, message.Nome, message.Quantidade, message.ValorUnitario);

        if (pedido == null)
        {
            pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.ClienteId);
            pedido.AdicionarItem(pedidoItem);

            _pedidoRepository.AdicionarPedido(pedido);
        }
        else
        {
            var pedidoExistente = pedido.PedidoItemExistente(pedidoItem);
            pedido.AdicionarItem(pedidoItem);

            if (pedidoExistente)
            {
                _pedidoRepository.AtualizarItem(
                    pedido.PedidoItems.FirstOrDefault(x => x.ProdutoId == pedidoItem.ProdutoId));
            }
            else
            {
                _pedidoRepository.AtualizarItem(pedidoItem);
            }
        }

        return await _pedidoRepository.UnitOfWork.Commit();
    }

    private bool ValidarComando(Command message)
    {
        if (message.EhValido()) return true;

        foreach (var error in message.ValidationResult.Errors)
        {
            _mediator.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
        }

        return false;
    }
}