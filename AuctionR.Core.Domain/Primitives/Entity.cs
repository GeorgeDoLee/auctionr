namespace AuctionR.Core.Domain.Primitives;

public abstract class Entity
{
    public int Id { get; set; }

    private readonly List<DomainEvent> _domainEvents = new();

    public ICollection<DomainEvent> DomainEvents => _domainEvents;

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
