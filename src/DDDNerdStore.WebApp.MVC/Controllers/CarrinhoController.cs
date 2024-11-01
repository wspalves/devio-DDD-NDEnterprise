using DDDNerdStore.Catalogo.Application.Services;
using DDDNerdStore.Core.Communication.Mediator;
using DDDNerdStore.Core.Messages.CommonMessages.Notifications;
using DDDNerdStore.Vendas.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDNerdStore.WebApp.MVC.Controllers;

public class CarrinhoController : ControllerBase
{
    private readonly IProdutoAppService _produtoAppService;
    private readonly IMediatorHandler _mediatorHandler;

    public CarrinhoController(INotificationHandler<DomainNotification> notificationHandler,
        IProdutoAppService produtoAppService, IMediatorHandler mediatorHandler) : base(notificationHandler,
        mediatorHandler)
    {
        _produtoAppService = produtoAppService;
        _mediatorHandler = mediatorHandler;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("meu-carrinho")]
    public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
    {
        var produto = await _produtoAppService.ObterPorId(id);
        if (produto == null)
            return BadRequest();

        if (produto.QuantidadeEstoque < quantidade)
        {
            TempData["Erro"] = "Produto com estoque insuficiente";
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }

        var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
        await _mediatorHandler.EnviarComando(command);

        if (OperacaoValida())
            return RedirectToAction("Index");
        
        

        TempData["Erros"] = ObterMensagemErro();
        return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
    }
}