using AuctionR.Core.Domain.Primitives;

namespace AuctionR.Core.Domain.Events;

public record BidPlacedEvent(Guid Id, int AuctionId, int BidId) : DomainEvent(Id);
