using DDDNerdStore.Core.Communication.Mediator;
using DDDNerdStore.Core.DomainObjects;

namespace DDDNerdStore.Vendas.Data;

public static class MediatorExtension
{
    public static async Task PublicarEventosAsync(this IMediatorHandler mediator, VendasContext context)
    {
        var domainEntities = context.ChangeTracker.Entries<Entity>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

        var domainEvents = domainEntities.SelectMany(x => x.Entity.Notificacoes).ToList();

        domainEntities.ToList().ForEach(entity => entity.Entity.LimparEventos());

        var tasks = domainEvents.Select(async (domainEvent) => await mediator.PublicarEvento(domainEvent));

        await Task.WhenAll(tasks);
    }
}