using FluentValidation;

namespace AuctionR.Core.Application.Features.Auctions.Queries.GetAll;

internal sealed class GetAllAuctionsQueryValidator : AbstractValidator<GetAllAuctionsQuery>
{
    public GetAllAuctionsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0.");
    }
}
