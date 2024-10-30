using AutoMapper;
using DDDNerdStore.Catalogo.Application.DTOs;
using DDDNerdStore.Catalogo.Domain;
using DDDNerdStore.Core.DomainObjects;

namespace DDDNerdStore.Catalogo.Application.Services;

public class ProdutoAppService : IProdutoAppService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;
    private readonly IEstoqueService _estoqueService;

    public ProdutoAppService(IProdutoRepository produtoRepository, IMapper mapper, IEstoqueService estoqueService)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
        _estoqueService = estoqueService;
    }

    public async Task<IEnumerable<ProdutoDTO>> ObterPorCategoria(int codigo)
    {
        return _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtoRepository.ObterProdutosPorCategoriaAsync(codigo));
    }

    public async Task<ProdutoDTO> ObterPorId(Guid id)
    {
        return _mapper.Map<ProdutoDTO>(await _produtoRepository.ObterProdutoPorIdAsync(id));
    }

    public async Task<IEnumerable<ProdutoDTO>> ObterTodos()
    {
        return _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtoRepository.ObterTodosAsync());
    }

    public async Task<IEnumerable<CategoriaDTO>> ObterCategorias()
    {
        return _mapper.Map<IEnumerable<CategoriaDTO>>(await _produtoRepository.ObterCategoriasAsync());
    }

    public async Task Adicionar(ProdutoDTO produto)
    {
        _produtoRepository.Adicionar(_mapper.Map<Produto>(produto));
        await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task Atualizar(ProdutoDTO produto)
    {
        _produtoRepository.Atualizar(_mapper.Map<Produto>(produto));
        await _produtoRepository.UnitOfWork.Commit();
    }

    public async Task<ProdutoDTO> DebitarEstoque(Guid id, int quantidade)
    {
        var result = await _estoqueService.DebitarEstoque(id, quantidade);
        if (!result)
            throw new DomainException("Falha ao debitar estoque.");

        return _mapper.Map<ProdutoDTO>(await _produtoRepository.ObterProdutoPorIdAsync(id));
    }

    public async Task<ProdutoDTO> ReporEstoque(Guid id, int quantidade)
    {
        var result = await _estoqueService.ReporEstoque(id, quantidade);
        if (!result)
            throw new DomainException("Falha ao repor estoque.");

        return _mapper.Map<ProdutoDTO>(await _produtoRepository.ObterProdutoPorIdAsync(id));
    }

    public void Dispose()
    {
        _produtoRepository?.Dispose();
        _estoqueService?.Dispose();
    }
}