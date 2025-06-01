using AuctionR.Core.Application.Contracts.Models;

namespace AuctionR.Core.Application.Contracts.HubClients;

public interface IAuctionClient
{
    Task AuctionUpdated(AuctionModel auctionModel);
}