using AuctionR.Core.Domain.Primitives;

namespace AuctionR.Core.Domain.Events;

public record BidRetractedEvent(Guid Id, int AuctionId, int BidId) : DomainEvent(Id);
