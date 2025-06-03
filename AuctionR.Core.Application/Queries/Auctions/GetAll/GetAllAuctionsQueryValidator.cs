using FluentValidation;

namespace AuctionR.Core.Application.Queries.Auctions.GetAll;

public class GetAllAuctionsQueryValidator : AbstractValidator<GetAllAuctionsQuery>
{
    public GetAllAuctionsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0.");
    }
}
