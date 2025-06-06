using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Features.Auctions.Queries.Get;

internal sealed class GetAuctionQueryValidator : AbstractValidator<GetAuctionQuery>
{
    public GetAuctionQueryValidator()
    {
        RuleFor(x => x.Id).ValidId();
    }
}
