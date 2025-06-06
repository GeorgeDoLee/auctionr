using FluentValidation;

namespace AuctionR.Core.Application.Queries.Auctions.Search;

public class SearchAuctionsQueryValidator : AbstractValidator<SearchAuctionsQuery>
{
    public SearchAuctionsQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).When(x => x.ProductId.HasValue)
            .WithMessage("ProductId must be greater than 0.");

        RuleFor(x => x.OwnerId)
            .GreaterThan(0).When(x => x.OwnerId.HasValue)
            .WithMessage("OwnerId must be greater than 0.");

        RuleFor(x => x.MaximumStartingPrice)
            .GreaterThanOrEqualTo(0).When(x => x.MaximumStartingPrice.HasValue)
            .WithMessage("MaximumStartingPrice must be 0 or more.");

        RuleFor(x => x.Currency)
            .Matches("^[A-Z]{3}$")
            .WithMessage("Currency must be a 3-letter uppercase ISO code.");

        RuleFor(x => x.Title)
            .MaximumLength(100)
            .WithMessage("Title cannot be longer than 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .WithMessage("Description cannot be longer than 2000 characters.");

        RuleFor(x => x.MinimumStartTime)
            .LessThanOrEqualTo(x => x.MaximumEndTime!.Value)
            .When(x => x.MinimumStartTime.HasValue && x.MaximumStartingPrice.HasValue)
            .WithMessage("MinimumStartTime must be earlier than or equal to MaximumEndTime.");

        RuleFor(x => x.MaximumCurrentBidAmount)
            .GreaterThanOrEqualTo(0).When(x => x.MaximumCurrentBidAmount.HasValue)
            .WithMessage("HighestBidAmount must be 0 or more.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0.");
    }
}
