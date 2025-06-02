using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.Cancel;

public class CancelAuctionCommand : IRequest<bool>
{
    public CancelAuctionCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
