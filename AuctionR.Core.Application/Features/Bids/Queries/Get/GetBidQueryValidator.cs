using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Features.Bids.Queries.Get;

public sealed class GetBidQueryValidator : AbstractValidator<GetBidQuery>
{
    public GetBidQueryValidator()
    {
        RuleFor(x => x.Id).ValidId();
    }
}
