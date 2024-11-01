using DDDNerdStore.Core.Communication.Mediator;
using DDDNerdStore.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDNerdStore.WebApp.MVC.Controllers;

public abstract class ControllerBase : Controller
{
    private readonly DomainNotificationHandler _notifications;
    private readonly IMediatorHandler _mediator;

    public ControllerBase(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator)
    {
        _notifications = (DomainNotificationHandler)notifications;
        _mediator = mediator;
    }

    protected Guid ClienteId = Guid.Parse("00000000-0000-0000-0000-000000000001");

    protected bool OperacaoValida()
    {
        return !_notifications.TemNotificacoes();
    }

    protected IEnumerable<string> ObterMensagemErro()
    {
        return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
    }

    protected void NotificarErro(string codigo, string mensagem)
    {
        _mediator.PublicarNotificacao(new DomainNotification(codigo, mensagem));
    }
}