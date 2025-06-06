using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Application.Features.Auctions.Commands.Create;
using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Enums;
using Mapster;

namespace AuctionR.Core.Application.Mapping;

public class AuctionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateAuctionCommand, Auction>()
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.OwnerId, src => src.OwnerId)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.StartingPrice, src => src.StartingPrice)
            .Map(dest => dest.Currency, src => src.Currency)
            .Map(dest => dest.StartTime, src => src.StartTime)
            .Map(dest => dest.EndTime, src => src.EndTime)
            .Map(dest => dest.CreatedAt, _ => DateTime.UtcNow)
            .Map(dest => dest.Status, _ => AuctionStatus.Pending)
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Bids!)
            .Ignore(dest => dest.HighestBidAmount!)
            .Ignore(dest => dest.HighestBidderId!);

        config.NewConfig<Auction, AuctionModel>()
            .Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.Bids, src => src.Bids);
    }
}