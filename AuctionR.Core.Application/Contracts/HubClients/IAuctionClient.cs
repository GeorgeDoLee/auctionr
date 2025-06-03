using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Application.Contracts.Responses;

namespace AuctionR.Core.Application.Contracts.HubClients;

public interface IAuctionClient
{
    Task BidPlaced(BidModel bidModel);

    Task BidRetracted(BidRetractedResponse bidRetractedResponse);
}