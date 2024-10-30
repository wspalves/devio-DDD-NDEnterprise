using DDDNerdStore.Core.DomainObjects;

namespace DDDNerdStore.Core.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}