using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Queries.Auctions.Get;

public class GetAuctionQueryValidator : AbstractValidator<GetAuctionQuery>
{
    public GetAuctionQueryValidator()
    {
        RuleFor(x => x.Id).ValidId();
    }
}
