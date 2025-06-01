using AuctionR.Core.Application.Models;
using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Enums;
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

    public async Task<AuctionModel?> Handle(CreateAuctionCommand command, CancellationToken ct)
    {
        var existingAuctions = await _unitOfWork.Auctions
            .FindAsync(a => a.ProductId == command.ProductId, ct);

        foreach (var auction in existingAuctions)
        {
            if(auction.Status != AuctionStatus.Cancelled)
            {
                return null;
            }
        }

        var newAuction = command.Adapt<Auction>();

        await _unitOfWork.Auctions.AddAsync(newAuction, ct);
        await _unitOfWork.Complete(ct);

        return newAuction.Adapt<AuctionModel>();
    }
}