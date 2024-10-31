using DDDNerdStore.Core.Messages;

namespace DDDNerdStore.Core.Bus;

public interface IMediatorHandler
{
    Task PublicarEvento<T>(T evento) where T : Event;
    Task<bool> EnviarComando<T>(T evento) where T : Command;
}