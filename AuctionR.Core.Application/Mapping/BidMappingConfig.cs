using AuctionR.Core.Application.Contracts.Responses;
using AuctionR.Core.Application.Features.Bids.Commands.PlaceBid;
using AuctionR.Core.Domain.Entities;
using Mapster;

namespace AuctionR.Core.Application.Mapping;

public class BidMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PlaceBidCommand, Bid>()
            .Map(dest => dest.AuctionId, src => src.AuctionId)
            .Map(dest => dest.BidderId, src => src.BidderId)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Timestamp, _ => DateTime.UtcNow);

        config.NewConfig<Bid, BidRetractedResponse>()
            .Map(dest => dest.PreviousHighestBidId, src => src.Id)
            .Map(dest => dest.AuctionId, src => src.AuctionId)
            .Map(dest => dest.PreviousHighestBidderId, src => src.BidderId)
            .Map(dest => dest.PreviousHighestBidAmount, src => src.Amount)
            .Map(dest => dest.PreviousHighestBidTimestamp, src => src.Timestamp);
    }
}
