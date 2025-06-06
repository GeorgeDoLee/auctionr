using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Enums;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Features.Auctions.Commands.Create;

internal sealed class CreateAuctionCommandHandler
    : IRequestHandler<CreateAuctionCommand, AuctionModel?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateAuctionCommandHandler> _logger;
    public CreateAuctionCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<CreateAuctionCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<AuctionModel?> Handle(CreateAuctionCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Initiating auction creation with properties: {@auction}", command);
        var existingAuctions = await _unitOfWork.Auctions
            .FindAsync(a => a.ProductId == command.ProductId, ct);

        foreach (var auction in existingAuctions)
        {
            if(auction.Status != AuctionStatus.Cancelled)
            {
                _logger.LogWarning("There already exists pending, running or ended auction with same product.");
                return null;
            }
        }

        var newAuction = command.Adapt<Auction>();

        await _unitOfWork.Auctions.AddAsync(newAuction, ct);
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("New auction created successfully.");
        return newAuction.Adapt<AuctionModel>();
    }
}