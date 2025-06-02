using AuctionR.Core.Domain.Enums;
using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Commands.Auctions.End;

public class EndAuctionCommandHandler : IRequestHandler<EndAuctionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EndAuctionCommand> _logger;

    public EndAuctionCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<EndAuctionCommand> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(EndAuctionCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Trying to manually end auction with id: {auctionId}.", command.Id);
        var auction = await _unitOfWork.Auctions.GetAsync(command.Id, ct);

        if (auction == null)
        {
            _logger.LogWarning("Auction with id: {auctionId} could not be found.", command.Id);
            throw new NotFoundException($"Auction with id: {command.Id} could not be found.");
        }

        if (auction.Status != AuctionStatus.Active)
        {
            _logger.LogWarning("Auction with id: {auctionId} could not be ended manually.", command.Id);
            throw new InvalidOperationException("Only active auctions can be ended manually.");
        }

        auction.Status = AuctionStatus.Ended;
        auction.EndTime = DateTime.UtcNow;

        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Auction with id: {auctionId} successfully ended.", command.Id);
        return true;
    }
}
