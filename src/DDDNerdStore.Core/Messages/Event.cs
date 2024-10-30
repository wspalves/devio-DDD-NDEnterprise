using MediatR;

namespace DDDNerdStore.Core.Messages;

public abstract class Event : Message, INotification
{
    public Event()
    {
        Timestamp = DateTime.UtcNow;
    }

    public DateTime Timestamp { get; private set; }
}