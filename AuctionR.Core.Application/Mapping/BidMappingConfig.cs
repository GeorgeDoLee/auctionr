using AuctionR.Core.Application.Commands.Bids.Create;
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
    }
}
