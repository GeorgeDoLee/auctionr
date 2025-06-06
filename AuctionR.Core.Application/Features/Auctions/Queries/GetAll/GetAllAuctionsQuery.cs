using AuctionR.Core.Application.Contracts.Models;
using MediatR;

namespace AuctionR.Core.Application.Features.Auctions.Queries.GetAll;

public sealed class GetAllAuctionsQuery : IRequest<IEnumerable<AuctionModel>>
{
    public int PageNumber { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
}
