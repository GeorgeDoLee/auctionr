using AuctionR.Core.Domain.Primitives;

namespace AuctionR.Core.Domain.Events;

public record AuctionPostponedEvent(Guid Id, int AuctionId, DateTime NewStartTime, DateTime NewEndTime) : DomainEvent(Id);