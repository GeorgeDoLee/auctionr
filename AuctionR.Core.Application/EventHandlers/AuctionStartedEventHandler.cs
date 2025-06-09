using AuctionR.Core.Application.Common.Interfaces;
using AuctionR.Core.Application.Contracts.Dtos;
using AuctionR.Core.Domain.Events;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.EventHandlers;

internal sealed class AuctionStartedEventHandler : INotificationHandler<AuctionStartedEvent>
{
    private readonly IEnumerable<INotifier> _notifiers;
    private readonly ILogger<AuctionStartedEventHandler> _logger;

    public AuctionStartedEventHandler(
        IEnumerable<INotifier> notifiers,
        ILogger<AuctionStartedEventHandler> logger)
    {
        _notifiers = notifiers;
        _logger = logger;
    }

    public async Task Handle(AuctionStartedEvent auctionStartedEvent, CancellationToken ct)
    {
        _logger.LogInformation("Handling AuctionStartedEvent.");
        var tasks = _notifiers.Select(n =>
            n.NotifyAuctionStasrtedAsync(auctionStartedEvent.Adapt<AuctionStartedDto>())
        );

        await Task.WhenAll(tasks);
    }
}
