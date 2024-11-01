using DDDNerdStore.Catalogo.Domain.Entities;
using DDDNerdStore.Core.Data;

namespace DDDNerdStore.Catalogo.Domain;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> ObterTodosAsync();

    Task<Produto> ObterProdutoPorIdAsync(Guid id);

    Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(int categoriaId);

    Task<IEnumerable<Categoria>> ObterCategoriasAsync();

    void Adicionar(Produto produto);
    void Atualizar(Produto produto);
    void Adicionar(Categoria categoria);
    void Atualizar(Categoria categoria);
}