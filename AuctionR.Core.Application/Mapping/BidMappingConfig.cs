using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Application.Features.Bids.Commands.PlaceBid;
using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Events;
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

        config.NewConfig<BidPlacedEvent, BidModel>()
            .Map(dest => dest.Id, src => src.BidId)
            .Map(dest => dest.AuctionId, src => src.AuctionId)
            .Map(dest => dest.BidderId, src => src.BidderId)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Timestamp, src => src.Timestamp);
    }
}
