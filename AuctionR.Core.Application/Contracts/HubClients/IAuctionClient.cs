namespace AuctionR.Core.Application.Contracts.HubClients;

public interface IAuctionClient
{
    Task AuctionUpdated(string message);
}