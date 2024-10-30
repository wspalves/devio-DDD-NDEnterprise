using DDDNerdStore.Catalogo.Application.DTOs;
using DDDNerdStore.Catalogo.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DDDNerdStore.WebApp.MVC.Controllers.Admin;

public class AdminProdutosController : Controller
{
    private readonly IProdutoAppService _produtoAppService;

    public AdminProdutosController(IProdutoAppService produtoAppService)
    {
        _produtoAppService = produtoAppService;
    }

    [HttpGet]
    [Route("admin-produtos")]
    public async Task<IActionResult> Index()
    {
        return View(await _produtoAppService.ObterTodos());
    }

    [HttpGet]
    [Route("novo-produto")]
    public async Task<IActionResult> NovoProduto()
    {
        return View(await PopularCategorias(new ProdutoDTO()));
    }

    [HttpPost]
    [Route("novo-produto")]
    public async Task<IActionResult> NovoProduto(ProdutoDTO produtoDto)
    {
        if (!ModelState.IsValid)
            return View(await PopularCategorias(produtoDto));

        await _produtoAppService.Adicionar(produtoDto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("editar-produto")]
    public async Task<IActionResult> AtualizarProduto(Guid id)
    {
        return View(await PopularCategorias(await _produtoAppService.ObterPorId(id)));
    }

    [HttpPost]
    [Route("editar-produto")] 
    public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoDTO produtoDto)
    {
        ModelState.Remove("QuantidadeEstoque");
        ModelState.Remove("Categorias");
        if (!ModelState.IsValid)
            return View(await PopularCategorias(produtoDto));
        
        var produto = await _produtoAppService.ObterPorId(id);
        produtoDto.QuantidadeEstoque = produto.QuantidadeEstoque;
        
        await _produtoAppService.Atualizar(produtoDto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("produtos-atualizar-estoque")]
    public async Task<IActionResult> AtualizarEstoque(Guid id)
    {
        return View("Estoque", await _produtoAppService.ObterPorId(id));
    }

    [HttpPost]
    [Route("produtos-atualizar-estoque")]
    public async Task<IActionResult> AtualizarEstoque(Guid id, int quantidade)
    {
        if (quantidade > 0)
            await _produtoAppService.ReporEstoque(id, quantidade);
        else
            await _produtoAppService.DebitarEstoque(id, quantidade);

        return View("Index", await _produtoAppService.ObterTodos());
    }

    private async Task<ProdutoDTO> PopularCategorias(ProdutoDTO produtoDto)
    {
        produtoDto.Categorias = await _produtoAppService.ObterCategorias();
        return produtoDto;
    }
}