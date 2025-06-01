using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.Update;

public class UpdateAuctionCommandHandler : IRequestHandler<UpdateAuctionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuctionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateAuctionCommand command, CancellationToken ct)
    {
        var auction = await _unitOfWork.Auctions.GetAsync(command.Id);

        if (auction == null)
        {
            throw new NotFoundException($"Auction with id: {command.Id} could not be found.");
        }

        command.Adapt(auction);
        await _unitOfWork.Complete();

        return true;
    }
}
