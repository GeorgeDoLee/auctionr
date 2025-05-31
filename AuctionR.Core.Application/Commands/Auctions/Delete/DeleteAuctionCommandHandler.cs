using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.Delete;

public class DeleteAuctionCommandHandler : IRequestHandler<DeleteAuctionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuctionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteAuctionCommand command, CancellationToken ct)
    {
        var auction = await _unitOfWork.Auctions.GetAsync(command.Id, ct);

        if (auction == null)
        {
            throw new NotFoundException($"Auction with id: {command.Id} not found.");
        }

        _unitOfWork.Auctions.Remove(auction);
        await _unitOfWork.Complete(ct);

        return true;
    }
}