using DDDNerdStore.Catalogo.Domain;
using DDDNerdStore.Catalogo.Domain.Entities;
using DDDNerdStore.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace DDDNerdStore.Catalogo.Data.Repository;

public class ProdutoRepository : IProdutoRepository
{
    private readonly CatalogoContext _context;

    public ProdutoRepository(CatalogoContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    public async Task<Produto> ObterProdutoPorIdAsync(Guid id)
    {
        return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(int codigo)
    {
        return await _context.Produtos.AsNoTracking().Include(p => p.Categoria).Where(c => c.Categoria.Codigo == codigo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Categoria>> ObterCategoriasAsync()
    {
        return await _context.Categorias.AsNoTracking().ToListAsync();
    }

    public void Adicionar(Produto produto)
    {
        _context.Produtos.Add(produto);
    }

    public void Atualizar(Produto produto)
    {
        _context.Produtos.Update(produto);
    }

    public void Adicionar(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
    }

    public void Atualizar(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}