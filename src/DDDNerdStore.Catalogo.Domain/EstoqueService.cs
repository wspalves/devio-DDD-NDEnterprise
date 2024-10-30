using DDDNerdStore.Catalogo.Domain.Events;
using DDDNerdStore.Core.Bus;
using MediatR;

namespace DDDNerdStore.Catalogo.Domain;

public class EstoqueService : IEstoqueService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMediatrHandler _mediator;

    public EstoqueService(IProdutoRepository produtoRepository, IMediatrHandler mediator)
    {
        _produtoRepository = produtoRepository;
        _mediator = mediator;
    }

    public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
    {
        var produto = await _produtoRepository.ObterProdutoPorIdAsync(produtoId);
        if (produto == null) return false;
        if (!produto.PossuiEstoque(quantidade)) return false;

        produto.DebitarEstoque(quantidade);

        if (produto.QuantidadeEstoque < 10)
            await _mediator.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produtoId, produto.QuantidadeEstoque));

        _produtoRepository.Atualizar(produto);
        return await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
    {
        var produto = await _produtoRepository.ObterProdutoPorIdAsync(produtoId);
        if (produto == null) return false;

        produto.ReporEstoque(quantidade);

        _produtoRepository.Atualizar(produto);
        return await _produtoRepository.UnitOfWork.Commit();
    }

    public void Dispose()
    {
        _produtoRepository.Dispose();
    }
}