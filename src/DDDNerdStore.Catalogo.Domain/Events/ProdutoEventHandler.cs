using MediatR;

namespace DDDNerdStore.Catalogo.Domain.Events;

public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoEventHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task Handle(ProdutoAbaixoEstoqueEvent notification, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.ObterProdutoPorIdAsync(notification.AggregateId);

        //Envia e-mail
        //Dispara outro evento...
    }
}