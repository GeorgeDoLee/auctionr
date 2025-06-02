using AuctionR.Core.Domain.Enums;
using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Commands.Auctions.Start;

public class StartAuctionCommandHandler : IRequestHandler<StartAuctionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<StartAuctionCommandHandler> _logger;

    public StartAuctionCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<StartAuctionCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(StartAuctionCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Tryin to start auction with id: {auctionId}", command.Id);
        var auction = await _unitOfWork.Auctions.GetAsync(command.Id, ct);

        if (auction == null)
        {
            _logger.LogWarning("Auction with id: {auctionId} could not be found.", command.Id);
            throw new NotFoundException($"Auction with id: {command.Id} could not be found.");
        }

        if (auction.Status != AuctionStatus.Pending)
        {
            _logger.LogWarning("Auction with id: {auctionId} could not be started.", command.Id);
            throw new InvalidOperationException("Only pending auctions can be started manually.");
        }

        auction.StartTime = DateTime.UtcNow;
        auction.Status = AuctionStatus.Active;
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Auction with id: {auctionId} started successfully.", command.Id);
        return true;
    }
}
