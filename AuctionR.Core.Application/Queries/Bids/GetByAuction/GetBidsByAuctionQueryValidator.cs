using AuctionR.Core.Application.Extensions;
using FluentValidation;

namespace AuctionR.Core.Application.Queries.Bids.GetByAuction;

public class GetBidsByAuctionQueryValidator : AbstractValidator<GetBidsByAuctionQuery>
{
    public GetBidsByAuctionQueryValidator()
    {
        RuleFor(x => x.AuctionId).ValidId("Auction Id");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0.");
    }
}
