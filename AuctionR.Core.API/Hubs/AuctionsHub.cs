using AuctionR.Core.Application.Contracts.HubClients;
using Microsoft.AspNetCore.SignalR;

namespace AuctionR.Core.API.Hubs;

public class AuctionsHub : Hub<IAuctionClient>
{
    public async Task JoinAuction(int auctionId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"auction-{auctionId}");
    }

    public async Task LeaveAuction(int auctionId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"auction-{auctionId}");
    }
}