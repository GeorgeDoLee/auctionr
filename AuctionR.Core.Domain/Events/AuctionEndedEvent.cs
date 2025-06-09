using AuctionR.Core.Domain.Primitives;

namespace AuctionR.Core.Domain.Events;

public record AuctionEndedEvent(Guid Id, int AuctionId, DateTime EndTime) : DomainEvent(Id);
