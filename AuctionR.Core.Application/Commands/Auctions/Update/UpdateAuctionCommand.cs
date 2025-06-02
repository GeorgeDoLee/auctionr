using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Enums;
using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.Update;

public class UpdateAuctionCommand : IRequest<bool>
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public decimal StartingPrice { get; set; }

    public decimal MinimumBidIncrement { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public decimal? HighestBidAmount { get; set; }

    public int? HighestBidderId { get; set; }

    public AuctionStatus Status { get; set; }
}
