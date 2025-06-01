using AuctionR.Core.Application.Commands.Auctions.Create;
using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Enums;
using Mapster;

namespace AuctionR.Core.Application.Mapping;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateAuctionCommand, Auction>()
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.StartingPrice, src => src.StartingPrice)
            .Map(dest => dest.StartTime, src => src.StartTime)
            .Map(dest => dest.EndTime, src => src.EndTime)
            .Map(dest => dest.Status, _ => AuctionStatus.Pending)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Bids!)
            .Ignore(dest => dest.HighestBidAmount!)
            .Ignore(dest => dest.HighestBidderId!);
    }
}