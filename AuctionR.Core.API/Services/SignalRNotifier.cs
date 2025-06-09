using AuctionR.Core.API.Hubs;
using AuctionR.Core.Application.Common.Interfaces;
using AuctionR.Core.Application.Contracts.Dtos;
using AuctionR.Core.Application.Contracts.HubClients;
using AuctionR.Core.Application.Contracts.Models;
using Microsoft.AspNetCore.SignalR;

namespace AuctionR.Core.API.Services;

internal sealed class SignalRNotifier : INotifier
{
    private readonly IHubContext<AuctionHub, IAuctionClient> _hubContext;
    private readonly ILogger<SignalRNotifier> _logger;

    public SignalRNotifier(
        IHubContext<AuctionHub, IAuctionClient> hubContext,
        ILogger<SignalRNotifier> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public Task NotifyAuctionPostponedAsync(AuctionPostponedDto dto) =>
        NotifyAsync(dto.AuctionId, "postponed auction", client => client.AuctionPostponed(dto));

    public Task NotifyAuctionEndedAsync(AuctionEndedDto dto) =>
        NotifyAsync(dto.AuctionId, "recently ended auction", client => client.AuctionEnded(dto));

    public Task NotifyAuctionStartedAsync(AuctionStartedDto dto) =>
        NotifyAsync(dto.AuctionId, "newly started auction", client => client.AuctionStarted(dto));

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
