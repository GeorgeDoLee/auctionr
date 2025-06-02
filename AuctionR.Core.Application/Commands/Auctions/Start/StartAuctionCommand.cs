using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.Start;

public class StartAuctionCommand : IRequest<bool>
{
    public StartAuctionCommand(int id)
    {
        Id = id;    
    }

    public int Id { get; set; }
}
