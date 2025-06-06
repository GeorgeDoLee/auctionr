using AuctionR.Core.Application.Contracts.Models;
using MediatR;

namespace AuctionR.Core.Application.Features.Bids.Queries.Search;

public sealed class SearchBidsQuery : IRequest<IEnumerable<BidModel>>
{
    public int? AuctionId { get; set; }

    public int? BidderId { get; set; }

    public decimal? MaxAmount { get; set; }

    public decimal? MinAmount { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}
