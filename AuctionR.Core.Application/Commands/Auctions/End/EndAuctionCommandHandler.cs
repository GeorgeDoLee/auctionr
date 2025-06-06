using AuctionR.Core.Application.Common.Guards;
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
        _logger.LogInformation("Trying to manually end auction with id: {auctionId}.", command.AuctionId);
        var auction = await _unitOfWork.Auctions.GetAsync(command.AuctionId, ct);

        if (auction == null)
        {
            _logger.LogWarning("Auction with id: {auctionId} could not be found.", command.AuctionId);
            throw new NotFoundException($"Auction with id: {command.AuctionId} could not be found.");
        }

        Guard.EnsureUserOwnsResource(auction.OwnerId, command.UserId, nameof(auction), _logger);

        auction.End();
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Auction with id: {auctionId} successfully ended.", command.AuctionId);
        return true;
    }
}