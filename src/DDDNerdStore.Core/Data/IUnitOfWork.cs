namespace DDDNerdStore.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}