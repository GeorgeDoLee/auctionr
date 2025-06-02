using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.End;

public class EndAuctionCommand : IRequest<bool>
{
    public EndAuctionCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
