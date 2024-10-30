using DDDNerdStore.Core.Messages;

namespace DDDNerdStore.Core.Bus;

public interface IMediatrHandler
{
    Task PublicarEvento<T>(T evento) where T : Event;
}