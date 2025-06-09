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

    public async Task NotifyAuctionStasrtedAsync(AuctionStartedDto auctionStartedDto)
    {
        _logger.LogInformation("Distributing newly started auction via SignalR.");
        await _hubContext.Clients
                .Group($"auction-{auctionStartedDto.AuctionId}")
                .AuctionStarted(auctionStartedDto);
    }

    public async Task NotifyBidPlacedAsync(BidModel bidModel)
    {
        _logger.LogInformation("Distributing newly placed bid via SignalR.");
        await _hubContext.Clients
                .Group($"auction-{bidModel.AuctionId}")
                .BidPlaced(bidModel);
    }
}
