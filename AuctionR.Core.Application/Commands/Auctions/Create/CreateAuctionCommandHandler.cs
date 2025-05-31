using AuctionR.Core.Application.Models;
using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.Create;

public class CreateAuctionCommandHandler
    : IRequestHandler<CreateAuctionCommand, AuctionModel?>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuctionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AuctionModel?> Handle(CreateAuctionCommand command, CancellationToken cancellationToken)
    {
        var existingAuction = await _unitOfWork.Auctions
            .FindAsync(a => a.ProductId == command.ProductId);

        if (existingAuction.Any())
        {
            return null;
        }

        var newAuction = command.Adapt<Auction>();

        await _unitOfWork.Auctions.AddAsync(newAuction);
        await _unitOfWork.Complete();

        return newAuction.Adapt<AuctionModel>();
    }
}