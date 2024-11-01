using DDDNerdStore.Core.Messages;

namespace DDDNerdStore.Core.DomainObjects;

public abstract class Entity
{
    public Guid Id { get; set; }

    private List<Event> _notificacoes;
    public IReadOnlyCollection<Event>? Notificacoes => _notificacoes?.AsReadOnly();

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public virtual bool EhValido()
    {
        throw new NotImplementedException();
    }

    public void AdicionarEvento(Event notificacao)
    {
        _notificacoes = _notificacoes ?? new List<Event>();
        _notificacoes.Add(notificacao);
    }

    public void RemoverEvento(Event notificacao) => _notificacoes?.Remove(notificacao);

    public void LimparEventos() => _notificacoes?.Clear();

    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id: {Id}]";
    }
}