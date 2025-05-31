using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.Delete;

public class DeleteAuctionCommand : IRequest<bool>
{
    public DeleteAuctionCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
