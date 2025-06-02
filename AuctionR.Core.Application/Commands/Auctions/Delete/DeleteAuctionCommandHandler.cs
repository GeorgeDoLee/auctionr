using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Commands.Auctions.Delete;

public class DeleteAuctionCommandHandler : IRequestHandler<DeleteAuctionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteAuctionCommandHandler> _logger;
    public DeleteAuctionCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<DeleteAuctionCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAuctionCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Trying to delete auction with id: {id}.", command.Id);

        var auction = await _unitOfWork.Auctions.GetAsync(command.Id, ct);

        if (auction == null)
        {
            _logger.LogWarning("Auction with id: {id} could not be found.", command.Id);
            throw new NotFoundException($"Auction with id: {command.Id} not found.");
        }

        _unitOfWork.Auctions.Remove(auction);
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Auction with id: {id} deleted successfully.", command.Id);
        return true;
    }
}