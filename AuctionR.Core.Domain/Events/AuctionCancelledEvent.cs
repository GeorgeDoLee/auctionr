using AuctionR.Core.Domain.Primitives;

namespace AuctionR.Core.Domain.Events;

public record AuctionCancelledEvent(Guid Id, int AuctionId) : DomainEvent(Id);