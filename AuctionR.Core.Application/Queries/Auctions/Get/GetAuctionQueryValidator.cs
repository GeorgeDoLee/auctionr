using FluentValidation;

namespace AuctionR.Core.Application.Queries.Auctions.Get;

public class GetAuctionQueryValidator : AbstractValidator<GetAuctionQuery>
{
    public GetAuctionQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
