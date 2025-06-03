using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Commands.Auctions.Postpone;

public class PostponeAuctionCommandHandler : IRequestHandler<PostponeAuctionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PostponeAuctionCommandHandler> _logger;

    public PostponeAuctionCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<PostponeAuctionCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(PostponeAuctionCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Trying to postpone auction with id: {auctionId}.", command.Id);
        var auction = await _unitOfWork.Auctions.GetAsync(command.Id);

        if (auction == null)
        {
            _logger.LogWarning("Auction with id: {auctionId} could not be found.", command.Id);
            throw new NotFoundException($"Auction with id: {command.Id} could not be found.");
        }

        auction.Postpone(command.StartTime, command.EndTime);
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Auction with id: {auctionId} postponed succussfully.", command.Id);
        return true;
    }
}
