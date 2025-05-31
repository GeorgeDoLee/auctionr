using AuctionR.Core.Application.Models;
using MediatR;

namespace AuctionR.Core.Application.Queries.Auctions.GetAll;

public class GetAllAuctionsQuery : IRequest<IEnumerable<AuctionModel>>
{
}
