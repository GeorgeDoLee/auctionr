using AuctionR.Core.Application.Common.Guards;
using AuctionR.Core.Domain.Enums;
using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Commands.Auctions.Cancel;

public class CancelAuctionCommandHandler : IRequestHandler<CancelAuctionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CancelAuctionCommandHandler> _logger;

    public CancelAuctionCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<CancelAuctionCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(CancelAuctionCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Trying to cancel an auction with id: {auctionId}", command.AuctionId);
        var auction = await _unitOfWork.Auctions.GetAsync(command.AuctionId, ct);

        if (auction == null)
        {
            _logger.LogWarning("Auction with id: {auctionId} could not be found.", command.AuctionId);
            throw new NotFoundException($"Auction with id: {command.AuctionId} could not be found.");
        }

        Guard.EnsureUserOwnsResource(auction.OwnerId, command.UserId, nameof(auction), _logger);

        auction.Cancel();
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Auction with id: {auctionId} cancelled successfully.", command.AuctionId);
        return true;
    }
}
