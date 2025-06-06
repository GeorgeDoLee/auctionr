using AuctionR.Core.Application.Contracts.Models;
using MediatR;

namespace AuctionR.Core.Application.Features.Auctions.Commands.Create;

public sealed class CreateAuctionCommand : IRequest<AuctionModel?>
{
    public int ProductId { get; set; }
    public int OwnerId { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }

    public decimal StartingPrice { get; set; }
    public decimal MinimumBidIncrement { get; set; }
    public string Currency { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
