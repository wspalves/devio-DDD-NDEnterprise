using MediatR;

namespace DDDNerdStore.Core.Messages.CommonMessages.Notifications;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
    private List<DomainNotification> _notifications;

    public DomainNotificationHandler()
    {
        _notifications = new List<DomainNotification>();
    }

    public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
    {
        _notifications.Add(notification);
        return Task.CompletedTask;
    }

    public virtual List<DomainNotification> ObterNotificacoes() => _notifications;

    public virtual bool TemNotificacoes() => _notifications.Any();

    public void Dispose()
    {
        _notifications = new List<DomainNotification>();
    }
}