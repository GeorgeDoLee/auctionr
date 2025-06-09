using AuctionR.Core.Domain.Primitives;

namespace AuctionR.Core.Domain.Events;

public record AuctionStartedEvent(Guid Id, int AuctionId, DateTime StartTime) : DomainEvent(Id);
