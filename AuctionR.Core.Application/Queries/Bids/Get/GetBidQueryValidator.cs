using FluentValidation;

namespace AuctionR.Core.Application.Queries.Bids.Get;

public class GetBidQueryValidator : AbstractValidator<GetBidQuery>
{
    public GetBidQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
