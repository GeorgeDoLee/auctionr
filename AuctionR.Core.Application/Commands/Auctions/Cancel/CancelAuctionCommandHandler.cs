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
        _logger.LogInformation("Trying to cancel an auction with id: {auctionId}", command.Id);
        var auction = await _unitOfWork.Auctions.GetAsync(command.Id, ct);

        if (auction == null)
        {
            _logger.LogWarning("Auction with id: {auctionId} could not be found.", command.Id);
            throw new NotFoundException($"Auction with id: {command.Id} could not be found.");
        }

        if (auction.Status == AuctionStatus.Ended)
        {
            _logger.LogWarning("Auction with id: {auctionId} could not be cancelled.", command.Id);
            throw new InvalidOperationException("Cannot cancel an auction that has already ended.");
        }

        auction.Status = AuctionStatus.Cancelled;
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Auction with id: {auctionId} cancelled successfully.", command.Id);
        return true;
    }
}
