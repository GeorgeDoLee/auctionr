using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Commands.Auctions.Update;

public class UpdateAuctionCommandHandler : IRequestHandler<UpdateAuctionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateAuctionCommandHandler> _logger;
    public UpdateAuctionCommandHandler(
        IUnitOfWork unitOfWork, 
        ILogger<UpdateAuctionCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateAuctionCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Trying to update auction with id: {id}.", command.Id);
        var auction = await _unitOfWork.Auctions.GetAsync(command.Id, ct);

        if (auction == null)
        {
            _logger.LogWarning("Auction with id: {id} could not be found.", command.Id);
            throw new NotFoundException($"Auction with id: {command.Id} could not be found.");
        }

        command.Adapt(auction);
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Auction with id: {id} updated successfully.", command.Id);
        return true;
    }
}