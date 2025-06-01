using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuctionR.Core.Application.Queries.Auctions.GetAll;

public class GetAllAuctionsQueryHandler :
    IRequestHandler<GetAllAuctionsQuery, IEnumerable<AuctionModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllAuctionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AuctionModel>> Handle(
        GetAllAuctionsQuery query, CancellationToken ct)
    {

        var auctions = await _unitOfWork.Auctions
            .GetAllAsync(query.PageNumber, query.PageSize, ct);

        return auctions.Adapt<List<AuctionModel>>();
    }
}
