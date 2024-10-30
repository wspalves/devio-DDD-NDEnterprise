using DDDNerdStore.Catalogo.Application.DTOs;

namespace DDDNerdStore.Catalogo.Application.Services;

public interface IProdutoAppService : IDisposable
{
    Task<IEnumerable<ProdutoDTO>> ObterPorCategoria(int codigo);
    Task<ProdutoDTO> ObterPorId(Guid id);
    Task<IEnumerable<ProdutoDTO>> ObterTodos();
    Task<IEnumerable<CategoriaDTO>> ObterCategorias();

    Task Adicionar(ProdutoDTO produto);
    Task Atualizar(ProdutoDTO produto);

    Task<ProdutoDTO> DebitarEstoque(Guid id, int quantidade);
    Task<ProdutoDTO> ReporEstoque(Guid id, int quantidade);
}