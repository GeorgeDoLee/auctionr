using AuctionR.Core.Application.Common.Interfaces;
using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Events;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.EventHandlers;

internal sealed class BidPlacedEventHandler : INotificationHandler<BidPlacedEvent>
{
    private readonly IEnumerable<INotifier> _notifiers;
    private readonly ILogger<BidPlacedEventHandler> _logger;

    public BidPlacedEventHandler(
        IEnumerable<INotifier> notifiers,
        ILogger<BidPlacedEventHandler> logger)
    {
        _notifiers = notifiers;
        _logger = logger;
    }

    public async Task Handle(BidPlacedEvent bidPlacedEvent, CancellationToken ct)
    {
        _logger.LogInformation("Handling BidPlacedEvent.");
        var tasks = _notifiers.Select(n =>
            n.NotifyBidPlacedAsync(bidPlacedEvent.Adapt<BidModel>())
        );

        await Task.WhenAll(tasks);
    }
}
