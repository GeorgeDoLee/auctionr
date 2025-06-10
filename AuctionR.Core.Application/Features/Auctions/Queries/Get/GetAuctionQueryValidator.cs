using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Features.Auctions.Queries.Get;

public sealed class GetAuctionQueryValidator : AbstractValidator<GetAuctionQuery>
{
    public GetAuctionQueryValidator()
    {
        RuleFor(x => x.Id).ValidId();
    }
}
