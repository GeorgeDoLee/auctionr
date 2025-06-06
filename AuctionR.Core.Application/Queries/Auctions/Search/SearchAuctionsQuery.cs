using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Enums;
using MediatR;

namespace AuctionR.Core.Application.Queries.Auctions.Search;

public class SearchAuctionsQuery : IRequest<IEnumerable<AuctionModel>>
{
    public int? ProductId { get; set; }
    public int? OwnerId { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }

    public decimal? MaximumStartingPrice { get; set; }
    public string? Currency { get; set; }

    public DateTime? MinimumStartTime { get; set; }
    public DateTime? MaximumEndTime { get; set; }

    public decimal? MaximumCurrentBidAmount { get; set; } 

    public AuctionStatus? Status { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}
