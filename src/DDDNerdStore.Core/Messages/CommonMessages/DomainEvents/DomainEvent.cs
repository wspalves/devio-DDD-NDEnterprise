namespace DDDNerdStore.Core.Messages.CommonMessages.DomainEvents;

public class DomainEvent : Event
{
    public DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}