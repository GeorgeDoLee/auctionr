using FluentValidation;

namespace AuctionR.Core.Application.Commands.Bids.Create;

public class PlaceBidCommandValidator : AbstractValidator<PlaceBidCommand>
{
    public PlaceBidCommandValidator()
    {
        RuleFor(x => x.AuctionId)
            .NotEmpty().WithMessage("AuctionId is required.")
            .GreaterThan(0).WithMessage("AuctionId must be greater than 0.");

        RuleFor(x => x.BidderId)
            .NotEmpty().WithMessage("BidderId is required.")
            .GreaterThan(0).WithMessage("BidderId must be greater than 0.");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount is required.")
            .GreaterThan(0).WithMessage("Bid amount must be greater than 0.");
    }
}
