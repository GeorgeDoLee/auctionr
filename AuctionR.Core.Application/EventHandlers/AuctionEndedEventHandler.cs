using AuctionR.Core.Application.Common.Interfaces;
using AuctionR.Core.Application.Contracts.Dtos;
using AuctionR.Core.Domain.Events;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.EventHandlers;

internal sealed class AuctionEndedEventHandler : INotificationHandler<AuctionEndedEvent>
{
    private readonly IEnumerable<INotifier> _notifiers;
    private readonly ILogger<AuctionEndedEventHandler> _logger;

    public AuctionEndedEventHandler(
        IEnumerable<INotifier> notifiers,
        ILogger<AuctionEndedEventHandler> logger)
    {
        _notifiers = notifiers;
        _logger = logger;
    }

    public async Task Handle(AuctionEndedEvent auctionEndedEvent, CancellationToken ct)
    {
        _logger.LogInformation("Handling AuctionEndedEvent.");
        var tasks = _notifiers.Select(n =>
            n.NotifyAuctionEndedAsync(auctionEndedEvent.Adapt<AuctionEndedDto>())
        );

        await Task.WhenAll(tasks);
    }
}
