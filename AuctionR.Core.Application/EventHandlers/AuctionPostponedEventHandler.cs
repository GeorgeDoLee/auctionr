using AuctionR.Core.Application.Common.Interfaces;
using AuctionR.Core.Application.Contracts.Dtos;
using AuctionR.Core.Domain.Events;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.EventHandlers;

internal sealed class AuctionPostponedEventHandler : INotificationHandler<AuctionPostponedEvent>
{
    private readonly IEnumerable<INotifier> _notifiers;
    private readonly ILogger<AuctionPostponedEventHandler> _logger;

    public AuctionPostponedEventHandler(
        IEnumerable<INotifier> notifiers,
        ILogger<AuctionPostponedEventHandler> logger)
    {
        _notifiers = notifiers;
        _logger = logger;
    }

    public async Task Handle(AuctionPostponedEvent auctionPostponedEvent, CancellationToken ct)
    {
        _logger.LogInformation("Handling AuctionPostponedEvent.");
        var tasks = _notifiers.Select(n =>
            n.NotifyAuctionPostponedAsync(auctionPostponedEvent.Adapt<AuctionPostponedDto>())
        );

        await Task.WhenAll(tasks);
    }
}
