using AuctionR.Core.Application.Contracts.Dtos;
using AuctionR.Core.Application.Contracts.Models;

namespace AuctionR.Core.Application.Common.Interfaces;

public interface INotifier
{
    Task NotifyBidPlacedAsync(BidModel bidModel);

    Task NotifyAuctionPostponedAsync(AuctionPostponedDto auctionPostponedDto);

    Task NotifyAuctionStartedAsync(AuctionStartedDto auctionStartedDto);

    Task NotifyAuctionEndedAsync(AuctionEndedDto auctionEndedDto);

    Task NotifyAuctionCancelledAsync(AuctionCancelledDto auctionCancelledDto);
}