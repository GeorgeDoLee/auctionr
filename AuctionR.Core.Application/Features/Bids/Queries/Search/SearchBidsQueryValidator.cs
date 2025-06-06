using FluentValidation;

namespace AuctionR.Core.Application.Features.Bids.Queries.Search;

internal sealed class SearchBidsQueryValidator : AbstractValidator<SearchBidsQuery>
{
    public SearchBidsQueryValidator()
    {
        RuleFor(x => x.AuctionId)
            .GreaterThan(0).When(x => x.AuctionId.HasValue)
            .WithMessage("AuctionId must be greater than 0.");

        RuleFor(x => x.BidderId)
            .GreaterThan(0).When(x => x.BidderId.HasValue)
            .WithMessage("BidderId must be greater than 0.");

        RuleFor(x => x.MaxAmount)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MaxAmount.HasValue)
            .WithMessage("MaxAmount must be 0 or more.");

        RuleFor(x => x.MinAmount)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MinAmount.HasValue)
            .WithMessage("MinAmount must be 0 or more.");

        RuleFor(x => x)
            .Must(x => x.MinAmount <= x.MaxAmount)
            .When(x => x.MinAmount.HasValue && x.MaxAmount.HasValue)
            .WithMessage("MinAmount must be less than or equal to MaxAmount.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0.");
    }
}
