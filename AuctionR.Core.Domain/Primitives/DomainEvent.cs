using MediatR;

namespace AuctionR.Core.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;