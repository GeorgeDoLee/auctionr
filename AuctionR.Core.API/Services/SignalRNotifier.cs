using AuctionR.Core.API.Hubs;
using AuctionR.Core.Application.Common.Interfaces;
using AuctionR.Core.Application.Contracts.Dtos;
using AuctionR.Core.Application.Contracts.HubClients;
using AuctionR.Core.Application.Contracts.Models;
using Microsoft.AspNetCore.SignalR;

namespace AuctionR.Core.API.Services;

internal sealed class SignalRNotifier : INotifier
{
    private readonly IHubContext<AuctionsHub, IAuctionClient> _hubContext;
    private readonly ILogger<SignalRNotifier> _logger;

    public SignalRNotifier(
        IHubContext<AuctionsHub, IAuctionClient> hubContext,
        ILogger<SignalRNotifier> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public Task NotifyAuctionPostponedAsync(AuctionPostponedDto dto) =>
        NotifyAsync(dto.AuctionId, "postponed auction", client => client.AuctionPostponed(dto));

    public Task NotifyAuctionEndedAsync(AuctionEndedDto dto) =>
        NotifyAsync(dto.AuctionId, "ended auction", client => client.AuctionEnded(dto));

    public Task NotifyAuctionStartedAsync(AuctionStartedDto dto) =>
        NotifyAsync(dto.AuctionId, "started auction", client => client.AuctionStarted(dto));

    public Task NotifyAuctionCancelledAsync(AuctionCancelledDto dto) =>
        NotifyAsync(dto.AuctionId, "cancelled auction", client => client.AuctionCancelled(dto));

    public Task NotifyBidPlacedAsync(BidModel model) =>
        NotifyAsync(model.AuctionId, "newly placed bid", client => client.BidPlaced(model));

    private async Task NotifyAsync(
        int auctionId,
        string actionDescription,
        Func<IAuctionClient, Task> notifyAction)
    {
        _logger.LogInformation("Distributing {ActionDescription} via SignalR.", actionDescription);

        var group = _hubContext.Clients.Group($"auction-{auctionId}");
        await notifyAction(group);
    }
}
