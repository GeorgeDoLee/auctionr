using AuctionR.Core.Application.Models;
using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.Create;

public class CreateAuctionCommand : IRequest<AuctionModel?>
{
    public int ProductId { get; set; }

    public decimal StartingPrice { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
