using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Queries.Bids.Get;

public class GetBidQueryValidator : AbstractValidator<GetBidQuery>
{
    public GetBidQueryValidator()
    {
        RuleFor(x => x.Id).ValidId();
    }
}
