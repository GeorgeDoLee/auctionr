using AuctionR.Core.Application.Contracts.Dtos;
using AuctionR.Core.Application.Contracts.Models;

namespace AuctionR.Core.Application.Contracts.HubClients;

public interface IAuctionClient
{
    Task BidPlaced(BidModel bidModel);

    Task AuctionStarted(AuctionStartedDto auctionStartedDto);

    Task AuctionEnded(AuctionEndedDto auctionEndedDto);

}