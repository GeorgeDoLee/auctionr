using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Features.Auctions.Queries.GetAll;

internal sealed class GetAllAuctionsQueryHandler :
    IRequestHandler<GetAllAuctionsQuery, IEnumerable<AuctionModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllAuctionsQueryHandler> _logger;
    public GetAllAuctionsQueryHandler(
        IUnitOfWork unitOfWork, 
        ILogger<GetAllAuctionsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<AuctionModel>> Handle(
        GetAllAuctionsQuery query, CancellationToken ct)
    {
        _logger.LogInformation("trying to fetch auctions on page {pageNumber} with size {pageSize}.", 
            query.PageNumber, query.PageSize);

        var auctions = await _unitOfWork.Auctions
            .GetPagedAsync(query.PageNumber, query.PageSize, null, ct);

        _logger.LogInformation("Auctions on page {pageNumber} with size {pageSize} fetched successfully.",
            query.PageNumber, query.PageSize);

        return auctions.Adapt<List<AuctionModel>>();
    }
}
