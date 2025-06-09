using AuctionR.Core.Application.Common.Interfaces;
using AuctionR.Core.Application.Contracts.Dtos;
using AuctionR.Core.Domain.Events;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.EventHandlers;

internal sealed class AuctionCancelledEventHandler : INotificationHandler<AuctionCancelledEvent>
{
    private readonly IEnumerable<INotifier> _notifiers;
    private readonly ILogger<AuctionCancelledEventHandler> _logger;

    public AuctionCancelledEventHandler(
        IEnumerable<INotifier> notifiers,
        ILogger<AuctionCancelledEventHandler> logger)
    {
        _notifiers = notifiers;
        _logger = logger;
    }
    
    public async Task Handle(AuctionCancelledEvent auctionCancelledEvent, CancellationToken ct)
    {
        _logger.LogInformation("Handling AuctionCancelledEvent.");
        var tasks = _notifiers.Select(n =>
            n.NotifyAuctionCancelledAsync(auctionCancelledEvent.Adapt<AuctionCancelledDto>())
        );

        await Task.WhenAll(tasks);
    }
}
