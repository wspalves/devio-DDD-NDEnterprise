using DDDNerdStore.Core.Messages;
using MediatR;

namespace DDDNerdStore.Core.Bus;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublicarEvento<T>(T evento) where T : Event
    {
        await _mediator.Publish(evento);
    }

    public async Task<bool> EnviarComando<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }
}