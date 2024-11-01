using DDDNerdStore.Core.Messages;
using DDDNerdStore.Core.Messages.CommonMessages.Notifications;

namespace DDDNerdStore.Core.Communication.Mediator;

public interface IMediatorHandler
{
    Task PublicarEvento<T>(T evento) where T : Event;
    Task<bool> EnviarComando<T>(T evento) where T : Command;
    Task PublicarNotificacao<T>(T notification) where T : DomainNotification;
}